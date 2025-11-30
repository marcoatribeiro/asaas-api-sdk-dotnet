using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment PIX QR Code response
/// </summary>
public class PaymentPixQrCodeResponseDto
{
    /// <summary>
    /// Payment identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// QR Code encoded payload
    /// </summary>
    [JsonPropertyName("encodedImage")]
    public string? EncodedImage { get; set; }

    /// <summary>
    /// PIX copy and paste code
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }

    /// <summary>
    /// Allow multiple payments
    /// </summary>
    [JsonPropertyName("allowsMultiplePayments")]
    public bool? AllowsMultiplePayments { get; set; }

    /// <summary>
    /// Expiration date
    /// </summary>
    [JsonPropertyName("expirationDate")]
    public string? ExpirationDate { get; set; }
}
