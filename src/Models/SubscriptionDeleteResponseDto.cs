using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Subscription delete response
/// </summary>
public class SubscriptionDeleteResponseDto
{
    /// <summary>
    /// Whether it was removed
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }

    /// <summary>
    /// Identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
