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
    /// Document approval status. One of <c>NOT_SENT</c>, <c>PENDING</c>, <c>APPROVED</c>, <c>REJECTED</c> or
    /// <c>IGNORED</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
