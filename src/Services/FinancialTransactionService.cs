using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Financial transaction service for managing financial transactions
/// </summary>
public class FinancialTransactionService : BaseService
{
    public FinancialTransactionService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List financial transactions
    /// </summary>
    public async Task<dynamic> ListFinancialTransactionsAsync(
        DateTime? startDate = null,
        DateTime? endDate = null,
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/financialTransactions")
            .AddQueryParam("startDate", startDate?.ToString("yyyy-MM-dd"))
            .AddQueryParam("finishDate", endDate?.ToString("yyyy-MM-dd"))
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get financial transaction by ID
    /// </summary>
    public async Task<dynamic> GetFinancialTransactionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/financialTransactions/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get payment anticipation financial transaction
    /// </summary>
    public async Task<dynamic> GetPaymentAnticipationFinancialTransactionAsync(
        string anticipationId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/financialTransactions")
            .AddQueryParam("anticipation", anticipationId);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get payment financial transaction
    /// </summary>
    public async Task<dynamic> GetPaymentFinancialTransactionAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/financialTransactions")
            .AddQueryParam("payment", paymentId);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
