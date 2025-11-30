using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Webhook service for managing webhook configurations
/// </summary>
public class WebhookService : BaseService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebhookService"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client instance</param>
    /// <param name="config">SDK configuration</param>
    public WebhookService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List webhooks with optional filters
    /// </summary>
    /// <param name="offset">Offset for pagination</param>
    /// <param name="limit">Limit for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of webhooks</returns>
    public async Task<WebhookConfigListResponseDto> ListWebhooksAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/webhooks")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<WebhookConfigListResponseDto>(request);
    }

    /// <summary>
    /// Get webhook by ID
    /// </summary>
    /// <param name="id">Webhook identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Webhook configuration details</returns>
    public async Task<WebhookConfigGetResponseDto> GetWebhookAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Webhook ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/webhooks/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<WebhookConfigGetResponseDto>(request);
    }

    /// <summary>
    /// Create a new webhook
    /// </summary>
    /// <param name="webhook">Webhook configuration details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created webhook configuration</returns>
    public async Task<WebhookConfigGetResponseDto> CreateWebhookAsync(
        WebhookConfigSaveRequestDto webhook,
        CancellationToken cancellationToken = default)
    {
        if (webhook == null)
        {
            throw new ValidationException("Webhook data is required");
        }

        if (string.IsNullOrEmpty(webhook.Url))
        {
            throw new ValidationException("Webhook URL is required");
        }

        if (string.IsNullOrEmpty(webhook.Email))
        {
            throw new ValidationException("Webhook email is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/webhooks")
            .SetBody(webhook);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<WebhookConfigGetResponseDto>(request);
    }

    /// <summary>
    /// Update an existing webhook
    /// </summary>
    /// <param name="id">Webhook identifier</param>
    /// <param name="webhook">Updated webhook configuration details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated webhook configuration</returns>
    public async Task<WebhookConfigGetResponseDto> UpdateWebhookAsync(
        string id,
        WebhookConfigUpdateRequestDto webhook,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Webhook ID is required");
        }

        if (webhook == null)
        {
            throw new ValidationException("Webhook data is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/webhooks/{id}")
            .SetBody(webhook);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<WebhookConfigGetResponseDto>(request);
    }

    /// <summary>
    /// Delete a webhook
    /// </summary>
    /// <param name="id">Webhook identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Webhook delete response</returns>
    public async Task<WebhookConfigDeleteResponseDto> DeleteWebhookAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Webhook ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/webhooks/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<WebhookConfigDeleteResponseDto>(request);
    }
}
