using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Webhook configuration update request
/// </summary>
public class WebhookConfigUpdateRequestDto
{
    /// <summary>
    /// Webhook name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Event destination URL
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Sequential (SEQUENTIALLY) or non-sequential (NON_SEQUENTIALLY)
    /// </summary>
    [JsonPropertyName("sendType")]
    public string? SendType { get; set; }

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
    /// Webhook authentication token
    /// </summary>
    [JsonPropertyName("authToken")]
    public string? AuthToken { get; set; }

    /// <summary>
    /// List of events this Webhook will observe
    /// </summary>
    [JsonPropertyName("events")]
    public List<string>? Events { get; set; }
}
