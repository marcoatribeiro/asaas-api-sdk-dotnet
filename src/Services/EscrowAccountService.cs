using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Escrow account service for managing escrow accounts
/// </summary>
public class EscrowAccountService : BaseService
{
    public EscrowAccountService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// Get escrow account
    /// </summary>
    public async Task<dynamic> GetEscrowAccountAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/paymentEscrowAccount");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// List escrow account items
    /// </summary>
    public async Task<dynamic> ListEscrowAccountItemsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/paymentEscrowAccount/items")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get escrow account item by payment ID
    /// </summary>
    public async Task<dynamic> GetEscrowAccountItemAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentEscrowAccount/items/{paymentId}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Cancel escrow account item
    /// </summary>
    public async Task<dynamic> CancelEscrowAccountItemAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/paymentEscrowAccount/items/{paymentId}/cancel");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Release escrow account item
    /// </summary>
    public async Task<dynamic> ReleaseEscrowAccountItemAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/paymentEscrowAccount/items/{paymentId}/release");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
