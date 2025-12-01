using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Transfer service for managing transfers
/// </summary>
public class TransferService : BaseService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TransferService"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client instance</param>
    /// <param name="config">SDK configuration</param>
    public TransferService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List transfers with optional filters
    /// </summary>
    /// <param name="offset">Offset for pagination</param>
    /// <param name="limit">Limit for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of transfers</returns>
    public async Task<TransferListResponseDto> ListTransfersAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/transfers")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<TransferListResponseDto>(request);
    }

    /// <summary>
    /// Get transfer by ID
    /// </summary>
    /// <param name="id">Transfer identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Transfer details</returns>
    public async Task<TransferGetResponseDto> GetTransferAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Transfer ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/transfers/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<TransferGetResponseDto>(request);
    }

    /// <summary>
    /// Create a new transfer
    /// </summary>
    /// <param name="transfer">Transfer details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created transfer</returns>
    public async Task<TransferGetResponseDto> CreateTransferAsync(
        TransferSaveRequestDto transfer,
        CancellationToken cancellationToken = default)
    {
        if (transfer == null)
        {
            throw new ValidationException("Transfer data is required");
        }

        if (transfer.Value <= 0)
        {
            throw new ValidationException("Transfer value must be greater than zero");
        }

        // Validate that either bank account or Pix key is provided
        if (transfer.BankAccount == null && string.IsNullOrEmpty(transfer.PixAddressKey))
        {
            throw new ValidationException("Either bank account or Pix address key must be provided");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/transfers")
            .SetBody(transfer);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<TransferGetResponseDto>(request);
    }

    /// <summary>
    /// Create an internal transfer between Asaas accounts
    /// </summary>
    /// <param name="transfer">Internal transfer details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created internal transfer</returns>
    public async Task<TransferSaveInternalTransferResponseDto> CreateInternalTransferAsync(
        TransferSaveInternalTransferRequestDto transfer,
        CancellationToken cancellationToken = default)
    {
        if (transfer == null)
        {
            throw new ValidationException("Transfer data is required");
        }

        if (transfer.Value <= 0)
        {
            throw new ValidationException("Transfer value must be greater than zero");
        }

        if (transfer.Account == null || string.IsNullOrEmpty(transfer.Account.WalletId))
        {
            throw new ValidationException("Destination account wallet ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/transfers/internal")
            .SetBody(transfer);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<TransferSaveInternalTransferResponseDto>(request);
    }

    /// <summary>
    /// Cancel a pending transfer
    /// </summary>
    /// <param name="id">Transfer identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Transfer cancellation response</returns>
    public async Task<TransferDeleteResponseDto> CancelTransferAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Transfer ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/transfers/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<TransferDeleteResponseDto>(request);
    }
}
