using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account (subaccount) save response
/// </summary>
public class AccountSaveResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Unique subaccount identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Subaccount name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Subaccount email
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Email for subaccount login, if not provided, the subaccount email will be used
    /// </summary>
    [JsonPropertyName("loginEmail")]
    public string? LoginEmail { get; set; }

    /// <summary>
    /// Telephone
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Cellphone
    /// </summary>
    [JsonPropertyName("mobilePhone")]
    public string? MobilePhone { get; set; }

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
    /// Address zip code
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }

    /// <summary>
    /// CPF or CNPJ of the subaccount owner
    /// </summary>
    [JsonPropertyName("cpfCnpj")]
    public string? CpfCnpj { get; set; }

    /// <summary>
    /// Date of birth (only for Individuals)
    /// </summary>
    [JsonPropertyName("birthDate")]
    public string? BirthDate { get; set; }

    /// <summary>
    /// Kind of person
    /// </summary>
    [JsonPropertyName("personType")]
    public string? PersonType { get; set; }

    /// <summary>
    /// Type of company (only when Legal Entity)
    /// </summary>
    [JsonPropertyName("companyType")]
    public string? CompanyType { get; set; }

    /// <summary>
    /// Unique city identifier in Asaas
    /// </summary>
    [JsonPropertyName("city")]
    public long? City { get; set; }

    /// <summary>
    /// State abbreviation (SP, RJ, SC, ...)
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// Country (Fixed Brazil)
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>
    /// Display name (auto-populated)
    /// </summary>
    [JsonPropertyName("tradingName")]
    public string? TradingName { get; set; }

    /// <summary>
    /// Url reffered to the subaccount
    /// </summary>
    [JsonPropertyName("site")]
    public string? Site { get; set; }

    /// <summary>
    /// Unique wallet identifier to split charges or transfer between Asaas accounts
    /// </summary>
    [JsonPropertyName("walletId")]
    public string? WalletId { get; set; }

    /// <summary>
    /// Subaccount number in Asaas
    /// </summary>
    [JsonPropertyName("accountNumber")]
    public AccountNumberDto? AccountNumber { get; set; }

    /// <summary>
    /// Information about the expiration of commercial data
    /// </summary>
    [JsonPropertyName("commercialInfoExpiration")]
    public AccountInfoCommercialInfoExpirationResponseDto? CommercialInfoExpiration { get; set; }

    /// <summary>
    /// API key
    /// </summary>
    [JsonPropertyName("apiKey")]
    public string? ApiKey { get; set; }
}
