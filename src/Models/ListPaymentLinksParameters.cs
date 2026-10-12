namespace Asaas.Sdk.Models;

/// <summary>
/// Parameters for listing payment links. Filters left null are not sent.
/// </summary>
public class ListPaymentLinksParameters
{
    /// <summary>
    /// Offset for pagination
    /// </summary>
    public long? Offset { get; set; }

    /// <summary>
    /// Limit for pagination (at most 100)
    /// </summary>
    public long? Limit { get; set; }

    /// <summary>
    /// Filters the enabled (true) or the disabled (false) links
    /// </summary>
    public bool? Active { get; set; }

    /// <summary>
    /// When true, includes the removed links
    /// </summary>
    public bool? IncludeDeleted { get; set; }

    /// <summary>
    /// Payment link name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Identifier in your system
    /// </summary>
    public string? ExternalReference { get; set; }
}
