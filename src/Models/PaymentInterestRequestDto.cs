using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Interest information for payment after due date
/// </summary>
public class PaymentInterestRequestDto
{
    /// <summary>
    /// Percentage of interest per month on the amount charged for payment after maturity
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }
}
