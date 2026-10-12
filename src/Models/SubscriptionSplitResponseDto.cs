using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Split settings of a subscription, as returned
/// </summary>
public class SubscriptionSplitResponseDto
{
    /// <summary>
    /// Asaas wallet identifier that receives the split (required)
    /// </summary>
    [JsonPropertyName("walletId")]
    public string? WalletId { get; set; }

    /// <summary>
    /// Fixed amount to transfer when the payment is received
    /// </summary>
    [JsonPropertyName("fixedValue")]
    public decimal? FixedValue { get; set; }

    /// <summary>
    /// Percentage of the net value of the charge to transfer when it is received
    /// </summary>
    [JsonPropertyName("percentualValue")]
    public decimal? PercentualValue { get; set; }

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

    /// <summary>
    /// Split status: <c>ACTIVE</c> or <c>DISABLED</c>
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Why the split was disabled (e.g. <c>WALLET_UNABLE_TO_RECEIVE</c>, <c>VALUE_DIVERGENCE</c>, <c>WALLET_REMOVED_FROM_OUTSIDE_ARRANGEMENT_ALLOW_LIST</c>)
    /// </summary>
    [JsonPropertyName("disabledReason")]
    public string? DisabledReason { get; set; }
}
