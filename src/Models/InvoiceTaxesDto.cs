using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Invoice taxes. The rates are percentages (the reference's examples are <c>iss: 2</c>, <c>pis: 0.65</c>, <c>ir: 1.5</c>), not
/// amounts, and are <see cref="decimal"/> so that values such as 0.65 are held exactly.
/// </summary>
public class InvoiceTaxesDto
{
    /// <summary>
    /// The invoice holder must withhold ISS or not
    /// </summary>
    [JsonPropertyName("retainIss")]
    public bool RetainIss { get; set; }

    /// <summary>
    /// COFINS rate, as a percentage: <c>2</c> means 2%
    /// </summary>
    [JsonPropertyName("cofins")]
    public decimal Cofins { get; set; }

    /// <summary>
    /// CSLL rate, as a percentage: <c>2</c> means 2%
    /// </summary>
    [JsonPropertyName("csll")]
    public decimal Csll { get; set; }

    /// <summary>
    /// INSS rate, as a percentage: <c>2</c> means 2%
    /// </summary>
    [JsonPropertyName("inss")]
    public decimal Inss { get; set; }

    /// <summary>
    /// IR rate, as a percentage: <c>2</c> means 2%
    /// </summary>
    [JsonPropertyName("ir")]
    public decimal Ir { get; set; }

    /// <summary>
    /// PIS rate, as a percentage: <c>2</c> means 2%
    /// </summary>
    [JsonPropertyName("pis")]
    public decimal Pis { get; set; }

    /// <summary>
    /// ISS rate, as a percentage: <c>2</c> means 2%
    /// </summary>
    [JsonPropertyName("iss")]
    public decimal Iss { get; set; }
}
