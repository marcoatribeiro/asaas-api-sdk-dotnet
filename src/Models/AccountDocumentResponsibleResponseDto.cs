using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Who is responsible for sending these documents
/// </summary>
public class AccountDocumentResponsibleResponseDto
{
    /// <summary>
    /// Responsible name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Responsible type. Each item is one of <c>ALLOW_BANK_ACCOUNT_DEPOSIT_STATEMENT</c>,
    /// <c>ASAAS_ACCOUNT_OWNER_EMANCIPATION_AGE</c>, <c>ASAAS_ACCOUNT_OWNER</c>, <c>ASSOCIATION</c>,
    /// <c>BANK_ACCOUNT_OWNER_EMANCIPATION_AGE</c>, <c>BANK_ACCOUNT_OWNER</c>, <c>CUSTOM</c>, <c>DIRECTOR</c>,
    /// <c>INDIVIDUAL_COMPANY</c>, <c>LIMITED_COMPANY</c>, <c>MEI</c> or <c>PARTNER</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public List<string>? Type { get; set; }
}
