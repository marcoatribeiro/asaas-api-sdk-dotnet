using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Invoice service for managing invoices
/// </summary>
public class InvoiceService : BaseService
{
    public InvoiceService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List invoices
    /// </summary>
    public async Task<dynamic> ListInvoicesAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/invoices")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get invoice by ID
    /// </summary>
    public async Task<dynamic> GetInvoiceAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/invoices/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Create an invoice
    /// </summary>
    public async Task<dynamic> CreateInvoiceAsync(
        object invoice,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/invoices")
            .SetBody(invoice);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Update an invoice
    /// </summary>
    public async Task<dynamic> UpdateInvoiceAsync(
        string id,
        object invoice,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/invoices/{id}")
            .SetBody(invoice);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Cancel an invoice
    /// </summary>
    public async Task<dynamic> CancelInvoiceAsync(
        string id,
        object reason,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/invoices/{id}/cancel")
            .SetBody(reason);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Authorize an invoice
    /// </summary>
    public async Task<dynamic> AuthorizeInvoiceAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/invoices/{id}/authorize");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
