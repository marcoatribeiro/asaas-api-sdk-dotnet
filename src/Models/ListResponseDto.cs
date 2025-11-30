using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Base response DTO with pagination support
/// </summary>
/// <typeparam name="T">Type of data items</typeparam>
public class ListResponseDto<T>
{
    /// <summary>
    /// Object type identifier
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Indicates if there are more items available
    /// </summary>
    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    /// <summary>
    /// Total number of items
    /// </summary>
    [JsonPropertyName("totalCount")]
    public int? TotalCount { get; set; }

    /// <summary>
    /// Maximum number of items per page
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Offset for pagination
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    /// <summary>
    /// List of data items
    /// </summary>
    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = new();
}
