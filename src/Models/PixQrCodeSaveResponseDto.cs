using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Static Pix QR Code
/// </summary>
public class PixQrCodeSaveResponseDto
{
    /// <summary>
    /// QR Code identifier. The Asaas reference swaps the descriptions of <c>id</c> and <c>encodedImage</c>; check the values you get.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// QR Code image, base64-encoded
    /// </summary>
    [JsonPropertyName("encodedImage")]
    public string? EncodedImage { get; set; }

    /// <summary>
    /// QR Code "copia e cola" payload
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }

    /// <summary>
    /// Whether the QR Code accepts more than one payment
    /// </summary>
    [JsonPropertyName("allowsMultiplePayments")]
    public bool? AllowsMultiplePayments { get; set; }

    /// <summary>
    /// Date and time after which payments are refused
    /// </summary>
    [JsonPropertyName("expirationDate")]
    public string? ExpirationDate { get; set; }

    /// <summary>
    /// Free search field
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// QR Code description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
