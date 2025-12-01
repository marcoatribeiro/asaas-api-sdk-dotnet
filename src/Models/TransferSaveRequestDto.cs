using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Transfer save request
/// </summary>
public class TransferSaveRequestDto
{
    /// <summary>
    /// Amount to be transferred
    /// </summary>
    [JsonPropertyName("value")]
    public double Value { get; set; }

    /// <summary>
    /// Enter your account details if it is a transfer to a bank account
    /// </summary>
    [JsonPropertyName("bankAccount")]
    public TransferBankAccountSaveRequestDto? BankAccount { get; set; }

    /// <summary>
    /// Transfer modality. If not informed and the receiving institution is a Pix participant, the transfer is via Pix. Otherwise via TED.
    /// </summary>
    [JsonPropertyName("operationType")]
    public string? OperationType { get; set; }

    /// <summary>
    /// Enter the Pix key if it is a transfer to a Pix key
    /// </summary>
    [JsonPropertyName("pixAddressKey")]
    public string? PixAddressKey { get; set; }

    /// <summary>
    /// Enter the type of key if it is a transfer to a Pix key (CPF, CNPJ, EMAIL, PHONE, EVP)
    /// </summary>
    [JsonPropertyName("pixAddressKeyType")]
    public string? PixAddressKeyType { get; set; }

    /// <summary>
    /// Transfers via Pix allow description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// For scheduled transfers, if not informed, payment is instantaneous
    /// </summary>
    [JsonPropertyName("scheduleDate")]
    public string? ScheduleDate { get; set; }

    /// <summary>
    /// Transfer identifier in your system
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Recurrence information. Only for Pix transfers
    /// </summary>
    [JsonPropertyName("recurring")]
    public TransferRecurringSaveRequestDto? Recurring { get; set; }
}
