using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment details response
/// </summary>
public class PaymentGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Unique payment identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Payment creation date
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Unique identifier of the customer to whom the payment belongs
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Unique subscription identifier (when recurring billing)
    /// </summary>
    [JsonPropertyName("subscription")]
    public string? Subscription { get; set; }

    /// <summary>
    /// Unique installment identifier (when installment payment)
    /// </summary>
    [JsonPropertyName("installment")]
    public string? Installment { get; set; }

    /// <summary>
    /// Unique checkout identifier
    /// </summary>
    [JsonPropertyName("checkoutSession")]
    public string? CheckoutSession { get; set; }

    /// <summary>
    /// Unique identifier of the payments link to which the payment belongs
    /// </summary>
    [JsonPropertyName("paymentLink")]
    public string? PaymentLink { get; set; }

    /// <summary>
    /// Payment due date
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Net value after fees
    /// </summary>
    [JsonPropertyName("netValue")]
    public decimal? NetValue { get; set; }

    /// <summary>
    /// Original amount of charge (filled when paid with interest and fine)
    /// </summary>
    [JsonPropertyName("originalValue")]
    public decimal? OriginalValue { get; set; }

    /// <summary>
    /// Calculated amount of interest and fine that must be paid after the charge is due
    /// </summary>
    [JsonPropertyName("interestValue")]
    public decimal? InterestValue { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Credit card information
    /// </summary>
    [JsonPropertyName("creditCard")]
    public PaymentSaveWithCreditCardCreditCardDto? CreditCard { get; set; }

    /// <summary>
    /// Informs whether the charge can be paid after the due date (Only for bank slip)
    /// </summary>
    [JsonPropertyName("canBePaidAfterDueDate")]
    public bool? CanBePaidAfterDueDate { get; set; }

    /// <summary>
    /// Unique identifier of the Pix transaction to which the payment belongs
    /// </summary>
    [JsonPropertyName("pixTransaction")]
    public string? PixTransaction { get; set; }

    /// <summary>
    /// Unique identifier of the static QrCode generated for a given Pix key
    /// </summary>
    [JsonPropertyName("pixQrCodeId")]
    public string? PixQrCodeId { get; set; }

    /// <summary>
    /// Payment status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Payment description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// External reference
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Original due date upon creation of the payment
    /// </summary>
    [JsonPropertyName("originalDueDate")]
    public string? OriginalDueDate { get; set; }

    /// <summary>
    /// Payment date on Asaas
    /// </summary>
    [JsonPropertyName("paymentDate")]
    public string? PaymentDate { get; set; }

    /// <summary>
    /// Date on which the customer paid the bank slip
    /// </summary>
    [JsonPropertyName("clientPaymentDate")]
    public string? ClientPaymentDate { get; set; }

    /// <summary>
    /// Parcel number
    /// </summary>
    [JsonPropertyName("installmentNumber")]
    public long? InstallmentNumber { get; set; }

    /// <summary>
    /// Confirmed date
    /// </summary>
    [JsonPropertyName("confirmedDate")]
    public string? ConfirmedDate { get; set; }

    /// <summary>
    /// Invoice URL
    /// </summary>
    [JsonPropertyName("invoiceUrl")]
    public string? InvoiceUrl { get; set; }

    /// <summary>
    /// Bill number
    /// </summary>
    [JsonPropertyName("invoiceNumber")]
    public string? InvoiceNumber { get; set; }

    /// <summary>
    /// Determines if the payment has been removed
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    /// <summary>
    /// Defines whether the charge was anticipated or is in the process of being anticipated
    /// </summary>
    [JsonPropertyName("anticipated")]
    public bool? Anticipated { get; set; }

    /// <summary>
    /// Determines whether the charge is anticipated
    /// </summary>
    [JsonPropertyName("anticipable")]
    public bool? Anticipable { get; set; }

    /// <summary>
    /// Date when the credit became available
    /// </summary>
    [JsonPropertyName("creditDate")]
    public string? CreditDate { get; set; }

    /// <summary>
    /// Estimated date when the credit will be available
    /// </summary>
    [JsonPropertyName("estimatedCreditDate")]
    public string? EstimatedCreditDate { get; set; }

    /// <summary>
    /// Transaction receipt URL
    /// </summary>
    [JsonPropertyName("transactionReceiptUrl")]
    public string? TransactionReceiptUrl { get; set; }

    /// <summary>
    /// Unique identification of the bank slip
    /// </summary>
    [JsonPropertyName("nossoNumero")]
    public string? NossoNumero { get; set; }

    /// <summary>
    /// Bank slip URL
    /// </summary>
    [JsonPropertyName("bankSlipUrl")]
    public string? BankSlipUrl { get; set; }

    /// <summary>
    /// Discount information
    /// </summary>
    [JsonPropertyName("discount")]
    public PaymentDiscountDto? Discount { get; set; }

    /// <summary>
    /// Fine information for payment after due date
    /// </summary>
    [JsonPropertyName("fine")]
    public PaymentFineResponseDto? Fine { get; set; }

    /// <summary>
    /// Interest information for payment after due date
    /// </summary>
    [JsonPropertyName("interest")]
    public PaymentInterestResponseDto? Interest { get; set; }

    /// <summary>
    /// Split Settings
    /// </summary>
    [JsonPropertyName("split")]
    public List<PaymentSplitGetResponseDto>? Split { get; set; }

    /// <summary>
    /// Define whether the payment will be sent via post
    /// </summary>
    [JsonPropertyName("postalService")]
    public bool? PostalService { get; set; }

    /// <summary>
    /// Days after registration cancellation deadline (only for bank slip)
    /// </summary>
    [JsonPropertyName("daysAfterDueDateToRegistrationCancellation")]
    public long? DaysAfterDueDateToRegistrationCancellation { get; set; }

    /// <summary>
    /// Chargeback information
    /// </summary>
    [JsonPropertyName("chargeback")]
    public PaymentChargebackResponseDto? Chargeback { get; set; }

    /// <summary>
    /// Payment escrow in the Escrow Account information
    /// </summary>
    [JsonPropertyName("escrow")]
    public PaymentEscrowGetResponseDto? Escrow { get; set; }

    /// <summary>
    /// Refunds information
    /// </summary>
    [JsonPropertyName("refunds")]
    public List<PaymentRefundGetResponseDto>? Refunds { get; set; }
}
