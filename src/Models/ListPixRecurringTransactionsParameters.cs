namespace Asaas.Sdk.Models;

/// <summary>
/// Parameters for listing recurring Pix transactions. Filters left null are not sent.
/// </summary>
public class ListPixRecurringTransactionsParameters
{
    /// <summary>
    /// Offset for pagination
    /// </summary>
    public long? Offset { get; set; }

    /// <summary>
    /// Limit for pagination (at most 100)
    /// </summary>
    public long? Limit { get; set; }

    /// <summary>
    /// Status: <c>AWAITING_CRITICAL_ACTION_AUTHORIZATION</c>, <c>PENDING</c>, <c>SCHEDULED</c>, <c>CANCELLED</c> or <c>DONE</c>
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Value of the recurrence
    /// </summary>
    public decimal? Value { get; set; }

    /// <summary>
    /// Receiver name
    /// </summary>
    public string? SearchText { get; set; }
}
