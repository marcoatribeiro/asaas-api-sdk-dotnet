using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Response of a chargeback dispute
/// </summary>
public class ChargebackDisputeResponseDto
{
    /// <summary>
    /// Identifier of the chargeback the dispute was created for
    /// </summary>
    [JsonPropertyName("chargebackId")]
    public string? ChargebackId { get; set; }

    /// <summary>
    /// Dispute status (<c>REQUESTED</c>, <c>ACCEPTED</c> or <c>REJECTED</c>). Do not compare it with the chargeback status,
    /// which has other values.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Names of the files submitted for the dispute
    /// </summary>
    [JsonPropertyName("files")]
    public List<string> Files { get; set; } = new();
}
