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
    public decimal? Value { get; set; }

    /// <summary>
    /// Total net worth
    /// </summary>
    [JsonPropertyName("netValue")]
    public decimal? NetValue { get; set; }
}
