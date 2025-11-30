using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Customer delete response
/// </summary>
public class CustomerDeleteResponseDto
{
    /// <summary>
    /// Indicates if the customer was deleted
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }

    /// <summary>
    /// Customer identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
