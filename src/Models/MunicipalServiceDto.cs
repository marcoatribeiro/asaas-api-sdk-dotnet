using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Municipal service item
/// </summary>
public class MunicipalServiceDto
{
    /// <summary>
    /// Service code
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Service description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
