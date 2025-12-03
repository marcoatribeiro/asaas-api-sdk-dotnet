using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Document type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AccountDocumentType>))]
public enum AccountDocumentType
{
    [EnumMember(Value = "IDENTIFICATION")]
    Identification,

    [EnumMember(Value = "SOCIAL_CONTRACT")]
    SocialContract,

    [EnumMember(Value = "ENTREPRENEUR_REQUIREMENT")]
    EntrepreneurRequirement,

    [EnumMember(Value = "MINUTES_OF_ELECTION")]
    MinutesOfElection,

    [EnumMember(Value = "CUSTOM")]
    Custom
}
