using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Credit card of a chargeback
/// </summary>
public class ChargebackCreditCardResponseDto
{
    /// <summary>
    /// Last 4 digits of the card
    /// </summary>
    [JsonPropertyName("number")]
    public string? Number { get; set; }

    /// <summary>
    /// Card brand (e.g. <c>VISA</c>, <c>MASTERCARD</c>, <c>ELO</c>, <c>UNKNOWN</c>)
    /// </summary>
    [JsonPropertyName("brand")]
    public string? Brand { get; set; }
}
