using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Decoded Pix QR Code
/// </summary>
public class PixQrCodeDecodeResponseDto
{
    /// <summary>
    /// QR Code "copia e cola" payload
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }

    /// <summary>
    /// QR Code type: <c>STATIC</c>, <c>DYNAMIC</c>, <c>DYNAMIC_WITH_ASAAS_ADDRESS_KEY</c> or <c>COMPOSITE</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Origin: <c>MANUAL</c>, <c>ADDRESS_KEY</c>, <c>STATIC_QRCODE</c>, <c>DYNAMIC_QRCODE</c>, <c>PAYMENT_INITIATION_SERVICE</c> or <c>AUTOMATIC_RECURRING</c>
    /// </summary>
    [JsonPropertyName("transactionOriginType")]
    public string? TransactionOriginType { get; set; }

    /// <summary>
    /// Pix key
    /// </summary>
    [JsonPropertyName("pixKey")]
    public string? PixKey { get; set; }

    /// <summary>
    /// QR Code identifier
    /// </summary>
    [JsonPropertyName("conciliationIdentifier")]
    public string? ConciliationIdentifier { get; set; }

    /// <summary>
    /// Due date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Expiration date and time
    /// </summary>
    [JsonPropertyName("expirationDate")]
    public string? ExpirationDate { get; set; }

    /// <summary>
    /// Finality: <c>WITHDRAWAL</c> or <c>CHANGE</c>
    /// </summary>
    [JsonPropertyName("finality")]
    public string? Finality { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Change value
    /// </summary>
    [JsonPropertyName("changeValue")]
    public decimal? ChangeValue { get; set; }

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
    /// Total value
    /// </summary>
    [JsonPropertyName("totalValue")]
    public decimal? TotalValue { get; set; }

    /// <summary>
    /// Whether it can be paid with a different value
    /// </summary>
    [JsonPropertyName("canBePaidWithDifferentValue")]
    public bool? CanBePaidWithDifferentValue { get; set; }

    /// <summary>
    /// Whether the change value can be modified
    /// </summary>
    [JsonPropertyName("canBeModifyChangeValue")]
    public bool? CanBeModifyChangeValue { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Whether the QR Code can be paid
    /// </summary>
    [JsonPropertyName("canBePaid")]
    public bool? CanBePaid { get; set; }

    /// <summary>
    /// Why it cannot be paid
    /// </summary>
    [JsonPropertyName("cannotBePaidReason")]
    public string? CannotBePaidReason { get; set; }

    /// <summary>
    /// Receiver
    /// </summary>
    [JsonPropertyName("receiver")]
    public PixQrCodeDecodeReceiverDto? Receiver { get; set; }

    /// <summary>
    /// Payer
    /// </summary>
    [JsonPropertyName("payer")]
    public PixTransactionQrCodePayerResponseDto? Payer { get; set; }
}
