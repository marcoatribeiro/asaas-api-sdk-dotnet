using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Financial transaction response
/// </summary>
public class FinancialTransactionGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Unique transaction identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Transaction value
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    /// <summary>
    /// Value in account at the time of the transaction
    /// </summary>
    [JsonPropertyName("balance")]
    public double? Balance { get; set; }

    /// <summary>
    /// Transaction type (PAYMENT, PAYMENT_FEE, TRANSFER, TRANSFER_FEE, etc.)
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Transaction date
    /// </summary>
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    /// <summary>
    /// Transaction description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Payment identifier (if any)
    /// </summary>
    [JsonPropertyName("paymentId")]
    public string? PaymentId { get; set; }

    /// <summary>
    /// Split identifier (if any)
    /// </summary>
    [JsonPropertyName("splitId")]
    public string? SplitId { get; set; }

    /// <summary>
    /// Transfer identifier (if any)
    /// </summary>
    [JsonPropertyName("transferId")]
    public string? TransferId { get; set; }

    /// <summary>
    /// Anticipation identifier (if any)
    /// </summary>
    [JsonPropertyName("anticipationId")]
    public string? AnticipationId { get; set; }

    /// <summary>
    /// Bill payment identifier (if any)
    /// </summary>
    [JsonPropertyName("billId")]
    public string? BillId { get; set; }

    /// <summary>
    /// Invoice identifier (if any)
    /// </summary>
    [JsonPropertyName("invoiceId")]
    public string? InvoiceId { get; set; }

    /// <summary>
    /// Payment dunning identifier (if any)
    /// </summary>
    [JsonPropertyName("paymentDunningId")]
    public string? PaymentDunningId { get; set; }

    /// <summary>
    /// Serasa consultation identifier (if any)
    /// </summary>
    [JsonPropertyName("creditBureauReportId")]
    public string? CreditBureauReportId { get; set; }
}
