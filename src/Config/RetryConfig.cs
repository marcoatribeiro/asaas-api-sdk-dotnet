namespace Asaas.Sdk.Config;

/// <summary>
/// Retry configuration for HTTP requests. Only requests that are safe to repeat (a <c>GET</c> without a body) are retried;
/// a <c>POST</c>, <c>PUT</c> or <c>DELETE</c>, and every upload, is sent once. See <see cref="Http.RetryHandler"/>.
/// </summary>
public class RetryConfig
{
    /// <summary>
    /// Maximum number of retries after the first attempt (default: 3); 0 sends each request once
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Delay before the first retry in milliseconds (default: 1000); also the fixed delay when backoff is not exponential
    /// </summary>
    public int InitialDelayMs { get; set; } = 1000;

    /// <summary>
    /// Longest wait before a retry in milliseconds (default: 10000). A <c>Retry-After</c> or <c>RateLimit-Reset</c> above it
    /// means the response is returned without retrying
    /// </summary>
    public int MaxDelayMs { get; set; } = 10000;

    /// <summary>
    /// Whether to use exponential backoff (default: true)
    /// </summary>
    public bool UseExponentialBackoff { get; set; } = true;
}
