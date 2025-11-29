namespace Asaas.Sdk.Http;

/// <summary>
/// Available environments for the Asaas API
/// </summary>
public enum Environment
{
    /// <summary>
    /// Default production environment
    /// </summary>
    Default,
    
    /// <summary>
    /// Production environment
    /// </summary>
    Production,
    
    /// <summary>
    /// Sandbox environment for testing
    /// </summary>
    Sandbox
}

/// <summary>
/// Extension methods for Environment enum
/// </summary>
public static class EnvironmentExtensions
{
    /// <summary>
    /// Get the base URL for the specified environment
    /// </summary>
    /// <param name="environment">The environment</param>
    /// <returns>The base URL string</returns>
    public static string GetUrl(this Environment environment)
    {
        return environment switch
        {
            Environment.Default => "https://api.asaas.com/",
            Environment.Production => "https://api.asaas.com/",
            Environment.Sandbox => "https://api-sandbox.asaas.com/",
            _ => "https://api.asaas.com/"
        };
    }
}
