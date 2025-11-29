using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Recurring PIX service for managing recurring PIX transactions
/// </summary>
public class RecurringPixService : BaseService
{
    public RecurringPixService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List recurring PIX transactions
    /// </summary>
    public async Task<dynamic> ListRecurringTransactionsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/pix/transactions/recurring")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get recurring PIX transaction by ID
    /// </summary>
    public async Task<dynamic> GetRecurringTransactionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/transactions/recurring/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// List items of a recurring PIX transaction
    /// </summary>
    public async Task<dynamic> ListRecurringTransactionItemsAsync(
        string id,
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/transactions/recurring/{id}/items")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get recurring PIX transaction item by ID
    /// </summary>
    public async Task<dynamic> GetRecurringTransactionItemAsync(
        string id,
        string itemId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/transactions/recurring/{id}/items/{itemId}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Cancel a recurring PIX transaction
    /// </summary>
    public async Task<dynamic> CancelRecurringTransactionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/pix/transactions/recurring/{id}/cancel");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
