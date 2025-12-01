using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Transfer bank account response
/// </summary>
public class TransferBankAccountGetResponseDto
{
    /// <summary>
    /// Bank information
    /// </summary>
    [JsonPropertyName("bank")]
    public TransferBankGetResponseDto? Bank { get; set; }

    /// <summary>
    /// Account holder name
    /// </summary>
    [JsonPropertyName("accountName")]
    public string? AccountName { get; set; }

    /// <summary>
    /// Account owner name
    /// </summary>
    [JsonPropertyName("ownerName")]
    public string? OwnerName { get; set; }

    /// <summary>
    /// CPF or CNPJ of the account owner
    /// </summary>
    [JsonPropertyName("cpfCnpj")]
    public string? CpfCnpj { get; set; }

    /// <summary>
    /// Bank agency
    /// </summary>
    [JsonPropertyName("agency")]
    public string? Agency { get; set; }

    /// <summary>
    /// Agency digit
    /// </summary>
    [JsonPropertyName("agencyDigit")]
    public string? AgencyDigit { get; set; }

    /// <summary>
    /// Account number
    /// </summary>
    [JsonPropertyName("account")]
    public string? Account { get; set; }

    /// <summary>
    /// Account digit
    /// </summary>
    [JsonPropertyName("accountDigit")]
    public string? AccountDigit { get; set; }

    /// <summary>
    /// Account type
    /// </summary>
    [JsonPropertyName("bankAccountType")]
    public string? BankAccountType { get; set; }

    /// <summary>
    /// Pix address key
    /// </summary>
    [JsonPropertyName("pixAddressKey")]
    public string? PixAddressKey { get; set; }
}
