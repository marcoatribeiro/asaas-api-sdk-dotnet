using System.Globalization;
using System.Net;
using Asaas.Sdk.Config;

namespace Asaas.Sdk.Http;

/// <summary>
/// Delegating handler that retries a failed request, and gives each attempt its own timeout.
/// </summary>
/// <remarks>
/// <para>
/// Only requests that are safe to repeat are retried: a <c>GET</c> (or <c>HEAD</c>) without a body. Every other request is
/// sent exactly once. The HTTP verb alone is no proof that repeating a financial operation is safe, and a timeout or a
/// <see cref="HttpRequestException"/> does not show that the server received nothing, so a <c>POST</c>, <c>PUT</c> or
/// <c>DELETE</c> (and any multipart upload) is never repeated, even after a 429 or a 5xx. Send one again yourself, once you
/// know it did not go through.
/// </para>
/// <para>
/// A retried attempt is a response with status 408, 429, 500, 502, 503 or 504, a <see cref="HttpRequestException"/>, or an
/// attempt that timed out. When the server sends <c>Retry-After</c> (seconds or a date) or <c>RateLimit-Reset</c> (seconds),
/// the wait is at least that long; if it is longer than <see cref="RetryConfig.MaxDelayMs"/> the response is returned
/// without waiting, since such a 429 is not a short transient limit (for example the 12-hour quota). Otherwise the wait is the
/// fixed or exponential delay of <see cref="RetryConfig"/>, never above <see cref="RetryConfig.MaxDelayMs"/>.
/// </para>
/// <para>
/// The timeout applies to each attempt (headers and body), not to the whole call, so a call can take up to
/// <c>(MaxRetries + 1)</c> timeouts plus the waits.
/// </para>
/// </remarks>
public class RetryHandler : DelegatingHandler
{
    private readonly RetryConfig _retryConfig;
    private readonly TimeSpan _attemptTimeout;
    private readonly TimeProvider _timeProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="RetryHandler"/> class.
    /// </summary>
    /// <param name="retryConfig">Retry settings, read on every request</param>
    /// <param name="attemptTimeout">Timeout of each attempt, or <see cref="Timeout.InfiniteTimeSpan"/> for none</param>
    /// <param name="timeProvider">Clock used for the waits and the timeouts; the system clock when null</param>
    public RetryHandler(RetryConfig retryConfig, TimeSpan attemptTimeout, TimeProvider? timeProvider = null)
    {
        _retryConfig = retryConfig ?? throw new ArgumentNullException(nameof(retryConfig));
        _attemptTimeout = attemptTimeout;
        _timeProvider = timeProvider ?? TimeProvider.System;
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var maxRetries = IsSafeToRepeat(request) ? Math.Max(0, _retryConfig.MaxRetries) : 0;

        for (var retry = 0; ; retry++)
        {
            HttpResponseMessage response;

            try
            {
                response = await SendAttemptAsync(request, cancellationToken);
            }
            catch (Exception exception) when (retry < maxRetries && IsTransient(exception, cancellationToken))
            {
                await DelayAsync(Backoff(retry + 1), cancellationToken);
                continue;
            }

            if (retry >= maxRetries || !IsTransient(response.StatusCode) || !TryGetDelay(response, retry + 1, out var delay))
            {
                return response;
            }

            response.Dispose();
            await DelayAsync(delay, cancellationToken);
        }
    }

    private static bool IsSafeToRepeat(HttpRequestMessage request) =>
        (request.Method == HttpMethod.Get || request.Method == HttpMethod.Head) && request.Content is null;

    private static bool IsTransient(HttpStatusCode status) =>
        status is HttpStatusCode.RequestTimeout or HttpStatusCode.TooManyRequests or HttpStatusCode.InternalServerError
            or HttpStatusCode.BadGateway or HttpStatusCode.ServiceUnavailable or HttpStatusCode.GatewayTimeout;

    private static bool IsTransient(Exception exception, CancellationToken cancellationToken) =>
        !cancellationToken.IsCancellationRequested && exception is HttpRequestException or AttemptTimeoutException;

    /// <summary>
    /// Send once, within the attempt timeout, and read the whole body so the timeout covers it too
    /// </summary>
    private async Task<HttpResponseMessage> SendAttemptAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var timeout = _attemptTimeout == Timeout.InfiniteTimeSpan
            ? null
            : new CancellationTokenSource(_attemptTimeout, _timeProvider);
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeout?.Token ?? CancellationToken.None);

        try
        {
            var response = await base.SendAsync(request, linked.Token);

            try
            {
                _ = await response.Content.ReadAsByteArrayAsync(linked.Token); // buffers the body; later reads use the buffer
            }
            catch
            {
                response.Dispose();
                throw;
            }

            return response;
        }
        catch (OperationCanceledException) when (timeout is { IsCancellationRequested: true } && !cancellationToken.IsCancellationRequested)
        {
            throw new AttemptTimeoutException(_attemptTimeout);
        }
    }

    /// <summary>
    /// How long to wait before retry number <paramref name="retry"/> (1-based) of a transient response, or false when
    /// the server asked for a wait longer than <see cref="RetryConfig.MaxDelayMs"/>
    /// </summary>
    private bool TryGetDelay(HttpResponseMessage response, int retry, out TimeSpan delay)
    {
        delay = Backoff(retry);

        if (!TryGetServerDelay(response, out var requested))
        {
            return true;
        }

        if (requested > MaxDelay())
        {
            return false;
        }

        delay = requested > delay ? requested : delay;
        return true;
    }

    private bool TryGetServerDelay(HttpResponseMessage response, out TimeSpan delay)
    {
        delay = TimeSpan.Zero;

        if (response.StatusCode is not (HttpStatusCode.TooManyRequests or HttpStatusCode.ServiceUnavailable))
        {
            return false;
        }

        var retryAfter = response.Headers.RetryAfter;
        if (retryAfter?.Delta is { } delta)
        {
            delay = delta;
            return true;
        }

        if (retryAfter?.Date is { } date)
        {
            delay = date - _timeProvider.GetUtcNow();
            delay = delay < TimeSpan.Zero ? TimeSpan.Zero : delay;
            return true;
        }

        if (response.Headers.TryGetValues("RateLimit-Reset", out var values) &&
            double.TryParse(values.FirstOrDefault(), NumberStyles.Float, CultureInfo.InvariantCulture, out var seconds) &&
            seconds >= 0 && seconds < TimeSpan.MaxValue.TotalSeconds)
        {
            delay = TimeSpan.FromSeconds(seconds);
            return true;
        }

        return false;
    }

    private TimeSpan Backoff(int retry)
    {
        var initial = Math.Max(0, _retryConfig.InitialDelayMs);
        var milliseconds = _retryConfig.UseExponentialBackoff ? initial * Math.Pow(2, retry - 1) : initial;

        return TimeSpan.FromMilliseconds(Math.Min(milliseconds, MaxDelay().TotalMilliseconds));
    }

    private TimeSpan MaxDelay() => TimeSpan.FromMilliseconds(Math.Max(0, _retryConfig.MaxDelayMs));

    private Task DelayAsync(TimeSpan delay, CancellationToken cancellationToken) =>
        delay > TimeSpan.Zero ? Task.Delay(delay, _timeProvider, cancellationToken) : Task.CompletedTask;

    /// <summary>
    /// An attempt that did not finish in time. It is a <see cref="TaskCanceledException"/> caused by a
    /// <see cref="TimeoutException"/>, the shape <see cref="HttpClient"/> itself throws on a timeout.
    /// </summary>
    private sealed class AttemptTimeoutException(TimeSpan timeout)
        : TaskCanceledException($"The request was canceled due to the configured timeout of {timeout.TotalSeconds} seconds elapsing.", new TimeoutException());
}
