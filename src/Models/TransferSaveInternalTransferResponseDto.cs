using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Internal transfer response
/// </summary>
public class TransferSaveInternalTransferResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Unique transfer identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Type of transfer
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Transfer request date
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    /// <summary>
    /// Net value minus transfer fee
    /// </summary>
    [JsonPropertyName("netValue")]
    public double? NetValue { get; set; }

    /// <summary>
    /// Transfer status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Transfer fee
    /// </summary>
    [JsonPropertyName("transferFee")]
    public double? TransferFee { get; set; }

    /// <summary>
    /// Effective date
    /// </summary>
    [JsonPropertyName("effectiveDate")]
    public string? EffectiveDate { get; set; }

    /// <summary>
    /// Transfer method
    /// </summary>
    [JsonPropertyName("operationType")]
    public string? OperationType { get; set; }

    /// <summary>
    /// Transfer description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Destination wallet identifier
    /// </summary>
    [JsonPropertyName("walletId")]
    public string? WalletId { get; set; }
}
