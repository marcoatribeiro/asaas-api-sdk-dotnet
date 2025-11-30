namespace Asaas.Sdk.Models;

/// <summary>
/// Parameters for listing payments
/// </summary>
public class ListPaymentsParameters
{
    /// <summary>
    /// Customer identifier
    /// </summary>
    public string? Customer { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    public string? BillingType { get; set; }

    /// <summary>
    /// Status filter
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Subscription identifier
    /// </summary>
    public string? Subscription { get; set; }

    /// <summary>
    /// Installment identifier
    /// </summary>
    public string? Installment { get; set; }

    /// <summary>
    /// External reference
    /// </summary>
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Offset for pagination
    /// </summary>
    public long? Offset { get; set; }

    /// <summary>
    /// Limit for pagination
    /// </summary>
    public long? Limit { get; set; }
}
