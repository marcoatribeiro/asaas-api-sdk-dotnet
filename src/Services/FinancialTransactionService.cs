using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Financial transaction service for managing financial transactions
/// </summary>
public class FinancialTransactionService : BaseService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FinancialTransactionService"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client instance</param>
    /// <param name="config">SDK configuration</param>
    public FinancialTransactionService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List financial transactions with optional filters
    /// </summary>
    /// <param name="startDate">Start date for filtering transactions</param>
    /// <param name="endDate">End date for filtering transactions</param>
    /// <param name="offset">Offset for pagination</param>
    /// <param name="limit">Limit for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of financial transactions</returns>
    public async Task<FinancialTransactionListResponseDto> ListFinancialTransactionsAsync(
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
        return await ExecuteAsync<FinancialTransactionListResponseDto>(request);
    }

    /// <summary>
    /// Get financial transaction by ID
    /// </summary>
    /// <param name="id">Financial transaction identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Financial transaction details</returns>
    public async Task<FinancialTransactionGetResponseDto> GetFinancialTransactionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Financial transaction ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/financialTransactions/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FinancialTransactionGetResponseDto>(request);
    }

    /// <summary>
    /// Get financial transactions related to a payment anticipation
    /// </summary>
    /// <param name="anticipationId">Anticipation identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of financial transactions related to the anticipation</returns>
    public async Task<FinancialTransactionListResponseDto> GetPaymentAnticipationFinancialTransactionAsync(
        string anticipationId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(anticipationId))
        {
            throw new ValidationException("Anticipation ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/financialTransactions")
            .AddQueryParam("anticipation", anticipationId);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FinancialTransactionListResponseDto>(request);
    }

    /// <summary>
    /// Get financial transactions related to a payment
    /// </summary>
    /// <param name="paymentId">Payment identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of financial transactions related to the payment</returns>
    public async Task<FinancialTransactionListResponseDto> GetPaymentFinancialTransactionAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(paymentId))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/financialTransactions")
            .AddQueryParam("payment", paymentId);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FinancialTransactionListResponseDto>(request);
    }
}
