using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Invoice get response
/// </summary>
public class InvoiceGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Unique invoice identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Invoice status
    /// </summary>
    [JsonPropertyName("status")]
    public InvoiceStatus? Status { get; set; }

    /// <summary>
    /// Unique customer identifier
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

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
    /// Invoice type
    /// </summary>
    [JsonPropertyName("type")]
    public InvoiceType? Type { get; set; }

    /// <summary>
    /// Description of the current status of the invoice
    /// </summary>
    [JsonPropertyName("statusDescription")]
    public string? StatusDescription { get; set; }

    /// <summary>
    /// Description of invoice services
    /// </summary>
    [JsonPropertyName("serviceDescription")]
    public string? ServiceDescription { get; set; }

    /// <summary>
    /// Link to pdf file of the invoice issued
    /// </summary>
    [JsonPropertyName("pdfUrl")]
    public string? PdfUrl { get; set; }

    /// <summary>
    /// Link to xml file of the issued invoice
    /// </summary>
    [JsonPropertyName("xmlUrl")]
    public string? XmlUrl { get; set; }

    /// <summary>
    /// Invoice series
    /// </summary>
    [JsonPropertyName("rpsSerie")]
    public string? RpsSerie { get; set; }

    /// <summary>
    /// RPS converted to invoice
    /// </summary>
    [JsonPropertyName("rpsNumber")]
    public string? RpsNumber { get; set; }

    /// <summary>
    /// Invoice number
    /// </summary>
    [JsonPropertyName("number")]
    public string? Number { get; set; }

    /// <summary>
    /// Verification code
    /// </summary>
    [JsonPropertyName("validationCode")]
    public string? ValidationCode { get; set; }

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
    /// Additional observations
    /// </summary>
    [JsonPropertyName("observations")]
    public string? Observations { get; set; }

    /// <summary>
    /// Estimated tax invoice
    /// </summary>
    [JsonPropertyName("estimatedTaxesDescription")]
    public string? EstimatedTaxesDescription { get; set; }

    /// <summary>
    /// Invoice identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Invoice taxes
    /// </summary>
    [JsonPropertyName("taxes")]
    public InvoiceTaxesDto? Taxes { get; set; }

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
    /// Name of municipal service
    /// </summary>
    [JsonPropertyName("municipalServiceName")]
    public string? MunicipalServiceName { get; set; }
}
