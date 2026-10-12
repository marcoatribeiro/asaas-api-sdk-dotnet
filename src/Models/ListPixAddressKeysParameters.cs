namespace Asaas.Sdk.Models;

/// <summary>
/// Parameters for listing Pix keys. Filters left null are not sent.
/// </summary>
public class ListPixAddressKeysParameters
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
    /// Status: <c>AWAITING_ACTIVATION</c>, <c>ACTIVE</c>, <c>AWAITING_DELETION</c>, <c>AWAITING_ACCOUNT_DELETION</c>, <c>DELETED</c> or <c>ERROR</c>
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// One or more statuses, separated by commas
    /// </summary>
    public string? StatusList { get; set; }
}
