using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Original transaction of a refund
/// </summary>
public class PixTransactionOriginalTransactionResponseDto
{
    /// <summary>
    /// Transaction identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Central Bank transaction identifier
    /// </summary>
    [JsonPropertyName("endToEndIdentifier")]
    public string? EndToEndIdentifier { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Date, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("effectiveDate")]
    public string? EffectiveDate { get; set; }
}
