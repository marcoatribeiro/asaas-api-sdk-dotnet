using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Pix key creation request
/// </summary>
public class PixAddressKeySaveRequestDto
{
    /// <summary>
    /// Key type. Only <c>EVP</c> (random key) can be created.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
