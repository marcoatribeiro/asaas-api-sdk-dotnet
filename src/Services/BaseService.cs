using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Http;
using Polly;
using Polly.Extensions.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Base service class for all API service implementations
/// </summary>
public abstract class BaseService
{
    protected readonly HttpClient HttpClient;
    protected readonly AsaasSdkConfig Config;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseService"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client instance</param>
    /// <param name="config">SDK configuration</param>
    protected BaseService(HttpClient httpClient, AsaasSdkConfig config)
    {
        HttpClient = httpClient;
        Config = config;
    }

    /// <summary>
    /// Set the base URL for API requests
    /// </summary>
    /// <param name="baseUrl">Base URL</param>
    public void SetBaseUrl(string baseUrl)
    {
        Config.BaseUrl = baseUrl;
    }

    /// <summary>
    /// Set the environment for API requests
    /// </summary>
    /// <param name="environment">Environment</param>
    public void SetEnvironment(Http.Environment environment)
    {
        Config.SetEnvironment(environment);
    }

    /// <summary>
    /// Execute an HTTP request
    /// </summary>
    /// <param name="request">HTTP request message</param>
    /// <returns>HTTP response message</returns>
    /// <exception cref="ApiException">Thrown when the request fails</exception>
    protected async Task<HttpResponseMessage> ExecuteAsync(HttpRequestMessage request)
    {
        try
        {
            var response = await HttpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            // Handle error response
            var content = await response.Content.ReadAsStringAsync();
            var message = ExtractErrorMessage(response, content);
            
            throw new ApiException(message, (int)response.StatusCode, content);
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException($"HTTP request failed: {ex.Message}", 0, null, ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new ApiException("Request timed out", 408, null, ex);
        }
    }

    /// <summary>
    /// Execute an HTTP request and convert response to model
    /// </summary>
    /// <typeparam name="T">Type of model to return</typeparam>
    /// <param name="request">HTTP request message</param>
    /// <returns>Deserialized model object</returns>
    protected async Task<T> ExecuteAsync<T>(HttpRequestMessage request)
    {
        var response = await ExecuteAsync(request);
        return await ModelConverter.ConvertAsync<T>(response);
    }

    /// <summary>
    /// Extract error message from response
    /// </summary>
    /// <param name="response">HTTP response</param>
    /// <param name="content">Response content</param>
    /// <returns>Error message string</returns>
    private static string ExtractErrorMessage(HttpResponseMessage response, string? content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            try
            {
                // Try to extract message from JSON response
                using var doc = System.Text.Json.JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("message", out var messageElement))
                {
                    return messageElement.GetString() ?? response.ReasonPhrase ?? "Unknown error";
                }
                if (doc.RootElement.TryGetProperty("error", out var errorElement))
                {
                    return errorElement.GetString() ?? response.ReasonPhrase ?? "Unknown error";
                }
            }
            catch
            {
                // If JSON parsing fails, continue to default message
            }
        }

        return response.ReasonPhrase ?? $"{(int)response.StatusCode} error in request to: {response.RequestMessage?.RequestUri}";
    }

    /// <summary>
    /// Build the base URL for requests
    /// </summary>
    /// <returns>Base URL string</returns>
    protected string GetBaseUrl()
    {
        return Config.BaseUrl ?? Http.Environment.Default.GetUrl();
    }

    /// <summary>
    /// Create a retry policy for HTTP requests
    /// </summary>
    /// <returns>Retry policy</returns>
    protected static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(RetryConfig config)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => (int)msg.StatusCode == 429) // Too Many Requests
            .WaitAndRetryAsync(
                config.MaxRetries,
                retryAttempt => config.UseExponentialBackoff
                    ? TimeSpan.FromMilliseconds(Math.Min(
                        config.InitialDelayMs * Math.Pow(2, retryAttempt - 1),
                        config.MaxDelayMs))
                    : TimeSpan.FromMilliseconds(config.InitialDelayMs)
            );
    }
}
