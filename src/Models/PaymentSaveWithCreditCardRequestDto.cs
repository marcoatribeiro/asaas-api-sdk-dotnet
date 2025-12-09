using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment save with credit card request
/// </summary>
public class PaymentSaveWithCreditCardRequestDto
{
    /// <summary>
    /// Unique customer identifier in Asaas
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Payment billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Payment amount
    /// </summary>
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

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
    /// Number of installments (only in the case of installment payment)
    /// </summary>
    [JsonPropertyName("installmentCount")]
    public long? InstallmentCount { get; set; }

    /// <summary>
    /// Enter the total amount of a charge that will be paid in installments (only in the case of an installment charge). If this field is sent, the installmentValue is not necessary, the calculation per installment will be automatic.
    /// </summary>
    [JsonPropertyName("totalValue")]
    public decimal? TotalValue { get; set; }

    /// <summary>
    /// Value of each installment (only in the case of installment payment). Send this field if you want to define the value of each installment.
    /// </summary>
    [JsonPropertyName("installmentValue")]
    public decimal? InstallmentValue { get; set; }

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

    /// <summary>
    /// Credit card holder name
    /// </summary>
    [JsonPropertyName("creditCardHolderName")]
    public string? CreditCardHolderName { get; set; }

    /// <summary>
    /// Credit card number
    /// </summary>
    [JsonPropertyName("creditCardNumber")]
    public string? CreditCardNumber { get; set; }

    /// <summary>
    /// Credit card expiry month
    /// </summary>
    [JsonPropertyName("creditCardExpiryMonth")]
    public string? CreditCardExpiryMonth { get; set; }

    /// <summary>
    /// Credit card expiry year
    /// </summary>
    [JsonPropertyName("creditCardExpiryYear")]
    public string? CreditCardExpiryYear { get; set; }

    /// <summary>
    /// Credit card CVV
    /// </summary>
    [JsonPropertyName("creditCardCcv")]
    public string? CreditCardCcv { get; set; }

    /// <summary>
    /// Credit card holder CPF/CNPJ
    /// </summary>
    [JsonPropertyName("creditCardHolderCpfCnpj")]
    public string? CreditCardHolderCpfCnpj { get; set; }

    /// <summary>
    /// Credit card token
    /// </summary>
    [JsonPropertyName("creditCardToken")]
    public string? CreditCardToken { get; set; }
}
