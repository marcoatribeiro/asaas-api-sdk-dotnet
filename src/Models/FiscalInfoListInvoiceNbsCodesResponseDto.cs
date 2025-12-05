using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Fiscal info list invoice NBS codes response
/// </summary>
public class FiscalInfoListInvoiceNbsCodesResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Indicates whether there is another page to be searched
    /// </summary>
    [JsonPropertyName("hasMore")]
    public bool? HasMore { get; set; }

    /// <summary>
    /// Total number of items for the filters entered
    /// </summary>
    [JsonPropertyName("totalCount")]
    public long? TotalCount { get; set; }

    /// <summary>
    /// Number of objects per page
    /// </summary>
    [JsonPropertyName("limit")]
    public long? Limit { get; set; }

    /// <summary>
    /// Position of the object from which the page should be loaded
    /// </summary>
    [JsonPropertyName("offset")]
    public long? Offset { get; set; }

    /// <summary>
    /// List of NBS codes
    /// </summary>
    [JsonPropertyName("data")]
    public List<NbsCodeDto>? Data { get; set; }
}
