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
    public async Task<dynamic> CreateCheckoutSessionAsync(
        object session,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/checkout/sessions")
            .SetBody(session);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get checkout session by ID
    /// </summary>
    public async Task<dynamic> GetCheckoutSessionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/checkout/sessions/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
