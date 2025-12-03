using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Fiscal info save/update request
/// </summary>
public class FiscalInfoSaveRequestDto
{
    /// <summary>
    /// Email used by Asaas to send invoice notifications and alerts
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the company opts for the simple national system
    /// </summary>
    [JsonPropertyName("simplesNacional")]
    public bool SimplesNacional { get; set; } = true;

    /// <summary>
    /// Company municipal registration
    /// </summary>
    [JsonPropertyName("municipalInscription")]
    public string? MunicipalInscription { get; set; }

    /// <summary>
    /// Identifies whether the company is classified as a cultural promoter
    /// </summary>
    [JsonPropertyName("culturalProjectsPromoter")]
    public bool? CulturalProjectsPromoter { get; set; }

    /// <summary>
    /// CNAE code
    /// </summary>
    [JsonPropertyName("cnae")]
    public string? Cnae { get; set; }

    /// <summary>
    /// Special taxation regime identifier
    /// </summary>
    [JsonPropertyName("specialTaxRegime")]
    public string? SpecialTaxRegime { get; set; }

    /// <summary>
    /// Service list item, as http://www.planalto.gov.br/ccivil_03/leis/LCP/Lcp116.htm
    /// </summary>
    [JsonPropertyName("serviceListItem")]
    public string? ServiceListItem { get; set; }

    /// <summary>
    /// NBS Code (Brazilian Nomenclature of Services). It must be included on the NFS-e (Electronic Service Invoice) when required by the municipal government and/or for import or export services. Check with your local government or your accounting department to determine whether this information is necessary.
    /// </summary>
    [JsonPropertyName("nbsCode")]
    public string? NbsCode { get; set; }

    /// <summary>
    /// Serial Number used by your company to issue invoices. In most cities the serial number used is '1' or 'E'
    /// </summary>
    [JsonPropertyName("rpsSerie")]
    public string? RpsSerie { get; set; }

    /// <summary>
    /// RPS number used on the last invoice issued by your company. If your last NF issued has an RPS equal to '100', this field must be filled in with '101'. If you have never issued invoices through your city hall's website, enter '1' in this field
    /// </summary>
    [JsonPropertyName("rpsNumber")]
    public long? RpsNumber { get; set; }

    /// <summary>
    /// Batch number used on the last invoice issued by your company. If the last lot used in your city hall is '25', this field must be filled in with '26'. Only enter this field if your city hall requires the use of lots
    /// </summary>
    [JsonPropertyName("loteNumber")]
    public long? LoteNumber { get; set; }

    /// <summary>
    /// User to access your city's city hall website
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Password to access the city hall website
    /// </summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    /// <summary>
    /// Token for access to the city hall website (If access to your city hall website is via Token)
    /// </summary>
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; set; }

    /// <summary>
    /// File
    /// </summary>
    [JsonPropertyName("certificateFile")]
    public byte[]? CertificateFile { get; set; }

    /// <summary>
    /// Password for the digital certificate sent (If access to your city hall website through a digital certificate)
    /// </summary>
    [JsonPropertyName("certificatePassword")]
    public string? CertificatePassword { get; set; }

    /// <summary>
    /// Identifier of the tax calculation regime. It must only be completed by companies classified as ME or EPP opting for Simples Nacional. Consult the need for this information with your city hall or accounting department.
    /// </summary>
    [JsonPropertyName("nationalPortalTaxCalculationRegime")]
    public string? NationalPortalTaxCalculationRegime { get; set; }
}
