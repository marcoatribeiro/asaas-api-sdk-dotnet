namespace Asaas.Sdk.Config;

/// <summary>
/// Retry configuration for HTTP requests
/// </summary>
public class RetryConfig
{
    /// <summary>
    /// Maximum number of retry attempts (default: 3)
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Initial delay between retries in milliseconds (default: 1000)
    /// </summary>
    public int InitialDelayMs { get; set; } = 1000;

    /// <summary>
    /// Maximum delay between retries in milliseconds (default: 10000)
    /// </summary>
    public int MaxDelayMs { get; set; } = 10000;

    /// <summary>
    /// Whether to use exponential backoff (default: true)
    /// </summary>
    public bool UseExponentialBackoff { get; set; } = true;
}
