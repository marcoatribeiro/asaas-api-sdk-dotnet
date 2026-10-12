using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Installment service for managing installment payments
/// </summary>
public class InstallmentService : BaseService
{
    public InstallmentService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List installments (<c>GET /v3/installments</c>)
    /// </summary>
    public async Task<InstallmentListResponseDto> ListInstallmentsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/installments")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InstallmentListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get installment by ID (<c>GET /v3/installments/{id}</c>)
    /// </summary>
    public async Task<InstallmentGetResponseDto> GetInstallmentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/installments/{Uri.EscapeDataString(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InstallmentGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Create a new installment (<c>POST /v3/installments</c>)
    /// </summary>
    public async Task<InstallmentGetResponseDto> CreateInstallmentAsync(
        InstallmentSaveRequestDto installment,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(installment);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/installments")
            .SetBody(installment);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InstallmentGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Delete an installment (<c>DELETE /v3/installments/{id}</c>)
    /// </summary>
    public async Task<InstallmentDeleteResponseDto> DeleteInstallmentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/installments/{Uri.EscapeDataString(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InstallmentDeleteResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List payments of an installment (<c>GET /v3/installments/{id}/payments</c>)
    /// </summary>
    /// <param name="id">Installment identifier</param>
    /// <param name="status">Payment status to filter by (e.g. <c>PENDING</c>, <c>RECEIVED</c>, <c>OVERDUE</c>)</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<PaymentListResponseDto> ListInstallmentPaymentsAsync(
        string id,
        string? status = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/installments/{Uri.EscapeDataString(id)}/payments")
            .AddQueryParam("status", status);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentListResponseDto>(request, cancellationToken);
    }
}
