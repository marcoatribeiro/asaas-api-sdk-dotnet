using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment delete response
/// </summary>
public class PaymentDeleteResponseDto
{
    /// <summary>
    /// Indicates if the payment was deleted
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }

    /// <summary>
    /// Payment identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
