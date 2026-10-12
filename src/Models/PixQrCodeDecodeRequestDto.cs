using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Request to decode a Pix QR Code
/// </summary>
public class PixQrCodeDecodeRequestDto
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

    /// <summary>
    /// Reference date to compute the updated amount, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("expectedPaymentDate")]
    public string? ExpectedPaymentDate { get; set; }
}
