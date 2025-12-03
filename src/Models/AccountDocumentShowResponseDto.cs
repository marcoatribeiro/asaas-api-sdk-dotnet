using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account document list response
/// </summary>
public class AccountDocumentShowResponseDto
{
    /// <summary>
    /// Reason why account approval was rejected
    /// </summary>
    [JsonPropertyName("rejectReasons")]
    public string? RejectReasons { get; set; }

    /// <summary>
    /// List of objects
    /// </summary>
    [JsonPropertyName("data")]
    public List<AccountDocumentGroupResponseDto>? Data { get; set; }
}
