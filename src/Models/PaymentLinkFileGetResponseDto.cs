using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Image of a payment link
/// </summary>
public class PaymentLinkFileGetResponseDto
{
    /// <summary>
    /// Image identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Whether it is the main image
    /// </summary>
    [JsonPropertyName("main")]
    public bool? Main { get; set; }

    /// <summary>
    /// Image details
    /// </summary>
    [JsonPropertyName("image")]
    public PaymentLinkFileImageResponseDto? Image { get; set; }
}
