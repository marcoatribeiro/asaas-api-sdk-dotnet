using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Type of company (only when Legal Entity)
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AccountInfoCompanyType>))]
public enum AccountInfoCompanyType
{
    [EnumMember(Value = "MEI")]
    Mei,

    [EnumMember(Value = "LIMITED")]
    Limited,

    [EnumMember(Value = "INDIVIDUAL")]
    Individual,

    [EnumMember(Value = "ASSOCIATION")]
    Association
}
