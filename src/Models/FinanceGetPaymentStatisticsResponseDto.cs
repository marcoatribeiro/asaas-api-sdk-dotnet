using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Finance payment statistics response
/// </summary>
public class FinanceGetPaymentStatisticsResponseDto
{
    /// <summary>
    /// Number of charges
    /// </summary>
    [JsonPropertyName("quantity")]
    public long? Quantity { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    /// <summary>
    /// Total net worth
    /// </summary>
    [JsonPropertyName("netValue")]
    public double? NetValue { get; set; }
}
