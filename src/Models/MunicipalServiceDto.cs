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
    /// ISS percentage rate (<c>5</c> means 5%). The Asaas reference gives both <c>0.5</c> and <c>5</c> as examples, so
    /// check the scale against a sandbox response before relying on it.
    /// </summary>
    [JsonPropertyName("issTax")]
    public decimal? IssTax { get; set; }
}
