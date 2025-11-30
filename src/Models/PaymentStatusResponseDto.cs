using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payment status response
/// </summary>
public class PaymentStatusResponseDto
{
    /// <summary>
    /// Payment identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Payment status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Nosso numero
    /// </summary>
    [JsonPropertyName("nossoNumero")]
    public string? NossoNumero { get; set; }
}
