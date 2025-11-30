using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment save request
/// </summary>
public class PaymentSaveRequestDto
{
    /// <summary>
    /// Customer identifier
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    /// <summary>
    /// Due date
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// External reference
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }
}
