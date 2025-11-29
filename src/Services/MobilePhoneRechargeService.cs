using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Mobile phone recharge service for managing mobile phone recharges
/// </summary>
public class MobilePhoneRechargeService : BaseService
{
    public MobilePhoneRechargeService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List mobile phone recharges
    /// </summary>
    public async Task<dynamic> ListMobilePhoneRechargesAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/mobilePhoneRecharges")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get mobile phone recharge by ID
    /// </summary>
    public async Task<dynamic> GetMobilePhoneRechargeAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/mobilePhoneRecharges/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Create a mobile phone recharge
    /// </summary>
    public async Task<dynamic> CreateMobilePhoneRechargeAsync(
        object recharge,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/mobilePhoneRecharges")
            .SetBody(recharge);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get available providers
    /// </summary>
    public async Task<dynamic> GetProvidersAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/mobilePhoneRecharges/providers");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get provider information
    /// </summary>
    public async Task<dynamic> GetProviderAsync(
        string provider,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/mobilePhoneRecharges/providers/{provider}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Cancel a mobile phone recharge
    /// </summary>
    public async Task<dynamic> CancelMobilePhoneRechargeAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/mobilePhoneRecharges/{id}/cancel");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
