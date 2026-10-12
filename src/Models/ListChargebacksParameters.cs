namespace Asaas.Sdk.Models;

/// <summary>
/// Parameters for listing chargebacks. Filters left null are not sent.
/// </summary>
public class ListChargebacksParameters
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
    /// Chargeback status: <c>REQUESTED</c>, <c>IN_DISPUTE</c>, <c>DISPUTE_LOST</c>, <c>REVERSED</c> or <c>DONE</c>
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Card brand: <c>VISA</c>, <c>MASTERCARD</c>, <c>ELO</c>, <c>DINERS</c>, <c>DISCOVER</c>, <c>AMEX</c>, <c>CABAL</c>,
    /// <c>BANESCARD</c>, <c>CREDZ</c>, <c>SOROCRED</c>, <c>CREDSYSTEM</c>, <c>JCB</c> or <c>UNKNOWN</c>
    /// </summary>
    public string? CreditCardBrand { get; set; }

    /// <summary>
    /// Dispute opened on or after this date (sent as <c>originDisputeDate[ge]</c>)
    /// </summary>
    public DateOnly? OriginDisputeDateGreaterOrEqual { get; set; }

    /// <summary>
    /// Dispute opened on or before this date (sent as <c>originDisputeDate[le]</c>)
    /// </summary>
    public DateOnly? OriginDisputeDateLessOrEqual { get; set; }

    /// <summary>
    /// Transaction made on or after this date (sent as <c>originTransactionDate[ge]</c>)
    /// </summary>
    public DateOnly? OriginTransactionDateGreaterOrEqual { get; set; }

    /// <summary>
    /// Transaction made on or before this date (sent as <c>originTransactionDate[le]</c>)
    /// </summary>
    public DateOnly? OriginTransactionDateLessOrEqual { get; set; }
}
