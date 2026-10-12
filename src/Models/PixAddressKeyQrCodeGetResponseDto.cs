using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// QR Code of a Pix key
/// </summary>
public class PixAddressKeyQrCodeGetResponseDto
{
    /// <summary>
    /// QR Code image, base64-encoded
    /// </summary>
    [JsonPropertyName("encodedImage")]
    public string? EncodedImage { get; set; }

    /// <summary>
    /// QR Code "copia e cola" payload
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }
}
