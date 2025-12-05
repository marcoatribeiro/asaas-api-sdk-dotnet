using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Fiscal info update use national portal request
/// </summary>
public class FiscalInfoUpdateUseNationalPortalRequestDto
{
    /// <summary>
    /// Whether to use the national portal for invoice issuing
    /// </summary>
    [JsonPropertyName("useNationalPortal")]
    public bool? UseNationalPortal { get; set; }
}
