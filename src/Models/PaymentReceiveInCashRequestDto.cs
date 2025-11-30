using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment receive in cash request
/// </summary>
public class PaymentReceiveInCashRequestDto
{
    /// <summary>
    /// Payment date
    /// </summary>
    [JsonPropertyName("paymentDate")]
    public string? PaymentDate { get; set; }

    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Indicates if notification should be sent
    /// </summary>
    [JsonPropertyName("notifyCustomer")]
    public bool? NotifyCustomer { get; set; }
}
