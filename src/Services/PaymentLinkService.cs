using System.Net.Http.Headers;
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
        return await ExecuteAsync<dynamic>(request, cancellationToken);
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
        return await ExecuteAsync<dynamic>(request, cancellationToken);
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
        return await ExecuteAsync<dynamic>(request, cancellationToken);
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
        return await ExecuteAsync<dynamic>(request, cancellationToken);
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
        await ExecuteAsync(request, cancellationToken);
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
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Add an image to a payment link (<c>POST /v3/paymentLinks/{id}/images</c>, multipart/form-data).
    /// </summary>
    /// <remarks>
    /// A payment link holds at most 5 images. The SDK does not know how many the link already has, so it does not check:
    /// the Asaas rejects the upload beyond that.
    /// </remarks>
    /// <param name="id">Identifier of the payment link</param>
    /// <param name="image">Content of the image. It is read once, and disposed together with the request.</param>
    /// <param name="fileName">File name sent with the content</param>
    /// <param name="contentType">Media type of the image (e.g. <c>image/png</c>)</param>
    /// <param name="main">Whether this is the main image of the link. Omitted from the request when null.</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<dynamic> AddPaymentLinkImageAsync(
        string id,
        Stream image,
        string fileName,
        string contentType,
        bool? main = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(image);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);

        using var content = new MultipartFormDataContent();

        var imageContent = new StreamContent(image);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(imageContent, "image", fileName);
        content.AddField("main", main);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/paymentLinks/{Uri.EscapeDataString(id)}/images")
            .SetMultipartContent(content);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Upload a file to a payment link
    /// </summary>
    [Obsolete("Sends JSON to a route the Asaas does not document. Use AddPaymentLinkImageAsync, which sends multipart/form-data to /images.")]
    public async Task<dynamic> UploadPaymentLinkFileAsync(
        string id,
        object file,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/paymentLinks/{id}/files")
            .SetBody(file);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }
}
