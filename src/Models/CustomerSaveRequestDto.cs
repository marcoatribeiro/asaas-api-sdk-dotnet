using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Customer save request
/// </summary>
public class CustomerSaveRequestDto
{
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

    /// <summary>
    /// Postal code
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Address number
    /// </summary>
    [JsonPropertyName("addressNumber")]
    public string? AddressNumber { get; set; }

    /// <summary>
    /// Complement
    /// </summary>
    [JsonPropertyName("complement")]
    public string? Complement { get; set; }

    /// <summary>
    /// Province (neighborhood)
    /// </summary>
    [JsonPropertyName("province")]
    public string? Province { get; set; }

    /// <summary>
    /// External reference
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Indicates if notifications are disabled
    /// </summary>
    [JsonPropertyName("notificationDisabled")]
    public bool? NotificationDisabled { get; set; }

    /// <summary>
    /// Additional emails for notifications
    /// </summary>
    [JsonPropertyName("additionalEmails")]
    public string? AdditionalEmails { get; set; }

    /// <summary>
    /// Municipal inscription
    /// </summary>
    [JsonPropertyName("municipalInscription")]
    public string? MunicipalInscription { get; set; }

    /// <summary>
    /// State inscription
    /// </summary>
    [JsonPropertyName("stateInscription")]
    public string? StateInscription { get; set; }

    /// <summary>
    /// Group name
    /// </summary>
    [JsonPropertyName("groupName")]
    public string? GroupName { get; set; }

    /// <summary>
    /// Company
    /// </summary>
    [JsonPropertyName("company")]
    public string? Company { get; set; }
}
