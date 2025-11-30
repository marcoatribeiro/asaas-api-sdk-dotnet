using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Error item
/// </summary>
public class ErrorItemDto
{
    /// <summary>
    /// Error code
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Error description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
