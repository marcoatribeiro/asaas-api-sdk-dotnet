using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Receiver of a recurring Pix transaction
/// </summary>
public class PixRecurringTransactionExternalAccountResponseDto
{
    /// <summary>
    /// Receiver name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Payment institution name
    /// </summary>
    [JsonPropertyName("financialInstitutionName")]
    public string? FinancialInstitutionName { get; set; }

    /// <summary>
    /// Receiver CPF or CNPJ
    /// </summary>
    [JsonPropertyName("cpfCnpj")]
    public string? CpfCnpj { get; set; }

    /// <summary>
    /// Receiver Pix key
    /// </summary>
    [JsonPropertyName("pixKey")]
    public string? PixKey { get; set; }
}
