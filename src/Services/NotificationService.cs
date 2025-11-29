using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Notification service for managing customer notifications
/// </summary>
public class NotificationService : BaseService
{
    public NotificationService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List notifications for a payment
    /// </summary>
    public async Task<dynamic> ListNotificationsAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{paymentId}/notifications");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get notification by ID
    /// </summary>
    public async Task<dynamic> GetNotificationAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/notifications/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Update notification configuration
    /// </summary>
    public async Task<dynamic> UpdateNotificationAsync(
        string id,
        object notification,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/notifications/{id}")
            .SetBody(notification);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Batch update notification configuration
    /// </summary>
    public async Task<dynamic> BatchUpdateNotificationsAsync(
        string paymentId,
        object notifications,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/payments/{paymentId}/notifications/batch")
            .SetBody(notifications);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
