using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Invoice update request
/// </summary>
public class InvoiceUpdateRequestDto
{
    /// <summary>
    /// Description of invoice services
    /// </summary>
    [JsonPropertyName("serviceDescription")]
    public string? ServiceDescription { get; set; }

    /// <summary>
    /// Additional observations
    /// </summary>
    [JsonPropertyName("observations")]
    public string? Observations { get; set; }

    /// <summary>
    /// Invoice identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Total value
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    /// <summary>
    /// Deductions. Deductions do not change the total value of the invoice, but they do change the ISS calculation basis.
    /// </summary>
    [JsonPropertyName("deductions")]
    public double? Deductions { get; set; }

    /// <summary>
    /// Invoice issuance date
    /// </summary>
    [JsonPropertyName("effectiveDate")]
    public string? EffectiveDate { get; set; }

    /// <summary>
    /// Update the Payment amount with the invoice taxes already deducted.
    /// </summary>
    [JsonPropertyName("updatePayment")]
    public bool? UpdatePayment { get; set; }

    /// <summary>
    /// Invoice taxes
    /// </summary>
    [JsonPropertyName("taxes")]
    public InvoiceTaxesDto? Taxes { get; set; }
}
