namespace Asaas.Sdk.Models;

/// <summary>
/// Parameters for listing municipal services
/// </summary>
public class ListMunicipalServicesParameters
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
    /// Description filter
    /// </summary>
    public string? Description { get; set; }
}
