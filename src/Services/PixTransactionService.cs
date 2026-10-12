using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// PIX transaction service for managing PIX transactions
/// </summary>
public class PixTransactionService : BaseService
{
    public PixTransactionService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// Pay a Pix QR Code (<c>POST /v3/pix/qrCodes/pay</c>)
    /// </summary>
    /// <remarks>
    /// The response is not the final confirmation: keep the transaction <c>Id</c> and read its current state with
    /// <see cref="GetTransactionAsync"/>.
    /// </remarks>
    public async Task<PixTransactionGetResponseDto> PayQrCodeAsync(
        PixTransactionSaveRequestDto transaction,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/pix/qrCodes/pay")
            .SetBody(transaction);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixTransactionGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Create a PIX transaction. Calls <c>POST /v3/pix/transactions</c>, which the Asaas does not document.
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    [Obsolete("Calls a route the Asaas does not document. Use PayQrCodeAsync, which calls POST /v3/pix/qrCodes/pay.")]
    public async Task<dynamic> CreateTransactionAsync(
        object transaction,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/pix/transactions")
            .SetBody(transaction);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Get PIX transaction by ID (<c>GET /v3/pix/transactions/{id}</c>)
    /// </summary>
    public async Task<PixTransactionGetResponseDto> GetTransactionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/pix/transactions/{PathSegment.Escape(id)}");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixTransactionGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List PIX transactions (<c>GET /v3/pix/transactions</c>)
    /// </summary>
    public async Task<PixTransactionListResponseDto> ListTransactionsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/pix/transactions")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixTransactionListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List PIX transactions (<c>GET /v3/pix/transactions</c>), with the filters the Asaas documents
    /// </summary>
    /// <param name="parameters">Pagination and filters. Those left null are not sent.</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<PixTransactionListResponseDto> ListTransactionsAsync(
        ListPixTransactionsParameters parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/pix/transactions")
            .AddQueryParam("offset", parameters.Offset)
            .AddQueryParam("limit", parameters.Limit)
            .AddQueryParam("status", parameters.Status)
            .AddQueryParam("type", parameters.Type)
            .AddQueryParam("endToEndIdentifier", parameters.EndToEndIdentifier);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixTransactionListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Cancel a scheduled PIX transaction (<c>POST /v3/pix/transactions/{id}/cancel</c>). Check
    /// <see cref="PixTransactionGetResponseDto.CanBeCanceled"/> first.
    /// </summary>
    public async Task<PixTransactionGetResponseDto> CancelTransactionAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/pix/transactions/{PathSegment.Escape(id)}/cancel");
        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixTransactionGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Decode a PIX QR Code to pay it (<c>POST /v3/pix/qrCodes/decode</c>)
    /// </summary>
    public async Task<PixQrCodeDecodeResponseDto> DecodeQrCodeAsync(
        PixQrCodeDecodeRequestDto payload,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(payload);

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/pix/qrCodes/decode")
            .SetBody(payload);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PixQrCodeDecodeResponseDto>(request, cancellationToken);
    }
}
