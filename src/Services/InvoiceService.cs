using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

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
    public async Task<InvoiceListResponseDto> ListInvoicesAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/invoices")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InvoiceListResponseDto>(request);
    }

    /// <summary>
    /// Get invoice by ID
    /// </summary>
    public async Task<InvoiceGetResponseDto> GetInvoiceAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/invoices/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InvoiceGetResponseDto>(request);
    }

    /// <summary>
    /// Create an invoice
    /// </summary>
    public async Task<InvoiceGetResponseDto> CreateInvoiceAsync(
        InvoiceSaveRequestDto invoice,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/invoices")
            .SetBody(invoice);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InvoiceGetResponseDto>(request);
    }

    /// <summary>
    /// Update an invoice
    /// </summary>
    public async Task<InvoiceGetResponseDto> UpdateInvoiceAsync(
        string id,
        InvoiceUpdateRequestDto invoice,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/invoices/{id}")
            .SetBody(invoice);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InvoiceGetResponseDto>(request);
    }

    /// <summary>
    /// Cancel an invoice
    /// </summary>
    public async Task<InvoiceGetResponseDto> CancelInvoiceAsync(
        string id,
        InvoiceCancelRequestDto? reason = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/invoices/{id}/cancel");
        
        if (reason != null)
        {
            requestBuilder.SetBody(reason);
        }

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InvoiceGetResponseDto>(request);
    }

    /// <summary>
    /// Authorize an invoice
    /// </summary>
    public async Task<InvoiceGetResponseDto> AuthorizeInvoiceAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/invoices/{id}/authorize");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<InvoiceGetResponseDto>(request);
    }
}
