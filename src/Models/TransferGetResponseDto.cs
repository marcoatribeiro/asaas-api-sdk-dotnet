using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Transfer response
/// </summary>
public class TransferGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Unique transfer identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Type of transfer (BANK_ACCOUNT, ASAAS_ACCOUNT)
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Transfer request date
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    /// <summary>
    /// Net value minus transfer fee
    /// </summary>
    [JsonPropertyName("netValue")]
    public double? NetValue { get; set; }

    /// <summary>
    /// Transfer status (PENDING, BANK_PROCESSING, DONE, CANCELLED, FAILED)
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Transfer rate
    /// </summary>
    [JsonPropertyName("transferFee")]
    public double? TransferFee { get; set; }

    /// <summary>
    /// Effective date
    /// </summary>
    [JsonPropertyName("effectiveDate")]
    public string? EffectiveDate { get; set; }

    /// <summary>
    /// Schedule date
    /// </summary>
    [JsonPropertyName("scheduleDate")]
    public string? ScheduleDate { get; set; }

    /// <summary>
    /// Unique identifier of the Pix transaction at the Central Bank
    /// </summary>
    [JsonPropertyName("endToEndIdentifier")]
    public string? EndToEndIdentifier { get; set; }

    /// <summary>
    /// False when awaiting authorization via SMS Token
    /// </summary>
    [JsonPropertyName("authorized")]
    public bool? Authorized { get; set; }

    /// <summary>
    /// Reason for transfer failure
    /// </summary>
    [JsonPropertyName("failReason")]
    public string? FailReason { get; set; }

    /// <summary>
    /// Transfer identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Proof of transfer will be available after the transfer is confirmed
    /// </summary>
    [JsonPropertyName("transactionReceiptUrl")]
    public string? TransactionReceiptUrl { get; set; }

    /// <summary>
    /// Transfer method (PIX, TED, INTERNAL)
    /// </summary>
    [JsonPropertyName("operationType")]
    public string? OperationType { get; set; }

    /// <summary>
    /// Transfer description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Recurring transfer identifier
    /// </summary>
    [JsonPropertyName("recurring")]
    public string? Recurring { get; set; }

    /// <summary>
    /// Bank account information
    /// </summary>
    [JsonPropertyName("bankAccount")]
    public TransferBankAccountGetResponseDto? BankAccount { get; set; }
}
