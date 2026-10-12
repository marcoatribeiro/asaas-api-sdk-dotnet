using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Image details of a payment link image
/// </summary>
public class PaymentLinkFileImageResponseDto
{
    /// <summary>
    /// Original file name
    /// </summary>
    [JsonPropertyName("originalName")]
    public string? OriginalName { get; set; }

    /// <summary>
    /// Image size
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }

    /// <summary>
    /// File extension (e.g. <c>png</c>)
    /// </summary>
    [JsonPropertyName("extension")]
    public string? Extension { get; set; }

    /// <summary>
    /// Preview URL
    /// </summary>
    [JsonPropertyName("previewUrl")]
    public string? PreviewUrl { get; set; }

    /// <summary>
    /// Download URL
    /// </summary>
    [JsonPropertyName("downloadUrl")]
    public string? DownloadUrl { get; set; }
}
