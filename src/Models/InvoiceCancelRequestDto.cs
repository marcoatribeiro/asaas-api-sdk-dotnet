using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Invoice cancel request
/// </summary>
public class InvoiceCancelRequestDto
{
    /// <summary>
    /// Cancel invoice only on Asaas
    /// </summary>
    [JsonPropertyName("cancelOnlyOnAsaas")]
    public bool? CancelOnlyOnAsaas { get; set; }
}
