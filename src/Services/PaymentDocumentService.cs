using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Payment document service for managing payment documents
/// </summary>
public class PaymentDocumentService : BaseService
{
    public PaymentDocumentService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List payment documents
    /// </summary>
    public async Task<dynamic> ListPaymentDocumentsAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{paymentId}/documents");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Upload payment document
    /// </summary>
    public async Task<dynamic> UploadPaymentDocumentAsync(
        string paymentId,
        object document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{paymentId}/documents")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Update payment document
    /// </summary>
    public async Task<dynamic> UpdatePaymentDocumentAsync(
        string paymentId,
        string documentId,
        object document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/payments/{paymentId}/documents/{documentId}")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Delete payment document
    /// </summary>
    public async Task DeletePaymentDocumentAsync(
        string paymentId,
        string documentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/payments/{paymentId}/documents/{documentId}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request);
    }
}
