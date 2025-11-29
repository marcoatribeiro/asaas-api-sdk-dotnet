using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Chargeback service for managing chargebacks
/// </summary>
public class ChargebackService : BaseService
{
    public ChargebackService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List chargebacks
    /// </summary>
    public async Task<dynamic> ListChargebacksAsync(
        long? offset = null,
        long? limit = null,
        string? status = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/chargebacks")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit)
            .AddQueryParam("status", status);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get chargeback by ID
    /// </summary>
    public async Task<dynamic> GetChargebackAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/chargebacks/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Request chargeback contest
    /// </summary>
    public async Task<dynamic> RequestChargebackContestAsync(
        string id,
        object contest,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/chargebacks/{id}/contest")
            .SetBody(contest);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// List chargeback documents
    /// </summary>
    public async Task<dynamic> ListChargebackDocumentsAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/chargebacks/{id}/documents");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Upload chargeback document
    /// </summary>
    public async Task<dynamic> UploadChargebackDocumentAsync(
        string id,
        object document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/chargebacks/{id}/documents")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
