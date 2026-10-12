using System.Globalization;
using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

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
    /// List recurring PIX transactions (<c>GET /v3/pix/transactions/recurrings</c>)
    /// </summary>
    public async Task<PixRecurringTransactionListResponseDto> ListRecurringTransactionsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/pix/transactions/recurrings")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixRecurringTransactionListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List recurring PIX transactions (<c>GET /v3/pix/transactions/recurrings</c>), with the filters the Asaas documents
    /// </summary>
    /// <param name="parameters">Pagination and filters. Those left null are not sent.</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<PixRecurringTransactionListResponseDto> ListRecurringTransactionsAsync(
        ListPixRecurringTransactionsParameters parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/pix/transactions/recurrings")
            .AddQueryParam("offset", parameters.Offset)
            .AddQueryParam("limit", parameters.Limit)
            .AddQueryParam("status", parameters.Status)
            .AddQueryParam("value", parameters.Value?.ToString(CultureInfo.InvariantCulture))
            .AddQueryParam("searchText", parameters.SearchText);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixRecurringTransactionListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get recurring PIX transaction by ID (<c>GET /v3/pix/transactions/recurrings/{id}</c>)
    /// </summary>
    public async Task<PixRecurringTransactionGetResponseDto> GetRecurringTransactionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/transactions/recurrings/{PathSegment.Escape(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixRecurringTransactionGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List items of a recurring PIX transaction (<c>GET /v3/pix/transactions/recurrings/{id}/items</c>)
    /// </summary>
    public async Task<PixRecurringTransactionItemListResponseDto> ListRecurringTransactionItemsAsync(
        string id,
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/transactions/recurrings/{PathSegment.Escape(id)}/items")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixRecurringTransactionItemListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get recurring PIX transaction item by ID. Calls <c>GET /v3/pix/transactions/recurring/{id}/items/{itemId}</c>, which
    /// the Asaas does not document; its reference has no operation to read one item, only to list them.
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    [Obsolete("Calls a route the Asaas does not document. Use ListRecurringTransactionItemsAsync.")]
    public async Task<dynamic> GetRecurringTransactionItemAsync(
        string id,
        string itemId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/transactions/recurring/{PathSegment.Escape(id)}/items/{PathSegment.Escape(itemId)}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Cancel a recurring PIX transaction (<c>POST /v3/pix/transactions/recurrings/{id}/cancel</c>). Check
    /// <see cref="PixRecurringTransactionGetResponseDto.CanBeCancelled"/> first.
    /// </summary>
    public async Task<PixRecurringTransactionGetResponseDto> CancelRecurringTransactionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/pix/transactions/recurrings/{PathSegment.Escape(id)}/cancel");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixRecurringTransactionGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Cancel an item of a recurring PIX transaction (<c>POST /v3/pix/transactions/recurrings/items/{id}/cancel</c>).
    /// Check <see cref="PixRecurringTransactionItemGetResponseDto.CanBeCancelled"/> in
    /// <see cref="ListRecurringTransactionItemsAsync"/> first.
    /// </summary>
    /// <param name="itemId">Identifier of the item, as listed by <see cref="ListRecurringTransactionItemsAsync"/></param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<PixRecurringTransactionItemGetResponseDto> CancelRecurringTransactionItemAsync(
        string itemId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/pix/transactions/recurrings/items/{PathSegment.Escape(itemId)}/cancel");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixRecurringTransactionItemGetResponseDto>(request, cancellationToken);
    }
}
