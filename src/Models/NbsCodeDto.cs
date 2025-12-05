using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// NBS code item
/// </summary>
public class NbsCodeDto
{
    /// <summary>
    /// NBS code
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Code description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
