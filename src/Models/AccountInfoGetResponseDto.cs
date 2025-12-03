using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account info get response
/// </summary>
public class AccountInfoGetResponseDto
{
    /// <summary>
    /// Account status
    /// </summary>
    [JsonPropertyName("status")]
    public AccountInfoStatus? Status { get; set; }

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
    /// Account owner name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Birthday (Required if the information is from an individual)
    /// </summary>
    [JsonPropertyName("birthDate")]
    public string? BirthDate { get; set; }

    /// <summary>
    /// Company Name
    /// </summary>
    [JsonPropertyName("companyName")]
    public string? CompanyName { get; set; }

    /// <summary>
    /// Type of company (only when Legal Entity)
    /// </summary>
    [JsonPropertyName("companyType")]
    public AccountInfoCompanyType? CompanyType { get; set; }

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

    /// <summary>
    /// City information registered in your account
    /// </summary>
    [JsonPropertyName("city")]
    public AccountInfoCityDto? City { get; set; }

    /// <summary>
    /// Reason why it is necessary to resend the information
    /// </summary>
    [JsonPropertyName("denialReason")]
    public string? DenialReason { get; set; }

    /// <summary>
    /// Display name (auto-populated)
    /// </summary>
    [JsonPropertyName("tradingName")]
    public string? TradingName { get; set; }

    /// <summary>
    /// Web site
    /// </summary>
    [JsonPropertyName("site")]
    public string? Site { get; set; }

    /// <summary>
    /// Company names available. Only filled in for Legal Entity type accounts.
    /// </summary>
    [JsonPropertyName("availableCompanyNames")]
    public List<string>? AvailableCompanyNames { get; set; }

    /// <summary>
    /// Information about the expiration of commercial data
    /// </summary>
    [JsonPropertyName("commercialInfoExpiration")]
    public AccountInfoCommercialInfoExpirationResponseDto? CommercialInfoExpiration { get; set; }
}
