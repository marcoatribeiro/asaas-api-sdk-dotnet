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
        return await ExecuteAsync<AccountDocumentShowResponseDto>(request);
    }

    /// <summary>
    /// Get account document by ID
    /// </summary>
    public async Task<AccountDocumentGetResponseDto> GetAccountDocumentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/myAccount/documents/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountDocumentGetResponseDto>(request);
    }

    /// <summary>
    /// Upload an account document
    /// </summary>
    public async Task<AccountDocumentGetResponseDto> UploadAccountDocumentAsync(
        AccountDocumentSaveRequestDto document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/myAccount/documents")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountDocumentGetResponseDto>(request);
    }

    /// <summary>
    /// Update an account document
    /// </summary>
    public async Task<AccountDocumentGetResponseDto> UpdateAccountDocumentAsync(
        string id,
        AccountDocumentUpdateRequestDto document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/myAccount/documents/{id}/update")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountDocumentGetResponseDto>(request);
    }

    /// <summary>
    /// Delete an account document
    /// </summary>
    public async Task<AccountDocumentDeleteResponseDto> DeleteAccountDocumentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/myAccount/documents/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountDocumentDeleteResponseDto>(request);
    }
}
