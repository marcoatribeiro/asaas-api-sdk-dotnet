namespace Asaas.Sdk.Models;

/// <summary>
/// Parameters for listing subscriptions. Filters left null are not sent.
/// </summary>
public class ListSubscriptionsParameters
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
    /// Customer identifier
    /// </summary>
    public string? Customer { get; set; }

    /// <summary>
    /// Customer group name
    /// </summary>
    public string? CustomerGroupName { get; set; }

    /// <summary>
    /// Billing type: <c>UNDEFINED</c>, <c>BOLETO</c>, <c>CREDIT_CARD</c>, <c>DEBIT_CARD</c>, <c>TRANSFER</c>, <c>DEPOSIT</c> or <c>PIX</c>
    /// </summary>
    public string? BillingType { get; set; }

    /// <summary>
    /// Status: <c>ACTIVE</c>, <c>EXPIRED</c> or <c>INACTIVE</c>
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// When true, returns only the removed subscriptions
    /// </summary>
    public bool? DeletedOnly { get; set; }

    /// <summary>
    /// When true, includes the removed subscriptions
    /// </summary>
    public bool? IncludeDeleted { get; set; }

    /// <summary>
    /// Identifier in your system
    /// </summary>
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Sort direction (<c>asc</c> or <c>desc</c>)
    /// </summary>
    public string? Order { get; set; }

    /// <summary>
    /// Field to sort by
    /// </summary>
    public string? Sort { get; set; }
}
