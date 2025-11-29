using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Payment with summary data service for payment operations with summary information
/// </summary>
public class PaymentWithSummaryDataService : BaseService
{
    public PaymentWithSummaryDataService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List payments with summary data
    /// </summary>
    public async Task<dynamic> ListPaymentsWithSummaryDataAsync(
        string? customer = null,
        string? status = null,
        DateTime? dueDateGe = null,
        DateTime? dueDateLe = null,
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/payments")
            .AddQueryParam("customer", customer)
            .AddQueryParam("status", status)
            .AddQueryParam("dueDateGe", dueDateGe?.ToString("yyyy-MM-dd"))
            .AddQueryParam("dueDateLe", dueDateLe?.ToString("yyyy-MM-dd"))
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit)
            .AddQueryParam("includeSummary", true);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get payment summary
    /// </summary>
    public async Task<dynamic> GetPaymentSummaryAsync(
        string? customer = null,
        string? status = null,
        DateTime? dueDateGe = null,
        DateTime? dueDateLe = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/payments/summary")
            .AddQueryParam("customer", customer)
            .AddQueryParam("status", status)
            .AddQueryParam("dueDateGe", dueDateGe?.ToString("yyyy-MM-dd"))
            .AddQueryParam("dueDateLe", dueDateLe?.ToString("yyyy-MM-dd"));

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
