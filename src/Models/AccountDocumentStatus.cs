using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Document approval status
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AccountDocumentStatus>))]
public enum AccountDocumentStatus
{
    [EnumMember(Value = "NOT_SENT")]
    NotSent,

    [EnumMember(Value = "PENDING")]
    Pending,

    [EnumMember(Value = "APPROVED")]
    Approved,

    [EnumMember(Value = "REJECTED")]
    Rejected,

    [EnumMember(Value = "IGNORED")]
    Ignored
}
