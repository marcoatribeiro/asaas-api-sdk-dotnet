using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Request to pay a Pix QR Code
/// </summary>
public class PixTransactionSaveRequestDto
{
    /// <summary>
    /// QR Code to pay (required)
    /// </summary>
    [JsonPropertyName("qrCode")]
    public PixTransactionQrCodeRequestDto? QrCode { get; set; }

    /// <summary>
    /// Amount to pay (required)
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Payment description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Date to schedule the payment for, <c>yyyy-MM-dd</c>
    /// </summary>
    [JsonPropertyName("scheduleDate")]
    public string? ScheduleDate { get; set; }
}
