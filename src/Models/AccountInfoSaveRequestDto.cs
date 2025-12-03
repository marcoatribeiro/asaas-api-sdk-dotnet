using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account info save/update request
/// </summary>
public class AccountInfoSaveRequestDto
{
    /// <summary>
    /// Person Type
    /// </summary>
    [JsonPropertyName("personType")]
    public AccountInfoPersonType? PersonType { get; set; }

    /// <summary>
    /// CPF or CNPJ of the account owner
    /// </summary>
    [JsonPropertyName("cpfCnpj")]
    public string? CpfCnpj { get; set; }

    /// <summary>
    /// Birthday (Required if the information is from an individual)
    /// </summary>
    [JsonPropertyName("birthDate")]
    public string? BirthDate { get; set; }

    /// <summary>
    /// Type of company (only when Legal Entity)
    /// </summary>
    [JsonPropertyName("companyType")]
    public AccountInfoCompanyType? CompanyType { get; set; }

    /// <summary>
    /// Company Name
    /// </summary>
    [JsonPropertyName("companyName")]
    public string? CompanyName { get; set; }

    /// <summary>
    /// Billing/Monthly income
    /// </summary>
    [JsonPropertyName("incomeValue")]
    public double? IncomeValue { get; set; }

    /// <summary>
    /// Account's email
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Telephone
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Cell phone
    /// </summary>
    [JsonPropertyName("mobilePhone")]
    public string? MobilePhone { get; set; }

    /// <summary>
    /// Web site
    /// </summary>
    [JsonPropertyName("site")]
    public string? Site { get; set; }

    /// <summary>
    /// Address zip code
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Public place
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Address number
    /// </summary>
    [JsonPropertyName("addressNumber")]
    public string? AddressNumber { get; set; }

    /// <summary>
    /// Address complement
    /// </summary>
    [JsonPropertyName("complement")]
    public string? Complement { get; set; }

    /// <summary>
    /// Neighborhood
    /// </summary>
    [JsonPropertyName("province")]
    public string? Province { get; set; }
}
