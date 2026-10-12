using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Installment
/// </summary>
public class InstallmentGetResponseDto
{
    /// <summary>
    /// Object type (<c>installment</c>)
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Installment identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Installment total value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Net value
    /// </summary>
    [JsonPropertyName("netValue")]
    public decimal? NetValue { get; set; }

    /// <summary>
    /// Value of each installment payment
    /// </summary>
    [JsonPropertyName("paymentValue")]
    public decimal? PaymentValue { get; set; }

    /// <summary>
    /// Number of installments
    /// </summary>
    [JsonPropertyName("installmentCount")]
    public int? InstallmentCount { get; set; }

    /// <summary>
    /// Billing type: <c>UNDEFINED</c>, <c>BOLETO</c>, <c>CREDIT_CARD</c>, <c>DEBIT_CARD</c>, <c>TRANSFER</c>, <c>DEPOSIT</c> or <c>PIX</c>
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Payment date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("paymentDate")]
    public string? PaymentDate { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Due day of each installment
    /// </summary>
    [JsonPropertyName("expirationDay")]
    public int? ExpirationDay { get; set; }

    /// <summary>
    /// Creation date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Customer identifier
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Payment link identifier
    /// </summary>
    [JsonPropertyName("paymentLink")]
    public string? PaymentLink { get; set; }

    /// <summary>
    /// Checkout identifier
    /// </summary>
    [JsonPropertyName("checkoutSession")]
    public string? CheckoutSession { get; set; }

    /// <summary>
    /// Transaction receipt URL
    /// </summary>
    [JsonPropertyName("transactionReceiptUrl")]
    public string? TransactionReceiptUrl { get; set; }

    /// <summary>
    /// Chargeback information
    /// </summary>
    [JsonPropertyName("chargeback")]
    public PaymentChargebackResponseDto? Chargeback { get; set; }

    /// <summary>
    /// Credit card information
    /// </summary>
    [JsonPropertyName("creditCard")]
    public InstallmentCreditCardResponseDto? CreditCard { get; set; }

    /// <summary>
    /// Whether the installment was removed
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    /// <summary>
    /// Refunds
    /// </summary>
    [JsonPropertyName("refunds")]
    public List<InstallmentRefundResponseDto>? Refunds { get; set; }

    /// <summary>
    /// 3D Secure challenge URL, returned only when the issuer requires it
    /// </summary>
    [JsonPropertyName("threeDSecureChallengeUrl")]
    public string? ThreeDSecureChallengeUrl { get; set; }
}
