namespace Asaas.Sdk.Http;

/// <summary>
/// Formats query string values the way the Asaas expects them
/// </summary>
internal static class QueryFormat
{
    /// <summary>
    /// Lowercase <c>true</c>/<c>false</c>, or null (the parameter is then left out) when there is no value. A bare
    /// <c>ToString()</c> would send <c>True</c>/<c>False</c>.
    /// </summary>
    public static string? Bool(bool? value) => value.HasValue ? (value.Value ? "true" : "false") : null;
}
