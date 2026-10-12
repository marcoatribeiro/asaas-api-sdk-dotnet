using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Credit card of an installment, as returned
/// </summary>
public class InstallmentCreditCardResponseDto
{
    /// <summary>
    /// Last 4 digits of the card
    /// </summary>
    [JsonPropertyName("creditCardNumber")]
    public string? CreditCardNumber { get; set; }

    /// <summary>
    /// Card brand (e.g. <c>VISA</c>, <c>MASTERCARD</c>, <c>ELO</c>, <c>UNKNOWN</c>)
    /// </summary>
    [JsonPropertyName("creditCardBrand")]
    public string? CreditCardBrand { get; set; }

    /// <summary>
    /// Credit card token, when tokenization is enabled
    /// </summary>
    [JsonPropertyName("creditCardToken")]
    public string? CreditCardToken { get; set; }
}
