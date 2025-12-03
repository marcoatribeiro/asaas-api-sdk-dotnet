using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// City information registered in your account
/// </summary>
public class AccountInfoCityDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Unique city identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    /// <summary>
    /// IBGE Code
    /// </summary>
    [JsonPropertyName("ibgeCode")]
    public string? IbgeCode { get; set; }

    /// <summary>
    /// City's name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// District code
    /// </summary>
    [JsonPropertyName("districtCode")]
    public string? DistrictCode { get; set; }

    /// <summary>
    /// District name
    /// </summary>
    [JsonPropertyName("district")]
    public string? District { get; set; }

    /// <summary>
    /// State abbreviation (SP, RJ, SC, ...)
    /// </summary>
    [JsonPropertyName("state")]
    public AccountInfoCityState? State { get; set; }
}
