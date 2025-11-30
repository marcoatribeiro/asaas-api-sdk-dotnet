using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Webhook configuration response
/// </summary>
public class WebhookConfigGetResponseDto
{
    /// <summary>
    /// Unique Webhook Identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Webhook name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Webhook URL
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Email that will receive notifications about the Webhook
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Set whether the Webhook is active
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// Set whether the sync queue is stopped
    /// </summary>
    [JsonPropertyName("interrupted")]
    public bool? Interrupted { get; set; }

    /// <summary>
    /// API Version
    /// </summary>
    [JsonPropertyName("apiVersion")]
    public long? ApiVersion { get; set; }

    /// <summary>
    /// Indicates whether an authentication token is registered for the webhook
    /// </summary>
    [JsonPropertyName("hasAuthToken")]
    public bool? HasAuthToken { get; set; }

    /// <summary>
    /// Sequential (SEQUENTIALLY) or non-sequential (NON_SEQUENTIALLY)
    /// </summary>
    [JsonPropertyName("sendType")]
    public string? SendType { get; set; }

    /// <summary>
    /// List of events this Webhook will observe
    /// </summary>
    [JsonPropertyName("events")]
    public List<string>? Events { get; set; }
}
