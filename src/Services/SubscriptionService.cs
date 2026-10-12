using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Subscription service for managing recurring subscriptions
/// </summary>
public class SubscriptionService : BaseService
{
    public SubscriptionService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List subscriptions (<c>GET /v3/subscriptions</c>)
    /// </summary>
    public async Task<SubscriptionListResponseDto> ListSubscriptionsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/subscriptions")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<SubscriptionListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List subscriptions (<c>GET /v3/subscriptions</c>), with the filters the Asaas documents
    /// </summary>
    /// <param name="parameters">Pagination and filters. Those left null are not sent.</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<SubscriptionListResponseDto> ListSubscriptionsAsync(
        ListSubscriptionsParameters parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/subscriptions")
            .AddQueryParam("offset", parameters.Offset)
            .AddQueryParam("limit", parameters.Limit)
            .AddQueryParam("customer", parameters.Customer)
            .AddQueryParam("customerGroupName", parameters.CustomerGroupName)
            .AddQueryParam("billingType", parameters.BillingType)
            .AddQueryParam("status", parameters.Status)
            .AddQueryParam("deletedOnly", QueryFormat.Bool(parameters.DeletedOnly))
            .AddQueryParam("includeDeleted", QueryFormat.Bool(parameters.IncludeDeleted))
            .AddQueryParam("externalReference", parameters.ExternalReference)
            .AddQueryParam("order", parameters.Order)
            .AddQueryParam("sort", parameters.Sort);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<SubscriptionListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get subscription by ID (<c>GET /v3/subscriptions/{id}</c>)
    /// </summary>
    public async Task<SubscriptionGetResponseDto> GetSubscriptionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/subscriptions/{Uri.EscapeDataString(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<SubscriptionGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Create a new subscription (<c>POST /v3/subscriptions</c>)
    /// </summary>
    public async Task<SubscriptionGetResponseDto> CreateSubscriptionAsync(
        SubscriptionSaveRequestDto subscription,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subscription);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/subscriptions")
            .SetBody(subscription);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<SubscriptionGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Update a subscription (<c>PUT /v3/subscriptions/{id}</c>). Properties left null are not sent.
    /// </summary>
    public async Task<SubscriptionGetResponseDto> UpdateSubscriptionAsync(
        string id,
        SubscriptionUpdateRequestDto subscription,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(subscription);

        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/subscriptions/{Uri.EscapeDataString(id)}")
            .SetBody(subscription);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<SubscriptionGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Delete a subscription (<c>DELETE /v3/subscriptions/{id}</c>)
    /// </summary>
    public async Task<SubscriptionDeleteResponseDto> DeleteSubscriptionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/subscriptions/{Uri.EscapeDataString(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<SubscriptionDeleteResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List payments of a subscription (<c>GET /v3/subscriptions/{id}/payments</c>)
    /// </summary>
    /// <param name="id">Subscription identifier</param>
    /// <param name="offset">Offset for pagination</param>
    /// <param name="limit">Limit for pagination</param>
    /// <param name="status">Payment status to filter by (e.g. <c>PENDING</c>, <c>RECEIVED</c>, <c>OVERDUE</c>)</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<PaymentListResponseDto> ListSubscriptionPaymentsAsync(
        string id,
        long? offset = null,
        long? limit = null,
        string? status = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/subscriptions/{Uri.EscapeDataString(id)}/payments")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit)
            .AddQueryParam("status", status);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentListResponseDto>(request, cancellationToken);
    }
}
