using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Internal transfer save request
/// </summary>
public class TransferSaveInternalTransferRequestDto
{
    /// <summary>
    /// Amount to be transferred
    /// </summary>
    [JsonPropertyName("value")]
    public double Value { get; set; }

    /// <summary>
    /// Destination account information
    /// </summary>
    [JsonPropertyName("account")]
    public TransferSaveInternalTransferAccountDto? Account { get; set; }

    /// <summary>
    /// Transfer description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
