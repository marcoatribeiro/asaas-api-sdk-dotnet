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
    /// Status of sent business data
    /// </summary>
    [JsonPropertyName("commercialInfo")]
    public MyAccountStatus? CommercialInfo { get; set; }

    /// <summary>
    /// Status of sent business data
    /// </summary>
    [JsonPropertyName("bankAccountInfo")]
    public MyAccountStatus? BankAccountInfo { get; set; }

    /// <summary>
    /// Status of sent business data
    /// </summary>
    [JsonPropertyName("documentation")]
    public MyAccountStatus? Documentation { get; set; }

    /// <summary>
    /// Status of sent business data
    /// </summary>
    [JsonPropertyName("general")]
    public MyAccountStatus? General { get; set; }
}
