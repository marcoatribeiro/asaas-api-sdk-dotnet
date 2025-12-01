using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Transfer bank information
/// </summary>
public class TransferBankSaveRequestDto
{
    /// <summary>
    /// Bank code
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }
}
