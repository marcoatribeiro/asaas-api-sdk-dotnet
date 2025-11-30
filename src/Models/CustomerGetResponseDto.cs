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

    /// <summary>
    /// Customer address
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Customer address number
    /// </summary>
    [JsonPropertyName("addressNumber")]
    public string? AddressNumber { get; set; }

    /// <summary>
    /// Customer address complement
    /// </summary>
    [JsonPropertyName("complement")]
    public string? Complement { get; set; }

    /// <summary>
    /// Customer address neighborhood
    /// </summary>
    [JsonPropertyName("province")]
    public string? Province { get; set; }

    /// <summary>
    /// Unique city identifier in Asaas
    /// </summary>
    [JsonPropertyName("city")]
    public long? City { get; set; }

    /// <summary>
    /// City of customer address
    /// </summary>
    [JsonPropertyName("cityName")]
    public string? CityName { get; set; }

    /// <summary>
    /// Customer address status
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// Customer country
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>
    /// Customer address zip code
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Kind of person
    /// </summary>
    [JsonPropertyName("personType")]
    public string? PersonType { get; set; }

    /// <summary>
    /// Indicates whether it is a deleted client
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    /// <summary>
    /// Additional customer emails
    /// </summary>
    [JsonPropertyName("additionalEmails")]
    public string? AdditionalEmails { get; set; }

    /// <summary>
    /// External customer reference
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Indicates whether notifications are disabled
    /// </summary>
    [JsonPropertyName("notificationDisabled")]
    public bool? NotificationDisabled { get; set; }

    /// <summary>
    /// Customer Observations
    /// </summary>
    [JsonPropertyName("observations")]
    public string? Observations { get; set; }

    /// <summary>
    /// Indicates if it's non-brazilian customer
    /// </summary>
    [JsonPropertyName("foreignCustomer")]
    public bool? ForeignCustomer { get; set; }
}
