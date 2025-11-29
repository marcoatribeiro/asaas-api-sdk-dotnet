using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// PIX service for managing PIX keys and QR codes
/// </summary>
public class PixService : BaseService
{
    public PixService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List PIX keys
    /// </summary>
    public async Task<dynamic> ListKeysAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/pix/addressKeys")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Create a PIX key
    /// </summary>
    public async Task<dynamic> CreateKeyAsync(
        object key,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/pix/addressKeys")
            .SetBody(key);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get PIX key by ID
    /// </summary>
    public async Task<dynamic> GetKeyAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/addressKeys/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Delete a PIX key
    /// </summary>
    public async Task DeleteKeyAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/pix/addressKeys/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request);
    }

    /// <summary>
    /// Create a PIX QR Code
    /// </summary>
    public async Task<dynamic> CreateQrCodeAsync(
        object qrCode,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/pix/qrCodes")
            .SetBody(qrCode);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get PIX QR Code by ID
    /// </summary>
    public async Task<dynamic> GetQrCodeAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/qrCodes/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Delete a PIX QR Code
    /// </summary>
    public async Task DeleteQrCodeAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/pix/qrCodes/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request);
    }
}
