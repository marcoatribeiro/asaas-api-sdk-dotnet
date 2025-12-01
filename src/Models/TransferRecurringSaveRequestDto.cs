using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Transfer recurring configuration
/// </summary>
public class TransferRecurringSaveRequestDto
{
    /// <summary>
    /// Recurrence frequency (WEEKLY, BIWEEKLY, MONTHLY, QUARTERLY, SEMIANNUALLY, YEARLY)
    /// </summary>
    [JsonPropertyName("scheduleFrequency")]
    public string? ScheduleFrequency { get; set; }

    /// <summary>
    /// Day of the week for weekly transfers (MONDAY to SUNDAY)
    /// </summary>
    [JsonPropertyName("weekDay")]
    public string? WeekDay { get; set; }

    /// <summary>
    /// Day of the month for monthly transfers (1-31)
    /// </summary>
    [JsonPropertyName("dayOfMonth")]
    public int? DayOfMonth { get; set; }

    /// <summary>
    /// Month for yearly transfers (1-12)
    /// </summary>
    [JsonPropertyName("month")]
    public int? Month { get; set; }

    /// <summary>
    /// Maximum number of transfers to be executed
    /// </summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    /// End date for the recurring transfer
    /// </summary>
    [JsonPropertyName("endDate")]
    public string? EndDate { get; set; }
}
