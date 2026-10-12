using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment link update request. Properties left null are not sent, so they keep their value.
/// </summary>
public class PaymentLinkUpdateRequestDto
{
    /// <summary>
    /// Payment link name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Payment link description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// End date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("endDate")]
    public string? EndDate { get; set; }

    /// <summary>
    /// Amount. When omitted the payer chooses it.
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Whether the link is active
    /// </summary>
    [JsonPropertyName("active")]
    public bool? Active { get; set; }

    /// <summary>
    /// Billing type: <c>UNDEFINED</c>, <c>BOLETO</c>, <c>CREDIT_CARD</c> or <c>PIX</c>
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Charge type: <c>DETACHED</c>, <c>RECURRENT</c> or <c>INSTALLMENT</c>
    /// </summary>
    [JsonPropertyName("chargeType")]
    public string? ChargeType { get; set; }

    /// <summary>
    /// Business days the bank slip can be paid after it is generated. Set it when the link allows bank slips.
    /// </summary>
    [JsonPropertyName("dueDateLimitDays")]
    public int? DueDateLimitDays { get; set; }

    /// <summary>
    /// Cycle, used when the charge type is <c>RECURRENT</c>: <c>WEEKLY</c>, <c>BIWEEKLY</c>, <c>MONTHLY</c>, <c>BIMONTHLY</c>, <c>QUARTERLY</c>, <c>SEMIANNUALLY</c> or <c>YEARLY</c>
    /// </summary>
    [JsonPropertyName("subscriptionCycle")]
    public string? SubscriptionCycle { get; set; }

    /// <summary>
    /// Maximum number of installments, for the <c>INSTALLMENT</c> charge type
    /// </summary>
    [JsonPropertyName("maxInstallmentCount")]
    public int? MaxInstallmentCount { get; set; }

    /// <summary>
    /// Identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Whether customers created through the link get notifications
    /// </summary>
    [JsonPropertyName("notificationEnabled")]
    public bool? NotificationEnabled { get; set; }

    /// <summary>
    /// Redirection after a successful payment
    /// </summary>
    [JsonPropertyName("callback")]
    public PaymentCallbackRequestDto? Callback { get; set; }
}
