using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Invoice type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<InvoiceType>))]
public enum InvoiceType
{
    [EnumMember(Value = "NFS-e")]
    NfSe
}
