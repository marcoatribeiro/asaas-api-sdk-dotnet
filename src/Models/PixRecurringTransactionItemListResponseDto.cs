using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Recurring Pix transaction item list response. The Asaas documents no pagination fields for this list.
/// </summary>
public class PixRecurringTransactionItemListResponseDto
{
    /// <summary>
    /// Items of the recurrence
    /// </summary>
    [JsonPropertyName("data")]
    public List<PixRecurringTransactionItemGetResponseDto> Data { get; set; } = new();
}
