using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account document update request
/// </summary>
public class AccountDocumentUpdateRequestDto
{
    /// <summary>
    /// File
    /// </summary>
    [JsonPropertyName("documentFile")]
    public byte[] DocumentFile { get; set; } = Array.Empty<byte>();
}
