using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

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
    /// List installments
    /// </summary>
    public async Task<dynamic> ListInstallmentsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/installments")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get installment by ID
    /// </summary>
    public async Task<dynamic> GetInstallmentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/installments/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Create a new installment
    /// </summary>
    public async Task<dynamic> CreateInstallmentAsync(
        object installment,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/installments")
            .SetBody(installment);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Delete an installment
    /// </summary>
    public async Task DeleteInstallmentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/installments/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request);
    }

    /// <summary>
    /// List payments of an installment
    /// </summary>
    public async Task<dynamic> ListInstallmentPaymentsAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/installments/{id}/payments");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
