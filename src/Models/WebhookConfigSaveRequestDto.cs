using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Webhook configuration save request
/// </summary>
public class WebhookConfigSaveRequestDto
{
    /// <summary>
    /// Webhook URL
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Webhook email
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// API version
    /// </summary>
    [JsonPropertyName("apiVersion")]
    public int? ApiVersion { get; set; }

    /// <summary>
    /// Enabled flag
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// Interrupted flag
    /// </summary>
    [JsonPropertyName("interrupted")]
    public bool? Interrupted { get; set; }

    /// <summary>
    /// Authentication token
    /// </summary>
    [JsonPropertyName("authToken")]
    public string? AuthToken { get; set; }
}
