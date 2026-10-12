using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment chargeback information
/// </summary>
public class PaymentChargebackResponseDto
{
    /// <summary>
    /// Chargeback identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Identifier of the payment
    /// </summary>
    [JsonPropertyName("payment")]
    public string? Payment { get; set; }

    /// <summary>
    /// Identifier of the installment
    /// </summary>
    [JsonPropertyName("installment")]
    public string? Installment { get; set; }

    /// <summary>
    /// Identifier of the customer linked to the chargeback
    /// </summary>
    [JsonPropertyName("customerAccount")]
    public string? CustomerAccount { get; set; }

    /// <summary>
    /// Chargeback status (<c>REQUESTED</c>, <c>IN_DISPUTE</c>, <c>DISPUTE_LOST</c>, <c>REVERSED</c> or <c>DONE</c>)
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Chargeback reason (e.g. <c>COMMERCIAL_DISAGREEMENT</c>, <c>FRAUD</c>, <c>SERVICE_NOT_RECEIVED</c>)
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    /// <summary>
    /// Date the chargeback was opened (<c>yyyy-MM-dd</c>)
    /// </summary>
    [JsonPropertyName("disputeStartDate")]
    public string? DisputeStartDate { get; set; }

    /// <summary>
    /// Chargeback amount
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Date the payment was settled in Asaas (<c>yyyy-MM-dd</c>)
    /// </summary>
    [JsonPropertyName("paymentDate")]
    public string? PaymentDate { get; set; }

    /// <summary>
    /// Credit card involved
    /// </summary>
    [JsonPropertyName("creditCard")]
    public ChargebackCreditCardResponseDto? CreditCard { get; set; }

    /// <summary>
    /// Dispute status (<c>REQUESTED</c>, <c>ACCEPTED</c> or <c>REJECTED</c>). It is not the chargeback
    /// <see cref="Status"/>, and has its own set of values.
    /// </summary>
    [JsonPropertyName("disputeStatus")]
    public string? DisputeStatus { get; set; }

    /// <summary>
    /// Deadline for sending the dispute documents (<c>yyyy-MM-dd</c>)
    /// </summary>
    [JsonPropertyName("deadlineToSendDisputeDocuments")]
    public string? DeadlineToSendDisputeDocuments { get; set; }
}
