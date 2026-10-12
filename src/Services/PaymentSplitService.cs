using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Payment split service for managing payment splits
/// </summary>
public class PaymentSplitService : BaseService
{
    public PaymentSplitService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List payment splits
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> ListPaymentSplitsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/paymentSplits")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Get payment split by payment ID
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> GetPaymentSplitAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentSplits/{paymentId}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Create or update payment split
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> SavePaymentSplitAsync(
        string paymentId,
        object split,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/paymentSplits/{paymentId}")
            .SetBody(split);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Delete payment split
    /// </summary>
    public async Task DeletePaymentSplitAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/paymentSplits/{paymentId}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request, cancellationToken);
    }
}
