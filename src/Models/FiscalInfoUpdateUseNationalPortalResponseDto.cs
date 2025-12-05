using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Fiscal info update use national portal response
/// </summary>
public class FiscalInfoUpdateUseNationalPortalResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Whether the national portal is being used
    /// </summary>
    [JsonPropertyName("useNationalPortal")]
    public bool? UseNationalPortal { get; set; }

    /// <summary>
    /// Success indicator
    /// </summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }
}
