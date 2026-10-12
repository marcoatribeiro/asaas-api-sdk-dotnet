using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Item of a recurring Pix transaction
/// </summary>
public class PixRecurringTransactionItemGetResponseDto
{
    /// <summary>
    /// Item identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Status: <c>PENDING</c>, <c>CANCELLED</c>, <c>REFUSED</c> or <c>DONE</c>. Treat other values as possible.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Scheduled date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("scheduledDate")]
    public string? ScheduledDate { get; set; }

    /// <summary>
    /// Whether the item can be cancelled
    /// </summary>
    [JsonPropertyName("canBeCancelled")]
    public bool? CanBeCancelled { get; set; }

    /// <summary>
    /// Number of the recurrence
    /// </summary>
    [JsonPropertyName("recurrenceNumber")]
    public int? RecurrenceNumber { get; set; }

    /// <summary>
    /// Number of repetitions
    /// </summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Why the item was refused
    /// </summary>
    [JsonPropertyName("refusalReasonDescription")]
    public string? RefusalReasonDescription { get; set; }

    /// <summary>
    /// Receiver
    /// </summary>
    [JsonPropertyName("externalAccount")]
    public PixRecurringTransactionExternalAccountResponseDto? ExternalAccount { get; set; }
}
