using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment interest information for payment after due date
/// </summary>
public class PaymentInterestResponseDto
{
    /// <summary>
    /// Interest value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Interest type
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
