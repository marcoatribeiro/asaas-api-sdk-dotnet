using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment limits response
/// </summary>
public class PaymentLimitsResponseDto
{
    /// <summary>
    /// Maximum payment value
    /// </summary>
    [JsonPropertyName("maxValue")]
    public decimal? MaxValue { get; set; }

    /// <summary>
    /// Minimum payment value
    /// </summary>
    [JsonPropertyName("minValue")]
    public decimal? MinValue { get; set; }

    /// <summary>
    /// Maximum installments
    /// </summary>
    [JsonPropertyName("maxInstallments")]
    public int? MaxInstallments { get; set; }
}
