using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Invoice save request
/// </summary>
public class InvoiceSaveRequestDto
{
    /// <summary>
    /// Description of invoice services
    /// </summary>
    [JsonPropertyName("serviceDescription")]
    public string ServiceDescription { get; set; } = string.Empty;

    /// <summary>
    /// Additional observations
    /// </summary>
    [JsonPropertyName("observations")]
    public string Observations { get; set; } = string.Empty;

    /// <summary>
    /// Total value
    /// </summary>
    [JsonPropertyName("value")]
    public double Value { get; set; }

    /// <summary>
    /// Deductions. Deductions do not change the total value of the invoice, but they do change the ISS calculation basis.
    /// </summary>
    [JsonPropertyName("deductions")]
    public double Deductions { get; set; }

    /// <summary>
    /// Invoice issuance date
    /// </summary>
    [JsonPropertyName("effectiveDate")]
    public string EffectiveDate { get; set; } = string.Empty;

    /// <summary>
    /// Name of the municipal service. If not provided, the municipalServiceCode attribute will be used as the name for identification.
    /// </summary>
    [JsonPropertyName("municipalServiceName")]
    public string MunicipalServiceName { get; set; } = string.Empty;

    /// <summary>
    /// Invoice taxes
    /// </summary>
    [JsonPropertyName("taxes")]
    public InvoiceTaxesDto Taxes { get; set; } = new();

    /// <summary>
    /// Unique payment identifier in Asaas
    /// </summary>
    [JsonPropertyName("payment")]
    public string? Payment { get; set; }

    /// <summary>
    /// Unique installment identifier in Asaas
    /// </summary>
    [JsonPropertyName("installment")]
    public string? Installment { get; set; }

    /// <summary>
    /// Unique customer identifier
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Invoice identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Unique municipal service identifier
    /// </summary>
    [JsonPropertyName("municipalServiceId")]
    public string? MunicipalServiceId { get; set; }

    /// <summary>
    /// Municipal Service Code
    /// </summary>
    [JsonPropertyName("municipalServiceCode")]
    public string? MunicipalServiceCode { get; set; }

    /// <summary>
    /// Update the Payment amount with the invoice taxes already deducted.
    /// </summary>
    [JsonPropertyName("updatePayment")]
    public bool? UpdatePayment { get; set; }
}
