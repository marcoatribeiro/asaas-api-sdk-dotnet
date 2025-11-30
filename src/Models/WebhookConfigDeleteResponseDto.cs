using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Webhook configuration delete response
/// </summary>
public class WebhookConfigDeleteResponseDto
{
    /// <summary>
    /// Indicates whether the Webhook was removed
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    /// <summary>
    /// Unique Webhook Identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
