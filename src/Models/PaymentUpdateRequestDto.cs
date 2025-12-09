using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment update request
/// </summary>
public class PaymentUpdateRequestDto
{
    /// <summary>
    /// Payment billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Payment amount
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Payment due date
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Payment description (max. 500 characters)
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Days after registration cancellation deadline (only for bank slip)
    /// </summary>
    [JsonPropertyName("daysAfterDueDateToRegistrationCancellation")]
    public long? DaysAfterDueDateToRegistrationCancellation { get; set; }

    /// <summary>
    /// Free search field
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Discount information
    /// </summary>
    [JsonPropertyName("discount")]
    public PaymentDiscountDto? Discount { get; set; }

    /// <summary>
    /// Interest information for payment after due date
    /// </summary>
    [JsonPropertyName("interest")]
    public PaymentInterestRequestDto? Interest { get; set; }

    /// <summary>
    /// Fine information for payment after due date
    /// </summary>
    [JsonPropertyName("fine")]
    public PaymentFineRequestDto? Fine { get; set; }

    /// <summary>
    /// Define whether the payment will be sent via post
    /// </summary>
    [JsonPropertyName("postalService")]
    public bool? PostalService { get; set; }

    /// <summary>
    /// Split Settings
    /// </summary>
    [JsonPropertyName("split")]
    public List<PaymentSplitRequestDto>? Split { get; set; }

    /// <summary>
    /// Automatic redirection information after the payment of the link payment
    /// </summary>
    [JsonPropertyName("callback")]
    public PaymentCallbackRequestDto? Callback { get; set; }
}
