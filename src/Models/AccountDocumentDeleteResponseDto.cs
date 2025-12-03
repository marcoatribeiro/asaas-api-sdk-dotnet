using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account document delete response
/// </summary>
public class AccountDocumentDeleteResponseDto
{
    /// <summary>
    /// Indicates whether the document was removed
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    /// <summary>
    /// Unique document identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
