namespace Asaas.Sdk.Config;

/// <summary>
/// Main configuration for Asaas SDK
/// </summary>
public class AsaasSdkConfig
{
    /// <summary>
    /// User agent string for HTTP requests
    /// </summary>
    public string UserAgent { get; set; } = "asaas-sdk-dotnet/1.1.5";

    /// <summary>
    /// Base URL for API requests
    /// </summary>
    public string? BaseUrl { get; set; }

    /// <summary>
    /// Retry configuration for failed requests
    /// </summary>
    public RetryConfig RetryConfig { get; set; } = new();

    /// <summary>
    /// API key authentication configuration
    /// </summary>
    public ApiKeyAuthConfig ApiKeyAuthConfig { get; set; } = new();

    /// <summary>
    /// Timeout for HTTP requests (default: 10 seconds)
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Set the environment for the SDK
    /// </summary>
    /// <param name="environment">The environment to use</param>
    public void SetEnvironment(Asaas.Sdk.Http.Environment environment)
    {
        BaseUrl = Http.EnvironmentExtensions.GetUrl(environment);
    }
}
