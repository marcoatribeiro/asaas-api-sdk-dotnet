using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Checkout service for managing checkout sessions
/// </summary>
public class CheckoutService : BaseService
{
    public CheckoutService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// Create a checkout session
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> CreateCheckoutSessionAsync(
        object session,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/checkout/sessions")
            .SetBody(session);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Get checkout session by ID
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> GetCheckoutSessionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/checkout/sessions/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }
}
