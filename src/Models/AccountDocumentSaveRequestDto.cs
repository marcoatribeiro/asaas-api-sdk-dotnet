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
    /// Document Type. One of <c>IDENTIFICATION</c>, <c>SOCIAL_CONTRACT</c>, <c>ENTREPRENEUR_REQUIREMENT</c>,
    /// <c>MINUTES_OF_ELECTION</c> or <c>CUSTOM</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
