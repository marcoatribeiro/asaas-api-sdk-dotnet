using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account document save request
/// </summary>
public class AccountDocumentSaveRequestDto
{
    /// <summary>
    /// File
    /// </summary>
    [JsonPropertyName("documentFile")]
    public byte[]? DocumentFile { get; set; }

    /// <summary>
    /// Document Type
    /// </summary>
    [JsonPropertyName("type")]
    public AccountDocumentType? Type { get; set; }
}
