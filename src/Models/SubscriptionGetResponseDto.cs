using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Subscription
/// </summary>
public class SubscriptionGetResponseDto
{
    /// <summary>
    /// Object type (<c>subscription</c>)
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Subscription identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

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
    /// Identifier of the payment link that originated the subscription
    /// </summary>
    [JsonPropertyName("paymentLink")]
    public string? PaymentLink { get; set; }

    /// <summary>
    /// Billing type: <c>UNDEFINED</c>, <c>BOLETO</c>, <c>CREDIT_CARD</c>, <c>DEBIT_CARD</c>, <c>TRANSFER</c>, <c>DEPOSIT</c> or <c>PIX</c>
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Charge cycle: <c>WEEKLY</c>, <c>BIWEEKLY</c>, <c>MONTHLY</c>, <c>BIMONTHLY</c>, <c>QUARTERLY</c>, <c>SEMIANNUALLY</c> or <c>YEARLY</c>
    /// </summary>
    [JsonPropertyName("cycle")]
    public string? Cycle { get; set; }

    /// <summary>
    /// Subscription amount
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Due date of the next charge, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("nextDueDate")]
    public string? NextDueDate { get; set; }

    /// <summary>
    /// Last due date of the charges, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("endDate")]
    public string? EndDate { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Status: <c>ACTIVE</c>, <c>EXPIRED</c> or <c>INACTIVE</c>. Treat other values as possible.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Discount for payment up to the due date
    /// </summary>
    [JsonPropertyName("discount")]
    public PaymentDiscountDto? Discount { get; set; }

    /// <summary>
    /// Fine for payment after the due date
    /// </summary>
    [JsonPropertyName("fine")]
    public PaymentFineResponseDto? Fine { get; set; }

    /// <summary>
    /// Interest for payment after the due date
    /// </summary>
    [JsonPropertyName("interest")]
    public PaymentInterestResponseDto? Interest { get; set; }

    /// <summary>
    /// Whether the subscription was removed
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    /// <summary>
    /// Maximum number of charges to generate
    /// </summary>
    [JsonPropertyName("maxPayments")]
    public int? MaxPayments { get; set; }

    /// <summary>
    /// Identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Checkout identifier
    /// </summary>
    [JsonPropertyName("checkoutSession")]
    public string? CheckoutSession { get; set; }

    /// <summary>
    /// Split settings
    /// </summary>
    [JsonPropertyName("split")]
    public List<SubscriptionSplitResponseDto>? Split { get; set; }
}
