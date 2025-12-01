using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Finance service for managing financial information
/// </summary>
public class FinanceService : BaseService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FinanceService"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client instance</param>
    /// <param name="config">SDK configuration</param>
    public FinanceService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// Get account balance
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Current account balance</returns>
    public async Task<FinanceBalanceResponseDto> GetBalanceAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/finance/balance");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FinanceBalanceResponseDto>(request);
    }

    /// <summary>
    /// Get current account balance
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Current account balance</returns>
    public async Task<FinanceBalanceResponseDto> GetCurrentBalanceAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/finance/getCurrentBalance");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FinanceBalanceResponseDto>(request);
    }

    /// <summary>
    /// Get payment statistics
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment statistics including quantity, value, and net value</returns>
    public async Task<FinanceGetPaymentStatisticsResponseDto> GetStatisticsAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/finance/payment/statistics");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FinanceGetPaymentStatisticsResponseDto>(request);
    }

    /// <summary>
    /// Get split statistics
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Split statistics including income and values to be sent</returns>
    public async Task<FinanceGetSplitStatisticsResponseDto> GetSplitStatisticsAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/finance/split/statistics");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FinanceGetSplitStatisticsResponseDto>(request);
    }
}
