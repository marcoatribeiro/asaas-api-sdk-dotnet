using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment identification field response
/// </summary>
public class PaymentIdentificationFieldResponseDto
{
    /// <summary>
    /// Identification field (digitable line)
    /// </summary>
    [JsonPropertyName("identificationField")]
    public string? IdentificationField { get; set; }

    /// <summary>
    /// Nosso numero
    /// </summary>
    [JsonPropertyName("nossoNumero")]
    public string? NossoNumero { get; set; }

    /// <summary>
    /// Barcode
    /// </summary>
    [JsonPropertyName("barCode")]
    public string? BarCode { get; set; }
}
