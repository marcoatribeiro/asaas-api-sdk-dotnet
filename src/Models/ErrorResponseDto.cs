using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Error response
/// </summary>
public class ErrorResponseDto
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// List of errors
    /// </summary>
    [JsonPropertyName("errors")]
    public List<ErrorItemDto>? Errors { get; set; }
}
