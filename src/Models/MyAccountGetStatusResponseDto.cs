using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// My account status response
/// </summary>
public class MyAccountGetStatusResponseDto
{
    /// <summary>
    /// Unique account identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Status of the sent business data. One of <c>PENDING</c>, <c>APPROVED</c>, <c>REJECTED</c> or
    /// <c>AWAITING_APPROVAL</c>.
    /// </summary>
    [JsonPropertyName("commercialInfo")]
    public string? CommercialInfo { get; set; }

    /// <summary>
    /// Status of the sent bank account data. One of <c>PENDING</c>, <c>APPROVED</c>, <c>REJECTED</c> or
    /// <c>AWAITING_APPROVAL</c>.
    /// </summary>
    [JsonPropertyName("bankAccountInfo")]
    public string? BankAccountInfo { get; set; }

    /// <summary>
    /// Status of the sent documentation. One of <c>PENDING</c>, <c>APPROVED</c>, <c>REJECTED</c> or
    /// <c>AWAITING_APPROVAL</c>.
    /// </summary>
    [JsonPropertyName("documentation")]
    public string? Documentation { get; set; }

    /// <summary>
    /// Overall status of the account registration. One of <c>PENDING</c>, <c>APPROVED</c>, <c>REJECTED</c> or
    /// <c>AWAITING_APPROVAL</c>.
    /// </summary>
    [JsonPropertyName("general")]
    public string? General { get; set; }
}
