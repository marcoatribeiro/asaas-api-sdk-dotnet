using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment chargeback information
/// </summary>
public class PaymentChargebackResponseDto
{
    /// <summary>
    /// Chargeback status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Chargeback reason
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}
