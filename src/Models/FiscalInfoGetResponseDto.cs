using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Fiscal info get response
/// </summary>
public class FiscalInfoGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Email used by Asaas to send invoice notifications and alerts
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Company municipal registration
    /// </summary>
    [JsonPropertyName("municipalInscription")]
    public string? MunicipalInscription { get; set; }

    /// <summary>
    /// Indicates whether the company opts for the simple national system
    /// </summary>
    [JsonPropertyName("simplesNacional")]
    public bool? SimplesNacional { get; set; }

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
    /// Serial Number registered for the company
    /// </summary>
    [JsonPropertyName("rpsSerie")]
    public string? RpsSerie { get; set; }

    /// <summary>
    /// RPS number used in the last invoice issued to your company
    /// </summary>
    [JsonPropertyName("rpsNumber")]
    public long? RpsNumber { get; set; }

    /// <summary>
    /// Batch number used on the last invoice issued by your company
    /// </summary>
    [JsonPropertyName("loteNumber")]
    public long? LoteNumber { get; set; }

    /// <summary>
    /// User to access your city's city hall website
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Indicates whether the password to access the city hall website has been entered
    /// </summary>
    [JsonPropertyName("passwordSent")]
    public bool? PasswordSent { get; set; }

    /// <summary>
    /// Indicates whether the token for accessing the city hall website was provided
    /// </summary>
    [JsonPropertyName("accessTokenSent")]
    public bool? AccessTokenSent { get; set; }

    /// <summary>
    /// Indicates whether the digital certificate for access to the city hall website has been provided
    /// </summary>
    [JsonPropertyName("certificateSent")]
    public bool? CertificateSent { get; set; }

    /// <summary>
    /// Identifier of the tax calculation regime
    /// </summary>
    [JsonPropertyName("nationalPortalTaxCalculationRegime")]
    public string? NationalPortalTaxCalculationRegime { get; set; }
}
