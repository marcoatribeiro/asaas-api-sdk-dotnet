using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Sandbox actions service for sandbox environment operations
/// </summary>
public class SandboxActionsService : BaseService
{
    public SandboxActionsService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// Simulate payment received
    /// </summary>
    public async Task<dynamic> SimulatePaymentReceivedAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/payments/{paymentId}/receiveInCash");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Simulate payment confirmation
    /// </summary>
    public async Task<dynamic> SimulatePaymentConfirmationAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/payments/{paymentId}/confirm");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Simulate refund
    /// </summary>
    public async Task<dynamic> SimulateRefundAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/payments/{paymentId}/refund");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Simulate chargeback
    /// </summary>
    public async Task<dynamic> SimulateChargebackAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/payments/{paymentId}/chargeback");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Simulate PIX transaction received
    /// </summary>
    public async Task<dynamic> SimulatePixTransactionReceivedAsync(
        object transaction,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/sandbox/pix/transactions/receive")
            .SetBody(transaction);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Simulate transfer done
    /// </summary>
    public async Task<dynamic> SimulateTransferDoneAsync(
        string transferId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/transfers/{transferId}/done");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
