using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Pix key list response. The Asaas documents no pagination fields for this list.
/// </summary>
public class PixAddressKeyListResponseDto
{
    /// <summary>
    /// Pix keys
    /// </summary>
    [JsonPropertyName("data")]
    public List<PixAddressKeyGetResponseDto> Data { get; set; } = new();
}
