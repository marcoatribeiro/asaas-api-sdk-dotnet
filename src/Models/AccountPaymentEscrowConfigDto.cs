using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account payment escrow configuration
/// </summary>
public class AccountPaymentEscrowConfigDto
{
    /// <summary>
    /// Number of days to expire the payment escrow
    /// </summary>
    [JsonPropertyName("daysToExpire")]
    public long DaysToExpire { get; set; }

    /// <summary>
    /// Indicates whether the Escrow Account is enabled
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// Indicates whether the subaccount is responsible for paying the Escrow Account fee. If not informed, the main account will be responsible for the fee
    /// </summary>
    [JsonPropertyName("isFeePayer")]
    public bool? IsFeePayer { get; set; }
}
