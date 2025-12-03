using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Invoice taxes
/// </summary>
public class InvoiceTaxesDto
{
    /// <summary>
    /// The invoice holder must withhold ISS or not
    /// </summary>
    [JsonPropertyName("retainIss")]
    public bool RetainIss { get; set; }

    /// <summary>
    /// COFINS rate
    /// </summary>
    [JsonPropertyName("cofins")]
    public double Cofins { get; set; }

    /// <summary>
    /// CSLL rate
    /// </summary>
    [JsonPropertyName("csll")]
    public double Csll { get; set; }

    /// <summary>
    /// INSS rate
    /// </summary>
    [JsonPropertyName("inss")]
    public double Inss { get; set; }

    /// <summary>
    /// IR rate
    /// </summary>
    [JsonPropertyName("ir")]
    public double Ir { get; set; }

    /// <summary>
    /// PIS rate
    /// </summary>
    [JsonPropertyName("pis")]
    public double Pis { get; set; }

    /// <summary>
    /// ISS rate
    /// </summary>
    [JsonPropertyName("iss")]
    public double Iss { get; set; }
}
