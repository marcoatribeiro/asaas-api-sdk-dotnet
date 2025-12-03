using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account status
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AccountInfoStatus>))]
public enum AccountInfoStatus
{
    [EnumMember(Value = "APPROVED")]
    Approved,

    [EnumMember(Value = "AWAITING_ACTION_AUTHORIZATION")]
    AwaitingActionAuthorization,

    [EnumMember(Value = "DENIED")]
    Denied,

    [EnumMember(Value = "PENDING")]
    Pending
}
