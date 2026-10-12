using System.Net;
using System.Net.Sockets;
using System.Text;
using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;
using Asaas.Sdk.Services;

namespace Asaas.Sdk.Tests.Services;

/// <summary>
/// RetryConfig must take effect, and only for requests that are safe to repeat (issue #10).
/// </summary>
public sealed class RetryHandlerTests
{
    private static readonly TimeSpan AttemptTimeout = TimeSpan.FromMilliseconds(12345);
    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    private static RetryConfig Config(int maxRetries = 2, int initial = 1000, int max = 10000, bool exponential = true) => new()
    {
        MaxRetries = maxRetries, InitialDelayMs = initial, MaxDelayMs = max, UseExponentialBackoff = exponential
    };

    private sealed class Rig
    {
        public required ScriptedHandler Inner { get; init; }
        public required TestTimeProvider Time { get; init; }
        public required HttpClient Client { get; init; }
        public AsaasSdkConfig Sdk { get; } = new() { BaseUrl = "https://api.example.invalid" };
        public FinanceService Finance => new(Client, Sdk);
    }

    private static Rig Build(RetryConfig retry, Func<TimeSpan, bool>? autoFire = null, TimeSpan? timeout = null, params Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>[] script)
    {
        var time = new TestTimeProvider(autoFire ?? (due => due != AttemptTimeout));
        var inner = new ScriptedHandler(script);
        var client = new HttpClient(new RetryHandler(retry, timeout ?? AttemptTimeout, time) { InnerHandler = inner });
        return new Rig { Inner = inner, Time = time, Client = client };
    }

    private static Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> Respond(
        HttpStatusCode status, string body = "{}", Action<HttpResponseMessage>? configure = null) => (_, _) =>
    {
        var response = new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
        configure?.Invoke(response);
        return Task.FromResult(response);
    };

    private static Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> Throw(Exception exception) =>
        (_, _) => Task.FromException<HttpResponseMessage>(exception);

    [Fact]
    public async Task Get503_ShouldBeSentOncePlusMaxRetries_ThenSurfaceTheError()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 2), script: Respond(HttpStatusCode.ServiceUnavailable));

        // Act
        var exception = await Assert.ThrowsAsync<ApiException>(() => rig.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal(503, exception.StatusCode);
        Assert.Equal(3, rig.Inner.Attempts);
    }

    [Fact]
    public async Task MaxRetriesZero_ShouldSendOnce()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 0), script: Respond(HttpStatusCode.ServiceUnavailable));

        // Act
        await Assert.ThrowsAsync<ApiException>(() => rig.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal(1, rig.Inner.Attempts);
        Assert.Empty(rig.Time.Delays);
    }

    [Fact]
    public async Task Get_ShouldSucceed_WhenALaterAttemptDoes()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 3),
            script: [Respond(HttpStatusCode.BadGateway), Respond(HttpStatusCode.TooManyRequests), Respond(HttpStatusCode.OK, """{ "balance": 12.5 }""")]);

        // Act
        var balance = await rig.Finance.GetBalanceAsync(Ct);

        // Assert
        Assert.Equal(3, rig.Inner.Attempts);
        Assert.Equal(12.5, balance.Balance);
    }

    [Theory]
    [InlineData(HttpStatusCode.RequestTimeout, 3)]
    [InlineData(HttpStatusCode.TooManyRequests, 3)]
    [InlineData(HttpStatusCode.InternalServerError, 3)]
    [InlineData(HttpStatusCode.BadGateway, 3)]
    [InlineData(HttpStatusCode.ServiceUnavailable, 3)]
    [InlineData(HttpStatusCode.GatewayTimeout, 3)]
    [InlineData(HttpStatusCode.BadRequest, 1)]
    [InlineData(HttpStatusCode.Unauthorized, 1)]
    [InlineData(HttpStatusCode.Forbidden, 1)]
    [InlineData(HttpStatusCode.NotFound, 1)]
    [InlineData(HttpStatusCode.NotImplemented, 1)]
    public async Task Get_ShouldRetryOnlyTransientStatuses(HttpStatusCode status, int expectedAttempts)
    {
        // Arrange
        var rig = Build(Config(maxRetries: 2), script: Respond(status));

        // Act
        await Assert.ThrowsAsync<ApiException>(() => rig.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal(expectedAttempts, rig.Inner.Attempts);
    }

    [Fact]
    public async Task Get_ShouldRetryAfterAConnectionFailure()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 2),
            script: [Throw(new HttpRequestException("connection reset")), Throw(new HttpRequestException("connection reset")), Respond(HttpStatusCode.OK, """{ "balance": 1 }""")]);

        // Act
        var balance = await rig.Finance.GetBalanceAsync(Ct);

        // Assert
        Assert.Equal(3, rig.Inner.Attempts);
        Assert.Equal(1, balance.Balance);
    }

    [Fact]
    public async Task Get_ShouldSurfaceTheLastConnectionFailure_WhenRetriesRunOut()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 1), script: Throw(new HttpRequestException("connection reset")));

        // Act
        var exception = await Assert.ThrowsAsync<ApiException>(() => rig.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal(0, exception.StatusCode);
        Assert.Equal(2, rig.Inner.Attempts);
    }

    [Fact]
    public async Task Exceptions_ShouldNotBeRetried_WhenNotTransient()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 3), script: Throw(new InvalidOperationException("boom")));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => rig.Finance.GetBalanceAsync(Ct));
        Assert.Equal(1, rig.Inner.Attempts);
    }

    [Fact]
    public async Task ExponentialBackoff_ShouldDoubleTheDelay_UpToTheCap()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 4, initial: 1000, max: 3000, exponential: true), script: Respond(HttpStatusCode.ServiceUnavailable));

        // Act
        await Assert.ThrowsAsync<ApiException>(() => rig.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal([1000, 2000, 3000, 3000], rig.Time.Delays.Select(delay => (int)delay.TotalMilliseconds));
    }

    [Fact]
    public async Task FixedBackoff_ShouldRepeatTheInitialDelay_CappedAtTheMaximum()
    {
        // Arrange
        var fixedDelay = Build(Config(maxRetries: 3, initial: 500, max: 10000, exponential: false), script: Respond(HttpStatusCode.ServiceUnavailable));
        var capped = Build(Config(maxRetries: 2, initial: 5000, max: 2000, exponential: false), script: Respond(HttpStatusCode.ServiceUnavailable));

        // Act
        await Assert.ThrowsAsync<ApiException>(() => fixedDelay.Finance.GetBalanceAsync(Ct));
        await Assert.ThrowsAsync<ApiException>(() => capped.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal([500, 500, 500], fixedDelay.Time.Delays.Select(delay => (int)delay.TotalMilliseconds));
        Assert.Equal([2000, 2000], capped.Time.Delays.Select(delay => (int)delay.TotalMilliseconds));
    }

    [Fact]
    public async Task RetryAfterSeconds_ShouldBeHonoured_WhenLongerThanTheBackoff()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 1, initial: 1000, max: 30000),
            script: [Respond(HttpStatusCode.TooManyRequests, configure: r => r.Headers.RetryAfter = new(TimeSpan.FromSeconds(7))), Respond(HttpStatusCode.OK, """{ "balance": 1 }""")]);

        // Act
        await rig.Finance.GetBalanceAsync(Ct);

        // Assert
        Assert.Equal([7000], rig.Time.Delays.Select(delay => (int)delay.TotalMilliseconds));
    }

    [Fact]
    public async Task RetryAfterDate_ShouldBeConvertedToADelay()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 1, initial: 100, max: 30000), script: [
            Respond(HttpStatusCode.ServiceUnavailable, configure: r => r.Headers.RetryAfter = new(TestTimeProvider.Now.AddSeconds(5))),
            Respond(HttpStatusCode.OK, """{ "balance": 1 }""")]);

        // Act
        await rig.Finance.GetBalanceAsync(Ct);

        // Assert
        Assert.Equal([5000], rig.Time.Delays.Select(delay => (int)delay.TotalMilliseconds));
    }

    [Fact]
    public async Task RateLimitReset_ShouldBeHonoured_WhenThereIsNoRetryAfter()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 1, initial: 1000, max: 30000), script: [
            Respond(HttpStatusCode.TooManyRequests, configure: r => r.Headers.Add("RateLimit-Reset", "4")),
            Respond(HttpStatusCode.OK, """{ "balance": 1 }""")]);

        // Act
        await rig.Finance.GetBalanceAsync(Ct);

        // Assert
        Assert.Equal([4000], rig.Time.Delays.Select(delay => (int)delay.TotalMilliseconds));
    }

    [Theory]
    [InlineData("Retry-After", "43200")]
    [InlineData("RateLimit-Reset", "43200")]
    [InlineData("RateLimit-Reset", "1700000000")]
    public async Task A429ThatAsksForALongWait_ShouldNotBeRetried(string header, string value)
    {
        // Arrange: a wait beyond MaxDelayMs is a quota or block, not a short transient limit
        var rig = Build(Config(maxRetries: 3, initial: 1000, max: 10000),
            script: Respond(HttpStatusCode.TooManyRequests, configure: r => r.Headers.TryAddWithoutValidation(header, value)));

        // Act
        var exception = await Assert.ThrowsAsync<ApiException>(() => rig.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal(429, exception.StatusCode);
        Assert.Equal(1, rig.Inner.Attempts);
        Assert.Empty(rig.Time.Delays);
    }

    [Theory]
    [InlineData("not a number")]
    [InlineData("-5")]
    [InlineData("")]
    public async Task UnusableRateLimitHeaders_ShouldFallBackToTheBackoff(string value)
    {
        // Arrange
        var rig = Build(Config(maxRetries: 1, initial: 1000), script: [
            Respond(HttpStatusCode.TooManyRequests, configure: r => r.Headers.TryAddWithoutValidation("RateLimit-Reset", value)),
            Respond(HttpStatusCode.OK, """{ "balance": 1 }""")]);

        // Act
        await rig.Finance.GetBalanceAsync(Ct);

        // Assert
        Assert.Equal([1000], rig.Time.Delays.Select(delay => (int)delay.TotalMilliseconds));
    }

    [Fact]
    public async Task NonGetRequests_ShouldNeverBeRepeated_EvenAfterA429Or5xxOrAFailure()
    {
        // Arrange
        var failures = new Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>[]
        {
            Respond(HttpStatusCode.TooManyRequests), Respond(HttpStatusCode.ServiceUnavailable), Respond(HttpStatusCode.InternalServerError),
            Throw(new HttpRequestException("reset")),
        };

        foreach (var failure in failures)
        {
            var rig = Build(Config(maxRetries: 3), script: failure);
            var pix = new PixTransactionService(rig.Client, rig.Sdk);
            var customers = new CustomerService(rig.Client, rig.Sdk);
            var subscriptions = new SubscriptionService(rig.Client, rig.Sdk);
            var keys = new PixService(rig.Client, rig.Sdk);

            // Act: a POST that moves money, a DELETE, a PUT and another POST
            await Assert.ThrowsAsync<ApiException>(() => pix.PayQrCodeAsync(new PixTransactionSaveRequestDto { Value = 1 }, Ct));
            await Assert.ThrowsAsync<ApiException>(() => customers.RemoveCustomerAsync("cus_1", Ct));
            await Assert.ThrowsAsync<ApiException>(() => subscriptions.UpdateSubscriptionAsync("sub_1", new SubscriptionUpdateRequestDto(), Ct));
            await Assert.ThrowsAsync<ApiException>(() => keys.CreateKeyAsync(new PixAddressKeySaveRequestDto { Type = "EVP" }, Ct));

            // Assert
            Assert.Equal(4, rig.Inner.Attempts);
            Assert.Empty(rig.Time.Delays);
        }
    }

    [Fact]
    public async Task MultipartUpload_ShouldNeverBeRepeated_AndKeepItsStreamOwnership()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 3), script: Respond(HttpStatusCode.ServiceUnavailable));
        var stream = new TrackedStream([1, 2, 3]);

        // Act
        await Assert.ThrowsAsync<ApiException>(() => new PaymentLinkService(rig.Client, rig.Sdk)
            .AddPaymentLinkImageAsync("link_1", stream, "a.png", "image/png", cancellationToken: Ct));

        // Assert
        Assert.Equal(1, rig.Inner.Attempts);
        Assert.True(stream.Disposed);
    }

    [Fact]
    public async Task GetWithABody_ShouldNotBeRepeated_SinceItsContentCannotBeAssumedReplayable()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 3), script: Respond(HttpStatusCode.ServiceUnavailable));
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.invalid/v3/x") { Content = new StringContent("{}") };
        using var invoker = new HttpMessageInvoker(new RetryHandler(Config(maxRetries: 3), AttemptTimeout, rig.Time) { InnerHandler = rig.Inner });

        // Act
        using var response = await invoker.SendAsync(request, Ct);

        // Assert
        Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        Assert.Equal(1, rig.Inner.Attempts);
    }

    [Fact]
    public async Task Cancellation_ShouldInterruptTheWait_WithoutAnotherAttempt()
    {
        // Arrange: nothing fires on its own, so the call sits in the backoff wait
        var rig = Build(Config(maxRetries: 3), autoFire: _ => false, script: Respond(HttpStatusCode.ServiceUnavailable));
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(Ct);

        // Act
        var call = rig.Finance.GetBalanceAsync(cts.Token);
        await rig.Time.WaitForPendingAsync(TimeSpan.FromMilliseconds(1000));
        await cts.CancelAsync();

        // Assert
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => call);
        Assert.Equal(1, rig.Inner.Attempts);
    }

    [Fact]
    public async Task ACancelledToken_ShouldNotBeMistakenForATimeout_OrRetried()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 3), script: async (_, token) =>
        {
            await Task.Delay(Timeout.Infinite, token);
            return new HttpResponseMessage();
        });
        using var cts = new CancellationTokenSource();

        // Act
        var call = rig.Finance.GetBalanceAsync(cts.Token);
        await cts.CancelAsync();

        // Assert
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => call);
        Assert.Equal(1, rig.Inner.Attempts);
    }

    [Fact]
    public async Task AGetThatTimesOut_ShouldBeRetried_WithAFreshTimeoutEachAttempt()
    {
        // Arrange: attempt 1 hangs until its own timeout fires, attempt 2 answers
        var rig = Build(Config(maxRetries: 2), script: [
            async (_, token) => { await Task.Delay(Timeout.Infinite, token); return new HttpResponseMessage(); },
            Respond(HttpStatusCode.OK, """{ "balance": 3 }""")]);

        // Act
        var call = rig.Finance.GetBalanceAsync(Ct);
        await rig.Time.WaitForPendingAsync(AttemptTimeout);
        rig.Time.FirePending(AttemptTimeout);
        var balance = await call;

        // Assert
        Assert.Equal(3, balance.Balance);
        Assert.Equal(2, rig.Inner.Attempts);
        Assert.Equal(2, rig.Time.TimersCreated(AttemptTimeout));
    }

    [Fact]
    public async Task APostThatTimesOut_ShouldBeSentOnce_AndReportATimeout()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 3), script: async (_, token) => { await Task.Delay(Timeout.Infinite, token); return new HttpResponseMessage(); });
        var pix = new PixTransactionService(rig.Client, rig.Sdk);

        // Act
        var call = pix.PayQrCodeAsync(new PixTransactionSaveRequestDto { Value = 1 }, Ct);
        await rig.Time.WaitForPendingAsync(AttemptTimeout);
        rig.Time.FirePending(AttemptTimeout);

        // Assert
        var exception = await Assert.ThrowsAsync<ApiException>(() => call);
        Assert.Equal(408, exception.StatusCode);
        Assert.Equal("Request timed out", exception.Message);
        Assert.Equal(1, rig.Inner.Attempts);
    }

    [Fact]
    public async Task AGetThatAlwaysTimesOut_ShouldStopAtMaxRetries()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 1), script: async (_, token) => { await Task.Delay(Timeout.Infinite, token); return new HttpResponseMessage(); });

        // Act
        var call = rig.Finance.GetBalanceAsync(Ct);
        for (var attempt = 1; attempt <= 2; attempt++)
        {
            await rig.Time.WaitForPendingAsync(AttemptTimeout, attempt);
            rig.Time.FirePending(AttemptTimeout);
        }

        // Assert
        var exception = await Assert.ThrowsAsync<ApiException>(() => call);
        Assert.Equal(408, exception.StatusCode);
        Assert.Equal(2, rig.Inner.Attempts);
    }

    [Fact]
    public async Task InfiniteTimeout_ShouldCreateNoTimer()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 0), timeout: Timeout.InfiniteTimeSpan, script: Respond(HttpStatusCode.OK, """{ "balance": 2 }"""));

        // Act
        var balance = await rig.Finance.GetBalanceAsync(Ct);

        // Assert
        Assert.Equal(2, balance.Balance);
        Assert.Equal(0, rig.Time.TimersCreated(AttemptTimeout));
    }

    [Fact]
    public async Task IntermediateResponses_ShouldBeDisposed_AndTheLastOneKept()
    {
        // Arrange
        var contents = new List<DisposeTrackingContent>();
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> Tracked(HttpStatusCode status) => (_, _) =>
        {
            var content = new DisposeTrackingContent();
            contents.Add(content);
            return Task.FromResult(new HttpResponseMessage(status) { Content = content });
        };

        var rig = Build(Config(maxRetries: 2), script: [Tracked(HttpStatusCode.ServiceUnavailable), Tracked(HttpStatusCode.BadGateway), Tracked(HttpStatusCode.OK)]);
        using var invoker = new HttpMessageInvoker(new RetryHandler(Config(maxRetries: 2), AttemptTimeout, rig.Time) { InnerHandler = rig.Inner });
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.invalid/v3/x");

        // Act
        using var response = await invoker.SendAsync(request, Ct);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal([true, true, false], contents.Select(content => content.Disposed));
    }

    [Fact]
    public async Task RetriedGets_ShouldKeepTheirMethodUriAndHeaders()
    {
        // Arrange
        var rig = Build(Config(maxRetries: 2), script: [Respond(HttpStatusCode.ServiceUnavailable), Respond(HttpStatusCode.OK, """{ "balance": 1 }""")]);
        using var sdk = new HttpClient(new RetryHandler(Config(maxRetries: 2), AttemptTimeout, rig.Time)
        {
            InnerHandler = rig.Inner
        });
        sdk.DefaultRequestHeaders.Add("access_token", "key");

        // Act
        await new FinanceService(sdk, rig.Sdk).GetBalanceAsync(Ct);

        // Assert
        Assert.All(rig.Inner.Requests, request =>
        {
            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("/v3/finance/balance", request.Path);
            Assert.Equal("key", request.AccessToken);
        });
        Assert.Equal(2, rig.Inner.Requests.Count);
    }

    [Fact]
    public void ARetryHandlerWithoutAConfig_ShouldBeRejected()
    {
        Assert.Throws<ArgumentNullException>(() => new RetryHandler(null!, AttemptTimeout));
    }

    // ---- through the real SDK pipeline, against a loopback server

    [Theory]
    [InlineData(2, 3)]
    [InlineData(0, 1)]
    public async Task Sdk_ShouldApplyRetryConfig_ToAGetThroughTheRealPipeline(int maxRetries, int expectedAttempts)
    {
        // Arrange
        await using var server = new LoopbackServer();
        using var sdk = new AsaasSdk(new AsaasSdkConfig
        {
            BaseUrl = server.BaseUrl,
            RetryConfig = new RetryConfig { MaxRetries = maxRetries, InitialDelayMs = 1, MaxDelayMs = 1, UseExponentialBackoff = false }
        });

        // Act
        var exception = await Assert.ThrowsAsync<ApiException>(() => sdk.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal(503, exception.StatusCode);
        Assert.Equal(expectedAttempts, server.Attempts);
    }

    [Fact]
    public async Task Sdk_ShouldNotRepeatAPost_ThroughTheRealPipeline()
    {
        // Arrange
        await using var server = new LoopbackServer();
        using var sdk = new AsaasSdk(new AsaasSdkConfig
        {
            BaseUrl = server.BaseUrl,
            RetryConfig = new RetryConfig { MaxRetries = 3, InitialDelayMs = 1, MaxDelayMs = 1 }
        });

        // Act
        var exception = await Assert.ThrowsAsync<ApiException>(() =>
            sdk.PixTransaction.PayQrCodeAsync(new PixTransactionSaveRequestDto { Value = 1, QrCode = new PixTransactionQrCodeRequestDto { Payload = "000201" } }, Ct));

        // Assert
        Assert.Equal(503, exception.StatusCode);
        Assert.Equal(1, server.Attempts);
    }

    [Fact]
    public async Task Sdk_ShouldKeepTheAuthHeaders_OnEveryRetriedAttempt()
    {
        // Arrange
        await using var server = new LoopbackServer();
        using var sdk = new AsaasSdk(new AsaasSdkConfig
        {
            BaseUrl = server.BaseUrl,
            ApiKeyAuthConfig = new ApiKeyAuthConfig { ApiKey = "secret-key" },
            RetryConfig = new RetryConfig { MaxRetries = 2, InitialDelayMs = 1, MaxDelayMs = 1 }
        });

        // Act
        await Assert.ThrowsAsync<ApiException>(() => sdk.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal(3, server.RequestHeaders.Count);
        Assert.All(server.RequestHeaders, headers => Assert.Contains("access_token: secret-key", headers));
    }

    [Fact]
    public async Task Sdk_ShouldTimeOutEachAttempt_NotTheWholeCall()
    {
        // Arrange: the server never answers; each of the 2 attempts waits its own 300 ms
        await using var server = new LoopbackServer(respond: false);
        using var sdk = new AsaasSdk(new AsaasSdkConfig
        {
            BaseUrl = server.BaseUrl,
            Timeout = TimeSpan.FromMilliseconds(300),
            RetryConfig = new RetryConfig { MaxRetries = 1, InitialDelayMs = 1, MaxDelayMs = 1 }
        });

        // Act
        var exception = await Assert.ThrowsAsync<ApiException>(() => sdk.Finance.GetBalanceAsync(Ct));

        // Assert
        Assert.Equal(408, exception.StatusCode);
        Assert.Equal(2, server.Attempts);
    }

    // ---- test doubles

    private sealed class ScriptedHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>[] script) : HttpMessageHandler
    {
        private int _attempts;

        public int Attempts => Volatile.Read(ref _attempts);
        public List<(HttpMethod Method, string Path, string? AccessToken)> RequestLog { get; } = [];
        public IReadOnlyList<(HttpMethod Method, string Path, string? AccessToken)> Requests => RequestLog;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var index = Interlocked.Increment(ref _attempts) - 1;
            lock (RequestLog)
            {
                RequestLog.Add((request.Method, request.RequestUri!.AbsolutePath,
                    request.Headers.TryGetValues("access_token", out var values) ? values.FirstOrDefault() : null));
            }

            return script[Math.Min(index, script.Length - 1)](request, cancellationToken);
        }
    }

    private sealed class DisposeTrackingContent : HttpContent
    {
        public bool Disposed { get; private set; }

        protected override Task SerializeToStreamAsync(Stream stream, System.Net.TransportContext? context) => Task.CompletedTask;

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            Disposed = true;
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// A clock whose timers either fire at once (the waits) or only when the test says so (the attempt timeouts, the
    /// cancellation case), and that records the delays that were asked for
    /// </summary>
    private sealed class TestTimeProvider(Func<TimeSpan, bool> autoFire) : TimeProvider
    {
        public static readonly DateTimeOffset Now = new(2024, 5, 1, 12, 0, 0, TimeSpan.Zero);

        private readonly List<TestTimer> _timers = [];

        public List<TimeSpan> Delays { get; } = [];

        public override DateTimeOffset GetUtcNow() => Now;

        public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
        {
            var timer = new TestTimer(callback, state, dueTime);
            lock (_timers)
            {
                _timers.Add(timer);
            }

            if (dueTime != Timeout.InfiniteTimeSpan && autoFire(dueTime))
            {
                lock (Delays)
                {
                    Delays.Add(dueTime);
                }

                _ = Task.Run(timer.Fire);
            }

            return timer;
        }

        public int TimersCreated(TimeSpan dueTime)
        {
            lock (_timers)
            {
                return _timers.Count(timer => timer.DueTime == dueTime);
            }
        }

        public async Task WaitForPendingAsync(TimeSpan dueTime, int timersCreated = 1)
        {
            for (var tries = 0; tries < 500; tries++)
            {
                lock (_timers)
                {
                    if (_timers.Count(timer => timer.DueTime == dueTime) >= timersCreated)
                    {
                        return;
                    }
                }

                await Task.Delay(10);
            }

            throw new TimeoutException($"No timer of {dueTime} was created.");
        }

        public void FirePending(TimeSpan dueTime)
        {
            List<TestTimer> pending;
            lock (_timers)
            {
                pending = _timers.Where(timer => timer.DueTime == dueTime && !timer.Fired && !timer.Disposed).ToList();
            }

            pending.ForEach(timer => timer.Fire());
        }
    }

    private sealed class TestTimer(TimerCallback callback, object? state, TimeSpan dueTime) : ITimer
    {
        public TimeSpan DueTime { get; } = dueTime;
        public bool Fired { get; private set; }
        public bool Disposed { get; private set; }

        public void Fire()
        {
            if (Fired || Disposed)
            {
                return;
            }

            Fired = true;
            callback(state);
        }

        public bool Change(TimeSpan dueTime, TimeSpan period) => true;

        public void Dispose() => Disposed = true;

        public ValueTask DisposeAsync()
        {
            Disposed = true;
            return ValueTask.CompletedTask;
        }
    }

    /// <summary>
    /// A loopback-only server that answers every request with 503 (or never answers), counting them
    /// </summary>
    private sealed class LoopbackServer : IAsyncDisposable
    {
        private readonly TcpListener _listener = new(IPAddress.Loopback, 0);
        private readonly CancellationTokenSource _stop = new();
        private readonly Task _loop;
        private readonly bool _respond;
        private int _attempts;

        public LoopbackServer(bool respond = true)
        {
            _respond = respond;
            _listener.Start();
            BaseUrl = $"http://127.0.0.1:{((IPEndPoint)_listener.LocalEndpoint).Port}";
            _loop = Task.Run(Serve);
        }

        public string BaseUrl { get; }
        public int Attempts => Volatile.Read(ref _attempts);
        public List<string> RequestHeaders { get; } = [];

        private async Task Serve()
        {
            try
            {
                while (true)
                {
                    var connection = await _listener.AcceptTcpClientAsync(_stop.Token);
                    _ = Task.Run(() => Handle(connection));
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private async Task Handle(TcpClient connection)
        {
            using var _ = connection;
            try
            {
                using var stream = connection.GetStream();
                using var reader = new StreamReader(stream, Encoding.ASCII, leaveOpen: true);
                var headers = new StringBuilder();
                var contentLength = 0;
                string? line;
                while (!string.IsNullOrEmpty(line = await reader.ReadLineAsync(_stop.Token)))
                {
                    headers.AppendLine(line);
                    if (line.StartsWith("Content-Length:", StringComparison.OrdinalIgnoreCase))
                    {
                        contentLength = int.Parse(line["Content-Length:".Length..].Trim());
                    }
                }

                var body = new char[contentLength];
                var read = 0;
                while (read < contentLength)
                {
                    var count = await reader.ReadAsync(body.AsMemory(read), _stop.Token);
                    if (count == 0) break;
                    read += count;
                }

                Interlocked.Increment(ref _attempts);
                lock (RequestHeaders)
                {
                    RequestHeaders.Add(headers.ToString());
                }

                if (!_respond)
                {
                    await Task.Delay(Timeout.Infinite, _stop.Token);
                }

                var bytes = Encoding.ASCII.GetBytes("HTTP/1.1 503 Service Unavailable\r\nContent-Length: 0\r\nConnection: close\r\n\r\n");
                await stream.WriteAsync(bytes, _stop.Token);
            }
            catch (Exception exception) when (exception is OperationCanceledException or IOException or ObjectDisposedException)
            {
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _stop.CancelAsync();
            _listener.Stop();
            await _loop;
            _stop.Dispose();
        }
    }
}
