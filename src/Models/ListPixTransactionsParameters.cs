namespace Asaas.Sdk.Models;

/// <summary>
/// Parameters for listing Pix transactions. Filters left null are not sent.
/// </summary>
public class ListPixTransactionsParameters
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
    /// Status: <c>AWAITING_BALANCE_VALIDATION</c>, <c>AWAITING_INSTANT_PAYMENT_ACCOUNT_BALANCE</c>, <c>AWAITING_CRITICAL_ACTION_AUTHORIZATION</c>, <c>AWAITING_CHECKOUT_RISK_ANALYSIS_REQUEST</c>, <c>AWAITING_CASH_IN_RISK_ANALYSIS_REQUEST</c>, <c>SCHEDULED</c>, <c>AWAITING_REQUEST</c>, <c>REQUESTED</c>, <c>DONE</c>, <c>REFUSED</c> or <c>CANCELLED</c>
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Type: <c>DEBIT</c>, <c>CREDIT</c>, <c>CREDIT_REFUND</c>, <c>DEBIT_REFUND</c> or <c>DEBIT_REFUND_CANCELLATION</c>
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Central Bank transaction identifier
    /// </summary>
    public string? EndToEndIdentifier { get; set; }
}
