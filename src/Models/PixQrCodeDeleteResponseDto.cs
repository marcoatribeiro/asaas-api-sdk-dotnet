using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Static Pix QR Code delete response
/// </summary>
public class PixQrCodeDeleteResponseDto
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
