using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account document get response
/// </summary>
public class AccountDocumentGetResponseDto
{
    /// <summary>
    /// Unique document identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Document approval status
    /// </summary>
    [JsonPropertyName("status")]
    public AccountDocumentStatus? Status { get; set; }
}
