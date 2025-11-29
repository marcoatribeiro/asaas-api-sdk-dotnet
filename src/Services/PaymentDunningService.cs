using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Payment dunning service for managing payment dunnings
/// </summary>
public class PaymentDunningService : BaseService
{
    public PaymentDunningService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List payment dunnings
    /// </summary>
    public async Task<dynamic> ListPaymentDunningsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/paymentDunnings")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get payment dunning by ID
    /// </summary>
    public async Task<dynamic> GetPaymentDunningAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentDunnings/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Create a payment dunning
    /// </summary>
    public async Task<dynamic> CreatePaymentDunningAsync(
        object dunning,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/paymentDunnings")
            .SetBody(dunning);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Simulate a payment dunning
    /// </summary>
    public async Task<dynamic> SimulatePaymentDunningAsync(
        object simulation,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/paymentDunnings/simulate")
            .SetBody(simulation);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// List payment dunning history
    /// </summary>
    public async Task<dynamic> ListPaymentDunningHistoryAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentDunnings/{id}/history");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// List partial payments for a payment dunning
    /// </summary>
    public async Task<dynamic> ListPartialPaymentsAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentDunnings/{id}/partialPayments");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// List payments available for dunning
    /// </summary>
    public async Task<dynamic> ListPaymentsAvailableForDunningAsync(
        string customerId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/paymentDunnings/paymentsAvailableForDunning")
            .AddQueryParam("customer", customerId);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Cancel a payment dunning
    /// </summary>
    public async Task<dynamic> CancelPaymentDunningAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/paymentDunnings/{id}/cancel");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
