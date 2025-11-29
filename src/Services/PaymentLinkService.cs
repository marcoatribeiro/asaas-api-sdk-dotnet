using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Payment link service for managing payment links
/// </summary>
public class PaymentLinkService : BaseService
{
    public PaymentLinkService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List payment links
    /// </summary>
    public async Task<dynamic> ListPaymentLinksAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/paymentLinks")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get payment link by ID
    /// </summary>
    public async Task<dynamic> GetPaymentLinkAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentLinks/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Create a payment link
    /// </summary>
    public async Task<dynamic> CreatePaymentLinkAsync(
        object paymentLink,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/paymentLinks")
            .SetBody(paymentLink);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Update a payment link
    /// </summary>
    public async Task<dynamic> UpdatePaymentLinkAsync(
        string id,
        object paymentLink,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/paymentLinks/{id}")
            .SetBody(paymentLink);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Delete a payment link
    /// </summary>
    public async Task DeletePaymentLinkAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/paymentLinks/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request);
    }

    /// <summary>
    /// List files for a payment link
    /// </summary>
    public async Task<dynamic> ListPaymentLinkFilesAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentLinks/{id}/files");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Upload a file to a payment link
    /// </summary>
    public async Task<dynamic> UploadPaymentLinkFileAsync(
        string id,
        object file,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/paymentLinks/{id}/files")
            .SetBody(file);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
