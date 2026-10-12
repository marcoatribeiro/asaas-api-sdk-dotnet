using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Bill service for managing bill payments
/// </summary>
public class BillService : BaseService
{
    public BillService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List bill payments
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> ListBillsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/bill")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Get bill payment by ID
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> GetBillAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/bill/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Create a bill payment
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> CreateBillAsync(
        object bill,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/bill")
            .SetBody(bill);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Simulate a bill payment
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> SimulateBillAsync(
        object simulation,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/bill/simulate")
            .SetBody(simulation);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Cancel a bill payment
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> CancelBillAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/bill/{id}/cancel");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }
}
