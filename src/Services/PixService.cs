using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

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
    /// List Pix keys (<c>GET /v3/pix/addressKeys</c>)
    /// </summary>
    public async Task<PixAddressKeyListResponseDto> ListKeysAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/pix/addressKeys")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixAddressKeyListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List Pix keys (<c>GET /v3/pix/addressKeys</c>), with the status filters the Asaas documents
    /// </summary>
    /// <param name="parameters">Pagination and filters. Those left null are not sent.</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<PixAddressKeyListResponseDto> ListKeysAsync(
        ListPixAddressKeysParameters parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/pix/addressKeys")
            .AddQueryParam("offset", parameters.Offset)
            .AddQueryParam("limit", parameters.Limit)
            .AddQueryParam("status", parameters.Status)
            .AddQueryParam("statusList", parameters.StatusList);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixAddressKeyListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Create a Pix key (<c>POST /v3/pix/addressKeys</c>). Only a random (<c>EVP</c>) key can be created.
    /// </summary>
    public async Task<PixAddressKeyGetResponseDto> CreateKeyAsync(
        PixAddressKeySaveRequestDto key,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(key);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/pix/addressKeys")
            .SetBody(key);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixAddressKeyGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get Pix key by ID (<c>GET /v3/pix/addressKeys/{id}</c>)
    /// </summary>
    public async Task<PixAddressKeyGetResponseDto> GetKeyAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/addressKeys/{PathSegment.Escape(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixAddressKeyGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Delete a Pix key (<c>DELETE /v3/pix/addressKeys/{id}</c>). The Asaas answers with the key, with its new status.
    /// </summary>
    public async Task<PixAddressKeyGetResponseDto> DeleteKeyAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/pix/addressKeys/{PathSegment.Escape(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixAddressKeyGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Create a static Pix QR Code (<c>POST /v3/pix/qrCodes/static</c>)
    /// </summary>
    public async Task<PixQrCodeSaveResponseDto> CreateQrCodeAsync(
        PixQrCodeSaveRequestDto qrCode,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(qrCode);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/pix/qrCodes/static")
            .SetBody(qrCode);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixQrCodeSaveResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get PIX QR Code by ID. The route is not in the Asaas reference, which documents no way to read a static QR Code
    /// back, and it has not been confirmed with the Asaas. The value is a boxed <see cref="System.Text.Json.JsonElement"/>.
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    [Obsolete("Calls GET v3/pix/qrCodes/{id}, which the Asaas does not document.")]
    public async Task<dynamic> GetQrCodeAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/qrCodes/{PathSegment.Escape(id)}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Delete a static Pix QR Code (<c>DELETE /v3/pix/qrCodes/static/{id}</c>)
    /// </summary>
    public async Task<PixQrCodeDeleteResponseDto> DeleteQrCodeAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/pix/qrCodes/static/{PathSegment.Escape(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixQrCodeDeleteResponseDto>(request, cancellationToken);
    }
}
