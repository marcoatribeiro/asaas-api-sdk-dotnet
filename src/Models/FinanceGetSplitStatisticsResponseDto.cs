using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Finance split statistics response
/// </summary>
public class FinanceGetSplitStatisticsResponseDto
{
    /// <summary>
    /// Amounts receivable
    /// </summary>
    [JsonPropertyName("income")]
    public double? Income { get; set; }

    /// <summary>
    /// Values to be sent
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; set; }
}
