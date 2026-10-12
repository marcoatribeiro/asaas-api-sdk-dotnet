using System.Net.Http.Headers;
using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

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
    /// List payment links (<c>GET /v3/paymentLinks</c>)
    /// </summary>
    public async Task<PaymentLinkListResponseDto> ListPaymentLinksAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/paymentLinks")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentLinkListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List payment links (<c>GET /v3/paymentLinks</c>), with the filters the Asaas documents
    /// </summary>
    /// <param name="parameters">Pagination and filters. Those left null are not sent.</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<PaymentLinkListResponseDto> ListPaymentLinksAsync(
        ListPaymentLinksParameters parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/paymentLinks")
            .AddQueryParam("offset", parameters.Offset)
            .AddQueryParam("limit", parameters.Limit)
            .AddQueryParam("active", QueryFormat.Bool(parameters.Active))
            .AddQueryParam("includeDeleted", QueryFormat.Bool(parameters.IncludeDeleted))
            .AddQueryParam("name", parameters.Name)
            .AddQueryParam("externalReference", parameters.ExternalReference);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentLinkListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get payment link by ID (<c>GET /v3/paymentLinks/{id}</c>)
    /// </summary>
    public async Task<PaymentLinkGetResponseDto> GetPaymentLinkAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentLinks/{PathSegment.Escape(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentLinkGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Create a payment link (<c>POST /v3/paymentLinks</c>)
    /// </summary>
    public async Task<PaymentLinkGetResponseDto> CreatePaymentLinkAsync(
        PaymentLinkSaveRequestDto paymentLink,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(paymentLink);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/paymentLinks")
            .SetBody(paymentLink);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentLinkGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Update a payment link (<c>PUT /v3/paymentLinks/{id}</c>). Properties left null are not sent.
    /// </summary>
    public async Task<PaymentLinkGetResponseDto> UpdatePaymentLinkAsync(
        string id,
        PaymentLinkUpdateRequestDto paymentLink,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(paymentLink);

        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/paymentLinks/{PathSegment.Escape(id)}")
            .SetBody(paymentLink);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentLinkGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Delete a payment link (<c>DELETE /v3/paymentLinks/{id}</c>)
    /// </summary>
    public async Task<PaymentLinkDeleteResponseDto> DeletePaymentLinkAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/paymentLinks/{PathSegment.Escape(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentLinkDeleteResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List the images of a payment link (<c>GET /v3/paymentLinks/{id}/images</c>)
    /// </summary>
    public async Task<PaymentLinkFileListResponseDto> ListPaymentLinkImagesAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentLinks/{PathSegment.Escape(id)}/images");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentLinkFileListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List files for a payment link. Calls <c>GET /v3/paymentLinks/{id}/files</c>, which the Asaas does not document.
    /// The value is a boxed <see cref="System.Text.Json.JsonElement"/>.
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    [Obsolete("Calls a route the Asaas does not document. Use ListPaymentLinkImagesAsync, which calls /images.")]
    public async Task<dynamic> ListPaymentLinkFilesAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/paymentLinks/{PathSegment.Escape(id)}/files");
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
    public async Task<PaymentLinkFileGetResponseDto> AddPaymentLinkImageAsync(
        string id,
        Stream image,
        string fileName,
        string contentType,
        bool? main = null,
        CancellationToken cancellationToken = default)
    {
        var idSegment = PathSegment.Escape(id);
        ArgumentNullException.ThrowIfNull(image);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);

        using var content = new MultipartFormDataContent();

        var imageContent = new StreamContent(image);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(imageContent, "image", fileName);
        content.AddField("main", main);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/paymentLinks/{idSegment}/images")
            .SetMultipartContent(content);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentLinkFileGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Upload a file to a payment link
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    [Obsolete("Sends JSON to a route the Asaas does not document. Use AddPaymentLinkImageAsync, which sends multipart/form-data to /images.")]
    public async Task<dynamic> UploadPaymentLinkFileAsync(
        string id,
        object file,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/paymentLinks/{PathSegment.Escape(id)}/files")
            .SetBody(file);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }
}
