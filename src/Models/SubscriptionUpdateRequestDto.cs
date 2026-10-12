using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Subscription update request. Properties left null are not sent, so they keep their value.
/// </summary>
public class SubscriptionUpdateRequestDto
{
    /// <summary>
    /// Billing type: <c>UNDEFINED</c>, <c>BOLETO</c>, <c>CREDIT_CARD</c> or <c>PIX</c>
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Subscription status: <c>ACTIVE</c> or <c>INACTIVE</c>. Reactivating with <c>ACTIVE</c> also requires <see cref="NextDueDate"/>.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Due date of the next charge to generate, <c>yyyy-MM-dd</c>. It does not change existing charges.
    /// </summary>
    [JsonPropertyName("nextDueDate")]
    public string? NextDueDate { get; set; }

    /// <summary>
    /// Discount for payment up to the due date
    /// </summary>
    [JsonPropertyName("discount")]
    public PaymentDiscountDto? Discount { get; set; }

    /// <summary>
    /// Interest for payment after the due date
    /// </summary>
    [JsonPropertyName("interest")]
    public PaymentInterestRequestDto? Interest { get; set; }

    /// <summary>
    /// Fine for payment after the due date
    /// </summary>
    [JsonPropertyName("fine")]
    public PaymentFineRequestDto? Fine { get; set; }

    /// <summary>
    /// Charge cycle: <c>WEEKLY</c>, <c>BIWEEKLY</c>, <c>MONTHLY</c>, <c>BIMONTHLY</c>, <c>QUARTERLY</c>, <c>SEMIANNUALLY</c> or <c>YEARLY</c>
    /// </summary>
    [JsonPropertyName("cycle")]
    public string? Cycle { get; set; }

    /// <summary>
    /// Description (max. 500 characters)
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Last due date of the charges, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("endDate")]
    public string? EndDate { get; set; }

    /// <summary>
    /// When true, the supported changes also apply to the pending charges. By default only future charges change.
    /// </summary>
    [JsonPropertyName("updatePendingPayments")]
    public bool? UpdatePendingPayments { get; set; }

    /// <summary>
    /// Identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Split settings
    /// </summary>
    [JsonPropertyName("split")]
    public List<SubscriptionSplitRequestDto>? Split { get; set; }

    /// <summary>
    /// Redirection after a successful payment
    /// </summary>
    [JsonPropertyName("callback")]
    public PaymentCallbackRequestDto? Callback { get; set; }
}
