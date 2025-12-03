using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Type of authentication required at city hall
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<FiscalInfoAuthenticationType>))]
public enum FiscalInfoAuthenticationType
{
    [EnumMember(Value = "CERTIFICATE")]
    Certificate,

    [EnumMember(Value = "TOKEN")]
    Token,

    [EnumMember(Value = "USER_AND_PASSWORD")]
    UserAndPassword
}
