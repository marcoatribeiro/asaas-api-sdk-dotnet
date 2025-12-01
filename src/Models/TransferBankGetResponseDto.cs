using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Transfer bank information response
/// </summary>
public class TransferBankGetResponseDto
{
    /// <summary>
    /// Bank code
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Bank name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
