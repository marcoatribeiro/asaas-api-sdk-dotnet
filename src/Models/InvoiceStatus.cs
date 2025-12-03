using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Invoice status
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<InvoiceStatus>))]
public enum InvoiceStatus
{
    [EnumMember(Value = "SCHEDULED")]
    Scheduled,

    [EnumMember(Value = "AUTHORIZED")]
    Authorized,

    [EnumMember(Value = "PROCESSING_CANCELLATION")]
    ProcessingCancellation,

    [EnumMember(Value = "CANCELED")]
    Canceled,

    [EnumMember(Value = "CANCELLATION_DENIED")]
    CancellationDenied,

    [EnumMember(Value = "ERROR")]
    Error
}
