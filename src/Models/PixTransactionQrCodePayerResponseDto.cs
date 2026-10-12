using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Payer of a Pix QR Code
/// </summary>
public class PixTransactionQrCodePayerResponseDto
{
    /// <summary>
    /// Name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// CPF or CNPJ
    /// </summary>
    [JsonPropertyName("cpfCnpj")]
    public string? CpfCnpj { get; set; }
}
