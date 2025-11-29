using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Payment refund service for managing payment refunds
/// </summary>
public class PaymentRefundService : BaseService
{
    public PaymentRefundService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// Refund payment
    /// </summary>
    public async Task<dynamic> RefundPaymentAsync(
        string paymentId,
        object refundData,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{paymentId}/refund")
            .SetBody(refundData);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get refund status
    /// </summary>
    public async Task<dynamic> GetRefundStatusAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{paymentId}/status");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
