using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Subscription creation request
/// </summary>
public class SubscriptionSaveRequestDto
{
    /// <summary>
    /// Unique customer identifier in Asaas (required)
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Billing type: <c>UNDEFINED</c>, <c>BOLETO</c>, <c>CREDIT_CARD</c> or <c>PIX</c> (required)
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Subscription amount (required)
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Due date of the first charge, <c>yyyy-MM-dd</c> (required)
    /// </summary>
    [JsonPropertyName("nextDueDate")]
    public string? NextDueDate { get; set; }

    /// <summary>
    /// Charge cycle: <c>WEEKLY</c>, <c>BIWEEKLY</c>, <c>MONTHLY</c>, <c>BIMONTHLY</c>, <c>QUARTERLY</c>, <c>SEMIANNUALLY</c> or <c>YEARLY</c> (required)
    /// </summary>
    [JsonPropertyName("cycle")]
    public string? Cycle { get; set; }

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
