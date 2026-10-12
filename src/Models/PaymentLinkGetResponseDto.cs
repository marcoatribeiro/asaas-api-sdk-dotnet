using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment link
/// </summary>
public class PaymentLinkGetResponseDto
{
    /// <summary>
    /// Payment link identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Payment link name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Whether the link is active
    /// </summary>
    [JsonPropertyName("active")]
    public bool? Active { get; set; }

    /// <summary>
    /// Charge type: <c>DETACHED</c>, <c>RECURRENT</c> or <c>INSTALLMENT</c>
    /// </summary>
    [JsonPropertyName("chargeType")]
    public string? ChargeType { get; set; }

    /// <summary>
    /// Public URL of the link
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Billing type: <c>UNDEFINED</c>, <c>BOLETO</c>, <c>CREDIT_CARD</c> or <c>PIX</c>
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Cycle: <c>WEEKLY</c>, <c>BIWEEKLY</c>, <c>MONTHLY</c>, <c>BIMONTHLY</c>, <c>QUARTERLY</c>, <c>SEMIANNUALLY</c> or <c>YEARLY</c>
    /// </summary>
    [JsonPropertyName("subscriptionCycle")]
    public string? SubscriptionCycle { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// End date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("endDate")]
    public string? EndDate { get; set; }

    /// <summary>
    /// Whether the link was removed
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    /// <summary>
    /// Number of views
    /// </summary>
    [JsonPropertyName("viewCount")]
    public int? ViewCount { get; set; }

    /// <summary>
    /// Maximum number of installments
    /// </summary>
    [JsonPropertyName("maxInstallmentCount")]
    public int? MaxInstallmentCount { get; set; }

    /// <summary>
    /// Business days the bank slip can be paid after it is generated
    /// </summary>
    [JsonPropertyName("dueDateLimitDays")]
    public int? DueDateLimitDays { get; set; }

    /// <summary>
    /// Whether notifications are enabled
    /// </summary>
    [JsonPropertyName("notificationEnabled")]
    public bool? NotificationEnabled { get; set; }

    /// <summary>
    /// Whether the payer must give an address
    /// </summary>
    [JsonPropertyName("isAddressRequired")]
    public bool? IsAddressRequired { get; set; }

    /// <summary>
    /// Identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }
}
