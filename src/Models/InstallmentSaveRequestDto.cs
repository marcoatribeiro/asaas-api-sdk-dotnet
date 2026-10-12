using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Installment creation request
/// </summary>
public class InstallmentSaveRequestDto
{
    /// <summary>
    /// Number of installments (required)
    /// </summary>
    [JsonPropertyName("installmentCount")]
    public int? InstallmentCount { get; set; }

    /// <summary>
    /// Unique customer identifier in Asaas (required)
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Value of each installment (required unless <see cref="TotalValue"/> is sent)
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Total amount of the installment
    /// </summary>
    [JsonPropertyName("totalValue")]
    public decimal? TotalValue { get; set; }

    /// <summary>
    /// Billing type: <c>UNDEFINED</c>, <c>BOLETO</c>, <c>CREDIT_CARD</c> or <c>PIX</c> (required)
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Due date of the first installment, <c>yyyy-MM-dd</c> (required)
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Description (max. 500 characters)
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Whether the bank slips are also sent by post
    /// </summary>
    [JsonPropertyName("postalService")]
    public bool? PostalService { get; set; }

    /// <summary>
    /// Days after the due date before the bank slip registration is cancelled (bank slip only)
    /// </summary>
    [JsonPropertyName("daysAfterDueDateToRegistrationCancellation")]
    public int? DaysAfterDueDateToRegistrationCancellation { get; set; }

    /// <summary>
    /// Free search field copied to each payment
    /// </summary>
    [JsonPropertyName("paymentExternalReference")]
    public string? PaymentExternalReference { get; set; }

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
    /// Split settings
    /// </summary>
    [JsonPropertyName("splits")]
    public List<InstallmentSplitRequestDto>? Splits { get; set; }
}
