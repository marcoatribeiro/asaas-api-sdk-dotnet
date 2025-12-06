using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Municipal service item
/// </summary>
public class MunicipalServiceDto
{
    /// <summary>
    /// Unique service identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Service description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// ISS percentage rate
    /// </summary>
    [JsonPropertyName("issTax")]
    public double? IssTax { get; set; }
}
