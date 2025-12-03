using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Person type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AccountInfoPersonType>))]
public enum AccountInfoPersonType
{
    [EnumMember(Value = "JURIDICA")]
    Juridica,

    [EnumMember(Value = "FISICA")]
    Fisica
}
