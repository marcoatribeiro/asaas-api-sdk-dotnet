using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// QR Code to pay
/// </summary>
public class PixTransactionQrCodeRequestDto
{
    /// <summary>
    /// QR Code payload (required)
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }

    /// <summary>
    /// Change amount, for a change (troco) QR Code
    /// </summary>
    [JsonPropertyName("changeValue")]
    public decimal? ChangeValue { get; set; }
}
