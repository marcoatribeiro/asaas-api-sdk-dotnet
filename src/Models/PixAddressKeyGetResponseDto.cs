using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Pix key
/// </summary>
public class PixAddressKeyGetResponseDto
{
    /// <summary>
    /// Pix key identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Key value
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// Key type: <c>CPF</c>, <c>CNPJ</c>, <c>EMAIL</c>, <c>PHONE</c> or <c>EVP</c>
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Key status: <c>AWAITING_ACTIVATION</c>, <c>ACTIVE</c>, <c>AWAITING_DELETION</c>, <c>AWAITING_ACCOUNT_DELETION</c>, <c>DELETED</c> or <c>ERROR</c>
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Creation date and time (e.g. <c>2022-02-07 17:17:48</c>)
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Whether the key can be deleted
    /// </summary>
    [JsonPropertyName("canBeDeleted")]
    public bool? CanBeDeleted { get; set; }

    /// <summary>
    /// Why the key cannot be deleted
    /// </summary>
    [JsonPropertyName("cannotBeDeletedReason")]
    public string? CannotBeDeletedReason { get; set; }

    /// <summary>
    /// QR Code of the key
    /// </summary>
    [JsonPropertyName("qrCode")]
    public PixAddressKeyQrCodeGetResponseDto? QrCode { get; set; }
}
