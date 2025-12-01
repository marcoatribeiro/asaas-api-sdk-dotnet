using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Transfer delete/cancel response
/// </summary>
public class TransferDeleteResponseDto
{
    /// <summary>
    /// Indicates whether the transfer was deleted/cancelled
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    /// <summary>
    /// Transfer identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
