using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

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
    public async Task<dynamic> ListAccountDocumentsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/myAccount/documents")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get account document by ID
    /// </summary>
    public async Task<dynamic> GetAccountDocumentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/myAccount/documents/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Upload an account document
    /// </summary>
    public async Task<dynamic> UploadAccountDocumentAsync(
        object document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/myAccount/documents")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Update an account document
    /// </summary>
    public async Task<dynamic> UpdateAccountDocumentAsync(
        string id,
        object document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/myAccount/documents/{id}/update")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Delete an account document
    /// </summary>
    public async Task DeleteAccountDocumentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/myAccount/documents/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request);
    }
}
