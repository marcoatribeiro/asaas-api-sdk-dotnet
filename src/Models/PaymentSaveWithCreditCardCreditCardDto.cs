using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Credit card information
/// </summary>
public class PaymentSaveWithCreditCardCreditCardDto
{
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
