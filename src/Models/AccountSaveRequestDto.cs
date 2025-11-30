using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account (subaccount) save request
/// </summary>
public class AccountSaveRequestDto
{
    /// <summary>
    /// Subaccount name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Subaccount email
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// CPF or CNPJ of the subaccount owner
    /// </summary>
    [JsonPropertyName("cpfCnpj")]
    public string CpfCnpj { get; set; } = string.Empty;

    /// <summary>
    /// Cellphone
    /// </summary>
    [JsonPropertyName("mobilePhone")]
    public string MobilePhone { get; set; } = string.Empty;

    /// <summary>
    /// Billing/Monthly income
    /// </summary>
    [JsonPropertyName("incomeValue")]
    public double IncomeValue { get; set; }

    /// <summary>
    /// Public place
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Address number
    /// </summary>
    [JsonPropertyName("addressNumber")]
    public string AddressNumber { get; set; } = string.Empty;

    /// <summary>
    /// Neighborhood
    /// </summary>
    [JsonPropertyName("province")]
    public string Province { get; set; } = string.Empty;

    /// <summary>
    /// Address zip code
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    /// Email for subaccount login, if not provided, the subaccount email will be used
    /// </summary>
    [JsonPropertyName("loginEmail")]
    public string? LoginEmail { get; set; }

    /// <summary>
    /// Date of birth (only for Individuals)
    /// </summary>
    [JsonPropertyName("birthDate")]
    public string? BirthDate { get; set; }

    /// <summary>
    /// Type of company (only when Legal Entity)
    /// </summary>
    [JsonPropertyName("companyType")]
    public string? CompanyType { get; set; }

    /// <summary>
    /// Telephone
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Url reffered to the subaccount
    /// </summary>
    [JsonPropertyName("site")]
    public string? Site { get; set; }

    /// <summary>
    /// Address complement
    /// </summary>
    [JsonPropertyName("complement")]
    public string? Complement { get; set; }

    /// <summary>
    /// Array with desired Webhooks settings
    /// </summary>
    [JsonPropertyName("webhooks")]
    public List<WebhookConfigSaveRequestDto>? Webhooks { get; set; }
}
