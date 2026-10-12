using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Split settings of an installment
/// </summary>
public class InstallmentSplitRequestDto
{
    /// <summary>
    /// Asaas wallet identifier that receives the split (required)
    /// </summary>
    [JsonPropertyName("walletId")]
    public string? WalletId { get; set; }

    /// <summary>
    /// Fixed amount to transfer when each payment is received
    /// </summary>
    [JsonPropertyName("fixedValue")]
    public decimal? FixedValue { get; set; }

    /// <summary>
    /// Percentage of the net value of the charge to transfer when it is received
    /// </summary>
    [JsonPropertyName("percentualValue")]
    public decimal? PercentualValue { get; set; }

    /// <summary>
    /// Amount to split over the whole installment. Cannot be sent with <see cref="InstallmentNumber"/>.
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

    /// <summary>
    /// Number of the installment the split applies to. Cannot be sent with <see cref="TotalFixedValue"/>.
    /// </summary>
    [JsonPropertyName("installmentNumber")]
    public int? InstallmentNumber { get; set; }
}
