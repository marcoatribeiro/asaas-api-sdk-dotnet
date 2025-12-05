namespace Asaas.Sdk.Models;

/// <summary>
/// Parameters for listing NBS codes
/// </summary>
public class ListNbsCodesParameters
{
    /// <summary>
    /// Offset for pagination
    /// </summary>
    public long? Offset { get; set; }

    /// <summary>
    /// Limit for pagination
    /// </summary>
    public long? Limit { get; set; }

    /// <summary>
    /// Code description filter
    /// </summary>
    public string? CodeDescription { get; set; }
}
