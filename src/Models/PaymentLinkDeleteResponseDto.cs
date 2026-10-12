using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment link delete response
/// </summary>
public class PaymentLinkDeleteResponseDto
{
    /// <summary>
    /// Whether it was removed
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }

    /// <summary>
    /// Identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
