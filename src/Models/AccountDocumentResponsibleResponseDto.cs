using System.Text.Json.Serialization;
using Asaas.Sdk.Http;

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
    /// <remarks>
    /// The Asaas reference declares this field as an array, but the API answers with a single string
    /// (e.g. <c>"ASSOCIATION"</c>). Both shapes are read into this list.
    /// </remarks>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(SingleOrArrayStringListConverter))]
    public List<string>? Type { get; set; }
}
