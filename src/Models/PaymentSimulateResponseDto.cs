using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment simulate response
/// </summary>
public class PaymentSimulateResponseDto
{
    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Net value after fees
    /// </summary>
    [JsonPropertyName("netValue")]
    public decimal? NetValue { get; set; }

    /// <summary>
    /// Fee value
    /// </summary>
    [JsonPropertyName("feeValue")]
    public decimal? FeeValue { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }
}
