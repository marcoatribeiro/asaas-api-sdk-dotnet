using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account number information
/// </summary>
public class AccountNumberDto
{
    /// <summary>
    /// Agency (branch) number
    /// </summary>
    [JsonPropertyName("agency")]
    public string? Agency { get; set; }

    /// <summary>
    /// Account number
    /// </summary>
    [JsonPropertyName("account")]
    public string? Account { get; set; }

    /// <summary>
    /// Account digit
    /// </summary>
    [JsonPropertyName("accountDigit")]
    public string? AccountDigit { get; set; }
}
