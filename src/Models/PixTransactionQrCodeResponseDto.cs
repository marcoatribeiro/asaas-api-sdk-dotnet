using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// QR Code of a Pix transaction
/// </summary>
public class PixTransactionQrCodeResponseDto
{
    /// <summary>
    /// Payer
    /// </summary>
    [JsonPropertyName("payer")]
    public PixTransactionQrCodePayerResponseDto? Payer { get; set; }

    /// <summary>
    /// QR Code identifier
    /// </summary>
    [JsonPropertyName("conciliationIdentifier")]
    public string? ConciliationIdentifier { get; set; }

    /// <summary>
    /// Original value
    /// </summary>
    [JsonPropertyName("originalValue")]
    public decimal? OriginalValue { get; set; }

    /// <summary>
    /// Due date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonPropertyName("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Fine
    /// </summary>
    [JsonPropertyName("fine")]
    public decimal? Fine { get; set; }

    /// <summary>
    /// Discount
    /// </summary>
    [JsonPropertyName("discount")]
    public decimal? Discount { get; set; }

    /// <summary>
    /// Expiration date and time
    /// </summary>
    [JsonPropertyName("expirationDate")]
    public string? ExpirationDate { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
