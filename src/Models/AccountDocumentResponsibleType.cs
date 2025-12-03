using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Responsible type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AccountDocumentResponsibleType>))]
public enum AccountDocumentResponsibleType
{
    [EnumMember(Value = "ALLOW_BANK_ACCOUNT_DEPOSIT_STATEMENT")]
    AllowBankAccountDepositStatement,

    [EnumMember(Value = "ASAAS_ACCOUNT_OWNER_EMANCIPATION_AGE")]
    AsaasAccountOwnerEmancipationAge,

    [EnumMember(Value = "ASAAS_ACCOUNT_OWNER")]
    AsaasAccountOwner,

    [EnumMember(Value = "ASSOCIATION")]
    Association,

    [EnumMember(Value = "BANK_ACCOUNT_OWNER_EMANCIPATION_AGE")]
    BankAccountOwnerEmancipationAge,

    [EnumMember(Value = "BANK_ACCOUNT_OWNER")]
    BankAccountOwner,

    [EnumMember(Value = "CUSTOM")]
    Custom,

    [EnumMember(Value = "DIRECTOR")]
    Director,

    [EnumMember(Value = "INDIVIDUAL_COMPANY")]
    IndividualCompany,

    [EnumMember(Value = "LIMITED_COMPANY")]
    LimitedCompany,

    [EnumMember(Value = "MEI")]
    Mei,

    [EnumMember(Value = "PARTNER")]
    Partner
}
