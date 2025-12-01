using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Internal transfer account information
/// </summary>
public class TransferSaveInternalTransferAccountDto
{
    /// <summary>
    /// Wallet identifier for the destination account
    /// </summary>
    [JsonPropertyName("walletId")]
    public string? WalletId { get; set; }
}
