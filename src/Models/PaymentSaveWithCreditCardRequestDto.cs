using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment save with credit card request
/// </summary>
public class PaymentSaveWithCreditCardRequestDto
{
    /// <summary>
    /// Customer identifier
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    /// <summary>
    /// Due date
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// External reference
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

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
