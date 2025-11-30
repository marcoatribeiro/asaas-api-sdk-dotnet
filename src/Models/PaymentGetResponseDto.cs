using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment details response
/// </summary>
public class PaymentGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Unique payment identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Payment creation date
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Unique identifier of the customer to whom the payment belongs
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Unique subscription identifier (when recurring billing)
    /// </summary>
    [JsonPropertyName("subscription")]
    public string? Subscription { get; set; }

    /// <summary>
    /// Unique installment identifier (when installment payment)
    /// </summary>
    [JsonPropertyName("installment")]
    public string? Installment { get; set; }

    /// <summary>
    /// Payment due date
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Net value after fees
    /// </summary>
    [JsonPropertyName("netValue")]
    public decimal? NetValue { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Payment status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Payment description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// External reference
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Confirmed date
    /// </summary>
    [JsonPropertyName("confirmedDate")]
    public string? ConfirmedDate { get; set; }

    /// <summary>
    /// Invoice URL
    /// </summary>
    [JsonPropertyName("invoiceUrl")]
    public string? InvoiceUrl { get; set; }

    /// <summary>
    /// Bank slip URL
    /// </summary>
    [JsonPropertyName("bankSlipUrl")]
    public string? BankSlipUrl { get; set; }

    /// <summary>
    /// Transaction receipt URL
    /// </summary>
    [JsonPropertyName("transactionReceiptUrl")]
    public string? TransactionReceiptUrl { get; set; }
}
