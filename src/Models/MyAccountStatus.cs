using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Status of sent business data
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<MyAccountStatus>))]
public enum MyAccountStatus
{
    [EnumMember(Value = "PENDING")]
    Pending,

    [EnumMember(Value = "APPROVED")]
    Approved,

    [EnumMember(Value = "REJECTED")]
    Rejected,

    [EnumMember(Value = "AWAITING_APPROVAL")]
    AwaitingApproval
}
