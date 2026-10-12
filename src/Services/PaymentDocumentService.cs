using System.Net.Http.Headers;
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
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> ListPaymentDocumentsAsync(
        string paymentId,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{paymentId}/documents");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Upload a payment document (<c>POST /v3/payments/{id}/documents</c>, multipart/form-data).
    /// </summary>
    /// <param name="paymentId">Identifier of the payment</param>
    /// <param name="file">Content of the file. It is read once, and disposed together with the request.</param>
    /// <param name="fileName">File name sent with the content</param>
    /// <param name="contentType">Media type of the file (e.g. <c>application/pdf</c>)</param>
    /// <param name="availableAfterPayment">Whether the document is made available to the customer only after payment</param>
    /// <param name="type">
    /// Document type: <c>INVOICE</c>, <c>CONTRACT</c>, <c>MEDIA</c>, <c>DOCUMENT</c>, <c>SPREADSHEET</c>, <c>PROGRAM</c>
    /// or <c>OTHER</c>
    /// </param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> UploadPaymentDocumentAsync(
        string paymentId,
        Stream file,
        string fileName,
        string contentType,
        bool availableAfterPayment,
        string type,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(paymentId);
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);
        ArgumentException.ThrowIfNullOrWhiteSpace(type);

        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(file);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(fileContent, "file", fileName);
        content.AddField("availableAfterPayment", availableAfterPayment);
        content.AddField("type", type);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{Uri.EscapeDataString(paymentId)}/documents")
            .SetMultipartContent(content);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Upload payment document
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    [Obsolete("Sends JSON, but the Asaas only accepts multipart/form-data with a binary file. Use the overload that takes a Stream, fileName, contentType, availableAfterPayment and type.")]
    public async Task<dynamic> UploadPaymentDocumentAsync(
        string paymentId,
        object document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{paymentId}/documents")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Update payment document
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> UpdatePaymentDocumentAsync(
        string paymentId,
        string documentId,
        object document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/payments/{paymentId}/documents/{documentId}")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
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
        await ExecuteAsync(request, cancellationToken);
    }
}
