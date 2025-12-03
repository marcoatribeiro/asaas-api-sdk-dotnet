using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// State abbreviation (SP, RJ, SC, ...)
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AccountInfoCityState>))]
public enum AccountInfoCityState
{
    [EnumMember(Value = "AC")]
    AC,

    [EnumMember(Value = "AL")]
    AL,

    [EnumMember(Value = "AP")]
    AP,

    [EnumMember(Value = "AM")]
    AM,

    [EnumMember(Value = "BA")]
    BA,

    [EnumMember(Value = "CE")]
    CE,

    [EnumMember(Value = "DF")]
    DF,

    [EnumMember(Value = "ES")]
    ES,

    [EnumMember(Value = "GO")]
    GO,

    [EnumMember(Value = "MA")]
    MA,

    [EnumMember(Value = "MT")]
    MT,

    [EnumMember(Value = "MS")]
    MS,

    [EnumMember(Value = "MG")]
    MG,

    [EnumMember(Value = "PA")]
    PA,

    [EnumMember(Value = "PB")]
    PB,

    [EnumMember(Value = "PR")]
    PR,

    [EnumMember(Value = "PE")]
    PE,

    [EnumMember(Value = "PI")]
    PI,

    [EnumMember(Value = "RR")]
    RR,

    [EnumMember(Value = "RO")]
    RO,

    [EnumMember(Value = "RJ")]
    RJ,

    [EnumMember(Value = "RN")]
    RN,

    [EnumMember(Value = "RS")]
    RS,

    [EnumMember(Value = "SC")]
    SC,

    [EnumMember(Value = "SP")]
    SP,

    [EnumMember(Value = "SE")]
    SE,

    [EnumMember(Value = "TO")]
    TO
}
