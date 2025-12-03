using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Fiscal info municipal options get response
/// </summary>
public class FiscalInfoMunicipalOptionsGetResponseDto
{
    /// <summary>
    /// Type of authentication required at city hall
    /// </summary>
    [JsonPropertyName("authenticationType")]
    public FiscalInfoAuthenticationType? AuthenticationType { get; set; }

    /// <summary>
    /// Whether or not it supports the cancellation of invoices automatically at your city hall
    /// </summary>
    [JsonPropertyName("supportsCancellation")]
    public bool? SupportsCancellation { get; set; }

    /// <summary>
    /// It is necessary to inform or not the special taxation regime. If used, enter it in the `specialTaxRegime` field of **Create or update tax information** according to the options returned in the `specialTaxRegimesList` list.
    /// </summary>
    [JsonPropertyName("usesSpecialTaxRegimes")]
    public bool? UsesSpecialTaxRegimes { get; set; }

    /// <summary>
    /// Whether or not to inform the item on the service list
    /// </summary>
    [JsonPropertyName("usesServiceListItem")]
    public bool? UsesServiceListItem { get; set; }

    /// <summary>
    /// Tax calculation regime options
    /// </summary>
    [JsonPropertyName("specialTaxRegimesList")]
    public List<FiscalInfoMunicipalOptionsSpecialTaxRegimesDto>? SpecialTaxRegimesList { get; set; }

    /// <summary>
    /// Special taxation regime options
    /// </summary>
    [JsonPropertyName("nationalPortalTaxCalculationRegimeList")]
    public List<FiscalInfoMunicipalOptionsNationalPortalTaxCalculationRegimeDto>? NationalPortalTaxCalculationRegimeList { get; set; }

    /// <summary>
    /// Explanation of the tax calculation regime
    /// </summary>
    [JsonPropertyName("nationalPortalTaxCalculationRegimeHelp")]
    public string? NationalPortalTaxCalculationRegimeHelp { get; set; }

    /// <summary>
    /// Explanation of the municipal registration format
    /// </summary>
    [JsonPropertyName("municipalInscriptionHelp")]
    public string? MunicipalInscriptionHelp { get; set; }

    /// <summary>
    /// Explanation of the special taxation regime
    /// </summary>
    [JsonPropertyName("specialTaxRegimeHelp")]
    public string? SpecialTaxRegimeHelp { get; set; }

    /// <summary>
    /// Explanation of service list item format
    /// </summary>
    [JsonPropertyName("serviceListItemHelp")]
    public string? ServiceListItemHelp { get; set; }

    /// <summary>
    /// Explanation of digital certificate
    /// </summary>
    [JsonPropertyName("digitalCertificatedHelp")]
    public string? DigitalCertificatedHelp { get; set; }

    /// <summary>
    /// Token Explanation
    /// </summary>
    [JsonPropertyName("accessTokenHelp")]
    public string? AccessTokenHelp { get; set; }

    /// <summary>
    /// Explanation of municipal service code format
    /// </summary>
    [JsonPropertyName("municipalServiceCodeHelp")]
    public string? MunicipalServiceCodeHelp { get; set; }
}
