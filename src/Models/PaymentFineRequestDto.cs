using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Fine information for payment after due date
/// </summary>
public class PaymentFineRequestDto
{
    /// <summary>
    /// Percentage of fine on the amount of the charge for payment after the due date
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Fine type (FIXED or PERCENTAGE)
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
