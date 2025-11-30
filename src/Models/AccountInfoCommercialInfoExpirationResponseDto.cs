using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Account commercial information expiration response
/// </summary>
public class AccountInfoCommercialInfoExpirationResponseDto
{
    /// <summary>
    /// Expiration date
    /// </summary>
    [JsonPropertyName("expirationDate")]
    public string? ExpirationDate { get; set; }

    /// <summary>
    /// Days until expiration
    /// </summary>
    [JsonPropertyName("daysUntilExpiration")]
    public int? DaysUntilExpiration { get; set; }

    /// <summary>
    /// Indicates if commercial info is expired
    /// </summary>
    [JsonPropertyName("expired")]
    public bool? Expired { get; set; }
}
