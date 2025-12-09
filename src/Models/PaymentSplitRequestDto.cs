using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Split Settings
/// </summary>
public class PaymentSplitRequestDto
{
    /// <summary>
    /// Asaas wallet identifier that will be transferred
    /// </summary>
    [JsonPropertyName("walletId")]
    public string? WalletId { get; set; }

    /// <summary>
    /// Fixed amount to be transferred to the account when the payment is received
    /// </summary>
    [JsonPropertyName("fixedValue")]
    public decimal? FixedValue { get; set; }

    /// <summary>
    /// Percentage of the net value of the charge to be transferred when received
    /// </summary>
    [JsonPropertyName("percentualValue")]
    public decimal? PercentualValue { get; set; }

    /// <summary>
    /// (Instalments only). Amount that will be split relative to the total amount that will be paid in installments.
    /// </summary>
    [JsonPropertyName("totalFixedValue")]
    public decimal? TotalFixedValue { get; set; }

    /// <summary>
    /// Split identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Split description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
