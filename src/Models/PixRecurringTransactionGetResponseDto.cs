using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Recurring Pix transaction
/// </summary>
public class PixRecurringTransactionGetResponseDto
{
    /// <summary>
    /// Recurrence identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Status: <c>AWAITING_CRITICAL_ACTION_AUTHORIZATION</c>, <c>PENDING</c>, <c>SCHEDULED</c>, <c>CANCELLED</c> or <c>DONE</c>. Treat other values as possible.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Origin (<c>PIX</c>)
    /// </summary>
    [JsonPropertyName("origin")]
    public string? Origin { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Frequency: <c>WEEKLY</c> or <c>MONTHLY</c>
    /// </summary>
    [JsonPropertyName("frequency")]
    public string? Frequency { get; set; }

    /// <summary>
    /// Number of repetitions
    /// </summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    /// Start date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("startDate")]
    public string? StartDate { get; set; }

    /// <summary>
    /// End date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("finishDate")]
    public string? FinishDate { get; set; }

    /// <summary>
    /// Whether the recurrence can be cancelled
    /// </summary>
    [JsonPropertyName("canBeCancelled")]
    public bool? CanBeCancelled { get; set; }

    /// <summary>
    /// Receiver
    /// </summary>
    [JsonPropertyName("externalAccount")]
    public PixRecurringTransactionExternalAccountResponseDto? ExternalAccount { get; set; }
}
