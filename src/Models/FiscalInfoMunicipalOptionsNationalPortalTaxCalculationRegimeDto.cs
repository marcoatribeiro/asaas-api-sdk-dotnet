using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Special taxation regime options
/// </summary>
public class FiscalInfoMunicipalOptionsNationalPortalTaxCalculationRegimeDto
{
    /// <summary>
    /// Name of the tax calculation regime
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>
    /// Identifier of the tax calculation regime
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
