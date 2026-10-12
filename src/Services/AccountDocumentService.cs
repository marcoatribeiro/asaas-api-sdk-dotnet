using System.Net.Http.Headers;
using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Account document service for managing account documents
/// </summary>
public class AccountDocumentService : BaseService
{
    public AccountDocumentService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List account documents
    /// </summary>
    public async Task<AccountDocumentShowResponseDto> ListAccountDocumentsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/myAccount/documents")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountDocumentShowResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get a sent document file (<c>GET /v3/myAccount/documents/files/{id}</c>).
    /// </summary>
    /// <param name="id">
    /// Identifier of the file, as returned after an upload. It is not the document group id listed by
    /// <see cref="ListAccountDocumentsAsync"/>.
    /// </param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<AccountDocumentGetResponseDto> GetAccountDocumentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/myAccount/documents/files/{Uri.EscapeDataString(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountDocumentGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Upload the file of a pending document group (<c>POST /v3/myAccount/documents/{id}</c>, multipart/form-data).
    /// </summary>
    /// <remarks>
    /// Only for a group listed by <see cref="ListAccountDocumentsAsync"/> without an <c>onboardingUrl</c>: a group that
    /// carries one can only be dealt with through that link, and the Asaas rejects the upload. A group holds a single
    /// valid file, so a new upload replaces the previous one. A successful response confirms the file was received,
    /// not that it was approved.
    /// </remarks>
    /// <param name="id">Identifier of the document group, as listed by <see cref="ListAccountDocumentsAsync"/></param>
    /// <param name="documentFile">Content of the file. It is read once, and disposed together with the request.</param>
    /// <param name="fileName">File name sent with the content</param>
    /// <param name="contentType">Media type of the file (e.g. <c>application/pdf</c>)</param>
    /// <param name="type">
    /// Document type (e.g. <c>IDENTIFICATION</c>, <c>SOCIAL_CONTRACT</c>, <c>MINUTES_OF_ELECTION</c>, <c>CUSTOM</c>).
    /// Optional: the group is already identified by <paramref name="id"/>, so the field is omitted when null.
    /// </param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    /// <returns>The document identifier and its status after the upload</returns>
    public async Task<AccountDocumentGetResponseDto> UploadAccountDocumentAsync(
        string id,
        Stream documentFile,
        string fileName,
        string contentType,
        string? type = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(documentFile);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);

        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(documentFile);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(fileContent, "documentFile", fileName);

        if (!string.IsNullOrWhiteSpace(type))
        {
            content.Add(new StringContent(type), "type");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/myAccount/documents/{Uri.EscapeDataString(id)}")
            .SetMultipartContent(content);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountDocumentGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Replace a sent document file (<c>POST /v3/myAccount/documents/files/{id}</c>, multipart/form-data).
    /// </summary>
    /// <param name="id">
    /// Identifier of the file, as returned after an upload. It is not the document group id listed by
    /// <see cref="ListAccountDocumentsAsync"/>.
    /// </param>
    /// <param name="documentFile">Content of the new file. It is read once, and disposed together with the request.</param>
    /// <param name="fileName">File name sent with the content</param>
    /// <param name="contentType">Media type of the file (e.g. <c>application/pdf</c>)</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<AccountDocumentGetResponseDto> UpdateAccountDocumentAsync(
        string id,
        Stream documentFile,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(documentFile);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);

        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(documentFile);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(fileContent, "documentFile", fileName);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/myAccount/documents/files/{Uri.EscapeDataString(id)}")
            .SetMultipartContent(content);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountDocumentGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Delete a sent document file (<c>DELETE /v3/myAccount/documents/files/{id}</c>).
    /// </summary>
    /// <param name="id">
    /// Identifier of the file, as returned after an upload. It is not the document group id listed by
    /// <see cref="ListAccountDocumentsAsync"/>.
    /// </param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<AccountDocumentDeleteResponseDto> DeleteAccountDocumentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/myAccount/documents/files/{Uri.EscapeDataString(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountDocumentDeleteResponseDto>(request, cancellationToken);
    }
}
