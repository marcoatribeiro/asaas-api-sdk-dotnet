using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Webhook service for managing webhook configurations
/// </summary>
public class WebhookService : BaseService
{
    public WebhookService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List webhooks
    /// </summary>
    public async Task<dynamic> ListWebhooksAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/webhooks")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get webhook by ID
    /// </summary>
    public async Task<dynamic> GetWebhookAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/webhooks/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Create a webhook
    /// </summary>
    public async Task<dynamic> CreateWebhookAsync(
        object webhook,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/webhooks")
            .SetBody(webhook);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Update a webhook
    /// </summary>
    public async Task<dynamic> UpdateWebhookAsync(
        string id,
        object webhook,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/webhooks/{id}")
            .SetBody(webhook);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Delete a webhook
    /// </summary>
    public async Task DeleteWebhookAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/webhooks/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request);
    }
}
