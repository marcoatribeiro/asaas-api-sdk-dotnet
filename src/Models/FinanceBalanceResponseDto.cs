using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Finance balance response
/// </summary>
public class FinanceBalanceResponseDto
{
    /// <summary>
    /// Account balance
    /// </summary>
    [JsonPropertyName("balance")]
    public decimal? Balance { get; set; }
}
