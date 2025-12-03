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
    /// Responsible type
    /// </summary>
    [JsonPropertyName("type")]
    public List<AccountDocumentResponsibleType>? Type { get; set; }
}
