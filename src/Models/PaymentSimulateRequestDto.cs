using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment simulate request
/// </summary>
public class PaymentSimulateRequestDto
{
    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Due date
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }
}
