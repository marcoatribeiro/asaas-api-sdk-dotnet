using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account delete response
/// </summary>
public class AccountDeleteResponseDto
{
    /// <summary>
    /// Indicates if the account was deleted successfully
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }

    /// <summary>
    /// Account identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
