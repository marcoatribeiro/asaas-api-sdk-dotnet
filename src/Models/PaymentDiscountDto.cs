using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment discount information
/// </summary>
public class PaymentDiscountDto
{
    /// <summary>
    /// Discount value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Discount due date limit
    /// </summary>
    [JsonPropertyName("dueDateLimitDays")]
    public int? DueDateLimitDays { get; set; }

    /// <summary>
    /// Discount type
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
