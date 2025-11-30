using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment viewing information response
/// </summary>
public class PaymentViewingInfoResponseDto
{
    /// <summary>
    /// Payment identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Customer name
    /// </summary>
    [JsonPropertyName("customerName")]
    public string? CustomerName { get; set; }

    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Due date
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
