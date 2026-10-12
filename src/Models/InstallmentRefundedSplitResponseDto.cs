using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Split refunded with an installment payment
/// </summary>
public class InstallmentRefundedSplitResponseDto
{
    /// <summary>
    /// Split identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Refunded value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Whether the split was refunded
    /// </summary>
    [JsonPropertyName("done")]
    public bool? Done { get; set; }
}
