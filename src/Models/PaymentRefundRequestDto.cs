using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment refund request
/// </summary>
public class PaymentRefundRequestDto
{
    /// <summary>
    /// Refund value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
