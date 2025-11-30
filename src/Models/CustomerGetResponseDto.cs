using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Customer response
/// </summary>
public class CustomerGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Customer identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Date created
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Customer name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// CPF or CNPJ
    /// </summary>
    [JsonPropertyName("cpfCnpj")]
    public string? CpfCnpj { get; set; }

    /// <summary>
    /// Phone
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Mobile phone
    /// </summary>
    [JsonPropertyName("mobilePhone")]
    public string? MobilePhone { get; set; }
}
