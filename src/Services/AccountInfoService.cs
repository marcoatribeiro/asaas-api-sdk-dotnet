using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Account info service for managing account information
/// </summary>
public class AccountInfoService : BaseService
{
    public AccountInfoService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// Get account information
    /// </summary>
    public async Task<dynamic> GetAccountInfoAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/myAccount");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get commercial info
    /// </summary>
    public async Task<dynamic> GetCommercialInfoAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/myAccount/commercialInfo");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Update account information
    /// </summary>
    public async Task<dynamic> UpdateAccountInfoAsync(
        object accountInfo,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/myAccount")
            .SetBody(accountInfo);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get account status
    /// </summary>
    public async Task<dynamic> GetAccountStatusAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/myAccount/status");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get white label
    /// </summary>
    public async Task<dynamic> GetWhiteLabelAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/myAccount/whiteLabel");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get payment escrow config
    /// </summary>
    public async Task<dynamic> GetPaymentEscrowConfigAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/myAccount/paymentEscrowConfig");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Update payment escrow config
    /// </summary>
    public async Task<dynamic> UpdatePaymentEscrowConfigAsync(
        object config,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/myAccount/paymentEscrowConfig")
            .SetBody(config);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
