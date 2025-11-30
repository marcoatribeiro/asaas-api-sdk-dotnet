using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Notification details response
/// </summary>
public class NotificationGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Notification identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Customer identifier
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Enabled flag
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// Email enabled for billing
    /// </summary>
    [JsonPropertyName("emailEnabledForProvider")]
    public bool? EmailEnabledForProvider { get; set; }

    /// <summary>
    /// SMS enabled for billing
    /// </summary>
    [JsonPropertyName("smsEnabledForProvider")]
    public bool? SmsEnabledForProvider { get; set; }

    /// <summary>
    /// Email enabled for customer
    /// </summary>
    [JsonPropertyName("emailEnabledForCustomer")]
    public bool? EmailEnabledForCustomer { get; set; }

    /// <summary>
    /// SMS enabled for customer
    /// </summary>
    [JsonPropertyName("smsEnabledForCustomer")]
    public bool? SmsEnabledForCustomer { get; set; }

    /// <summary>
    /// Phone call enabled for customer
    /// </summary>
    [JsonPropertyName("phoneCallEnabledForCustomer")]
    public bool? PhoneCallEnabledForCustomer { get; set; }

    /// <summary>
    /// WhatsApp enabled for customer
    /// </summary>
    [JsonPropertyName("whatsappEnabledForCustomer")]
    public bool? WhatsappEnabledForCustomer { get; set; }
}
