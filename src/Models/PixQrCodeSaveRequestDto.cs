using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Static Pix QR Code creation request
/// </summary>
public class PixQrCodeSaveRequestDto
{
    /// <summary>
    /// Pix key that receives the payments
    /// </summary>
    [JsonPropertyName("addressKey")]
    public string? AddressKey { get; set; }

    /// <summary>
    /// QR Code description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Amount. When omitted the payer chooses it.
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Format of the response content: <c>ALL</c>, <c>IMAGE</c> or <c>PAYLOAD</c>
    /// </summary>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>
    /// Date and time after which payments are refused
    /// </summary>
    [JsonPropertyName("expirationDate")]
    public string? ExpirationDate { get; set; }

    /// <summary>
    /// Expiration, in seconds
    /// </summary>
    [JsonPropertyName("expirationSeconds")]
    public int? ExpirationSeconds { get; set; }

    /// <summary>
    /// Whether the QR Code accepts more than one payment (the Asaas default is true)
    /// </summary>
    [JsonPropertyName("allowsMultiplePayments")]
    public bool? AllowsMultiplePayments { get; set; }

    /// <summary>
    /// Free search field (max. 100 characters)
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }
}
