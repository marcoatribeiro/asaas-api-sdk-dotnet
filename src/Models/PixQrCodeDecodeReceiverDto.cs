using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Receiver of a decoded Pix QR Code
/// </summary>
public class PixQrCodeDecodeReceiverDto
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
    /// Trading name
    /// </summary>
    [JsonPropertyName("tradingName")]
    public string? TradingName { get; set; }

    /// <summary>
    /// CPF or CNPJ
    /// </summary>
    [JsonPropertyName("cpfCnpj")]
    public string? CpfCnpj { get; set; }

    /// <summary>
    /// Person type: <c>JURIDICA</c> or <c>FISICA</c>
    /// </summary>
    [JsonPropertyName("personType")]
    public string? PersonType { get; set; }

    /// <summary>
    /// Account type: <c>CHECKING_ACCOUNT</c>, <c>SALARY_ACCOUNT</c>, <c>INVESTIMENT_ACCOUNT</c>, <c>PAYMENT_ACCOUNT</c> or <c>INSTANT_PAYMENT_ACCOUNT</c>
    /// </summary>
    [JsonPropertyName("accountType")]
    public string? AccountType { get; set; }
}
