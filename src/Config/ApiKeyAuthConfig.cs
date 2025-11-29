namespace Asaas.Sdk.Config;

/// <summary>
/// API Key authentication configuration
/// </summary>
public class ApiKeyAuthConfig
{
    /// <summary>
    /// The API key for authentication
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The header name for the API key (default: "access_token")
    /// </summary>
    public string ApiKeyHeader { get; set; } = "access_token";
}
