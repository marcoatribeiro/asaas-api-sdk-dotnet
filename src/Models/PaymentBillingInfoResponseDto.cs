using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment billing information response
/// </summary>
public class PaymentBillingInfoResponseDto
{
    /// <summary>
    /// Bank slip URL
    /// </summary>
    [JsonPropertyName("bankSlipUrl")]
    public string? BankSlipUrl { get; set; }

    /// <summary>
    /// Invoice URL
    /// </summary>
    [JsonPropertyName("invoiceUrl")]
    public string? InvoiceUrl { get; set; }

    /// <summary>
    /// Identification field
    /// </summary>
    [JsonPropertyName("identificationField")]
    public string? IdentificationField { get; set; }

    /// <summary>
    /// Nosso numero
    /// </summary>
    [JsonPropertyName("nossoNumero")]
    public string? NossoNumero { get; set; }
}
