using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

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
    /// (Sandbox only) Simulate payment received
    /// </summary>
    public async Task<PaymentGetResponseDto> SimulatePaymentReceivedAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/payments/{PathSegment.Escape(paymentId)}/receiveInCash");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// (Sandbox only) Simulate payment confirmation
    /// </summary>
    public async Task<PaymentGetResponseDto> SimulatePaymentConfirmationAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/payments/{PathSegment.Escape(paymentId)}/confirm");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// (Sandbox only) Simulate refund
    /// </summary>
    public async Task<PaymentGetResponseDto> SimulateRefundAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/payments/{PathSegment.Escape(paymentId)}/refund");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// (Sandbox only) Simulate chargeback
    /// </summary>
    public async Task<PaymentGetResponseDto> SimulateChargebackAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/payments/{PathSegment.Escape(paymentId)}/chargeback");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// (Sandbox only) Simulate PIX transaction received
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> SimulatePixTransactionReceivedAsync(
        object transaction,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/sandbox/pix/transactions/receive")
            .SetBody(transaction);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// (Sandbox only) Simulate transfer done
    /// </summary>
    public async Task<TransferGetResponseDto> SimulateTransferDoneAsync(
        string transferId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/transfers/{PathSegment.Escape(transferId)}/done");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<TransferGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// (Sandbox only) Force charge overdue
    /// </summary>
    public async Task<PaymentGetResponseDto> ForceExpireAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/sandbox/payments/{PathSegment.Escape(paymentId)}/overdue");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request, cancellationToken);
    }
}
