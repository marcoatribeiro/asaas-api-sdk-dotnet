using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Counterparty of a Pix transaction
/// </summary>
public class PixTransactionExternalAccountResponseDto
{
    /// <summary>
    /// ISPB of the institution
    /// </summary>
    [JsonPropertyName("ispb")]
    public string? Ispb { get; set; }

    /// <summary>
    /// Name of the institution
    /// </summary>
    [JsonPropertyName("ispbName")]
    public string? IspbName { get; set; }

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

    /// <summary>
    /// Pix key
    /// </summary>
    [JsonPropertyName("addressKey")]
    public string? AddressKey { get; set; }

    /// <summary>
    /// Pix key type: <c>CPF</c>, <c>CNPJ</c>, <c>EMAIL</c>, <c>PHONE</c> or <c>EVP</c>
    /// </summary>
    [JsonPropertyName("addressKeyType")]
    public string? AddressKeyType { get; set; }
}
