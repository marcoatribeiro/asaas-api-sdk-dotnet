using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Refund of an installment payment
/// </summary>
public class InstallmentRefundResponseDto
{
    /// <summary>
    /// Creation date and time
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Refund status (e.g. <c>PENDING</c>, <c>CANCELLED</c>, <c>DONE</c>)
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Refunded value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Central Bank transaction identifier (Pix only)
    /// </summary>
    [JsonPropertyName("endToEndIdentifier")]
    public string? EndToEndIdentifier { get; set; }

    /// <summary>
    /// Refund description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Date and time the refund took effect (Pix only)
    /// </summary>
    [JsonPropertyName("effectiveDate")]
    public string? EffectiveDate { get; set; }

    /// <summary>
    /// Transaction receipt URL
    /// </summary>
    [JsonPropertyName("transactionReceiptUrl")]
    public string? TransactionReceiptUrl { get; set; }

    /// <summary>
    /// Splits that were refunded
    /// </summary>
    [JsonPropertyName("refundedSplits")]
    public List<InstallmentRefundedSplitResponseDto>? RefundedSplits { get; set; }

    /// <summary>
    /// Identifier of the refunded payment
    /// </summary>
    [JsonPropertyName("paymentId")]
    public string? PaymentId { get; set; }
}
