using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment fine information for payment after due date
/// </summary>
public class PaymentFineResponseDto
{
    /// <summary>
    /// Fine value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Fine type
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
