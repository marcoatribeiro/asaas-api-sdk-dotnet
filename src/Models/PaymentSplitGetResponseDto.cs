using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment split information
/// </summary>
public class PaymentSplitGetResponseDto
{
    /// <summary>
    /// Wallet identifier
    /// </summary>
    [JsonPropertyName("walletId")]
    public string? WalletId { get; set; }

    /// <summary>
    /// Fixed value
    /// </summary>
    [JsonPropertyName("fixedValue")]
    public decimal? FixedValue { get; set; }

    /// <summary>
    /// Percentage value
    /// </summary>
    [JsonPropertyName("percentualValue")]
    public decimal? PercentualValue { get; set; }

    /// <summary>
    /// Total value
    /// </summary>
    [JsonPropertyName("totalValue")]
    public decimal? TotalValue { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Refusal reason
    /// </summary>
    [JsonPropertyName("refusalReason")]
    public string? RefusalReason { get; set; }
}
