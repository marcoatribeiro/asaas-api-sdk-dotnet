using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Tax calculation regime options
/// </summary>
public class FiscalInfoMunicipalOptionsSpecialTaxRegimesDto
{
    /// <summary>
    /// Name of the special taxation regime
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>
    /// Special taxation regime identifier
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
