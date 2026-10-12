using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Pix transaction
/// </summary>
public class PixTransactionGetResponseDto
{
    /// <summary>
    /// Transaction identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Central Bank transaction identifier
    /// </summary>
    [JsonPropertyName("endToEndIdentifier")]
    public string? EndToEndIdentifier { get; set; }

    /// <summary>
    /// Finality: <c>WITHDRAWAL</c> or <c>CHANGE</c>
    /// </summary>
    [JsonPropertyName("finality")]
    public string? Finality { get; set; }

    /// <summary>
    /// Transaction value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Change value
    /// </summary>
    [JsonPropertyName("changeValue")]
    public decimal? ChangeValue { get; set; }

    /// <summary>
    /// Refunded value
    /// </summary>
    [JsonPropertyName("refundedValue")]
    public decimal? RefundedValue { get; set; }

    /// <summary>
    /// Transaction date and time
    /// </summary>
    [JsonPropertyName("effectiveDate")]
    public string? EffectiveDate { get; set; }

    /// <summary>
    /// Scheduled date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("scheduledDate")]
    public string? ScheduledDate { get; set; }

    /// <summary>
    /// Status: <c>AWAITING_BALANCE_VALIDATION</c>, <c>AWAITING_INSTANT_PAYMENT_ACCOUNT_BALANCE</c>, <c>AWAITING_CRITICAL_ACTION_AUTHORIZATION</c>, <c>AWAITING_CHECKOUT_RISK_ANALYSIS_REQUEST</c>, <c>AWAITING_CASH_IN_RISK_ANALYSIS_REQUEST</c>, <c>SCHEDULED</c>, <c>AWAITING_REQUEST</c>, <c>REQUESTED</c>, <c>DONE</c>, <c>REFUSED</c> or <c>CANCELLED</c>. Treat other values as possible.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Type: <c>DEBIT</c>, <c>CREDIT</c>, <c>CREDIT_REFUND</c>, <c>DEBIT_REFUND</c> or <c>DEBIT_REFUND_CANCELLATION</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Origin: <c>MANUAL</c>, <c>ADDRESS_KEY</c>, <c>STATIC_QRCODE</c>, <c>DYNAMIC_QRCODE</c>, <c>PAYMENT_INITIATION_SERVICE</c> or <c>AUTOMATIC_RECURRING</c>
    /// </summary>
    [JsonPropertyName("originType")]
    public string? OriginType { get; set; }

    /// <summary>
    /// Identifier of the QR Code linked to the transaction
    /// </summary>
    [JsonPropertyName("conciliationIdentifier")]
    public string? ConciliationIdentifier { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Receipt URL, available after confirmation
    /// </summary>
    [JsonPropertyName("transactionReceiptUrl")]
    public string? TransactionReceiptUrl { get; set; }

    /// <summary>
    /// Why the transaction was refused
    /// </summary>
    [JsonPropertyName("refusalReason")]
    public string? RefusalReason { get; set; }

    /// <summary>
    /// Whether the transaction can be cancelled
    /// </summary>
    [JsonPropertyName("canBeCanceled")]
    public bool? CanBeCanceled { get; set; }

    /// <summary>
    /// Original transaction, for a refund
    /// </summary>
    [JsonPropertyName("originalTransaction")]
    public PixTransactionOriginalTransactionResponseDto? OriginalTransaction { get; set; }

    /// <summary>
    /// Counterparty
    /// </summary>
    [JsonPropertyName("externalAccount")]
    public PixTransactionExternalAccountResponseDto? ExternalAccount { get; set; }

    /// <summary>
    /// QR Code data
    /// </summary>
    [JsonPropertyName("qrCode")]
    public PixTransactionQrCodeResponseDto? QrCode { get; set; }

    /// <summary>
    /// Charge identifier
    /// </summary>
    [JsonPropertyName("payment")]
    public string? Payment { get; set; }

    /// <summary>
    /// Whether the transaction can be refunded
    /// </summary>
    [JsonPropertyName("canBeRefunded")]
    public bool? CanBeRefunded { get; set; }

    /// <summary>
    /// Why a refund is disabled
    /// </summary>
    [JsonPropertyName("refundDisabledReason")]
    public string? RefundDisabledReason { get; set; }

    /// <summary>
    /// Debit or credit fee
    /// </summary>
    [JsonPropertyName("chargedFeeValue")]
    public decimal? ChargedFeeValue { get; set; }

    /// <summary>
    /// Creation date and time
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Pix key, for a credit
    /// </summary>
    [JsonPropertyName("addressKey")]
    public string? AddressKey { get; set; }

    /// <summary>
    /// Pix key type: <c>CPF</c>, <c>CNPJ</c>, <c>EMAIL</c>, <c>PHONE</c> or <c>EVP</c>
    /// </summary>
    [JsonPropertyName("addressKeyType")]
    public string? AddressKeyType { get; set; }

    /// <summary>
    /// Transfer identifier
    /// </summary>
    [JsonPropertyName("transferId")]
    public string? TransferId { get; set; }

    /// <summary>
    /// Free search field
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }
}
