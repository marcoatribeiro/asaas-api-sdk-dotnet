using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Subaccount service for managing subaccounts
/// </summary>
public class SubaccountService : BaseService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SubaccountService"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client instance</param>
    /// <param name="config">SDK configuration</param>
    public SubaccountService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List subaccounts with optional filters
    /// </summary>
    /// <param name="offset">Offset for pagination</param>
    /// <param name="limit">Limit for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of subaccounts</returns>
    public async Task<AccountListResponseDto> ListSubaccountsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/accounts")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountListResponseDto>(request);
    }

    /// <summary>
    /// Get subaccount by ID
    /// </summary>
    /// <param name="id">Subaccount identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Subaccount details</returns>
    public async Task<AccountGetResponseDto> GetSubaccountAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Subaccount ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/accounts/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountGetResponseDto>(request);
    }

    /// <summary>
    /// Create a new subaccount
    /// </summary>
    /// <param name="subaccount">Subaccount details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created subaccount with API key</returns>
    public async Task<AccountSaveResponseDto> CreateSubaccountAsync(
        AccountSaveRequestDto subaccount,
        CancellationToken cancellationToken = default)
    {
        if (subaccount == null)
        {
            throw new ValidationException("Subaccount data is required");
        }

        if (string.IsNullOrEmpty(subaccount.Name))
        {
            throw new ValidationException("Subaccount name is required");
        }

        if (string.IsNullOrEmpty(subaccount.Email))
        {
            throw new ValidationException("Subaccount email is required");
        }

        if (string.IsNullOrEmpty(subaccount.CpfCnpj))
        {
            throw new ValidationException("CPF or CNPJ is required");
        }

        if (string.IsNullOrEmpty(subaccount.MobilePhone))
        {
            throw new ValidationException("Mobile phone is required");
        }

        if (subaccount.IncomeValue <= 0)
        {
            throw new ValidationException("Income value must be greater than zero");
        }

        if (string.IsNullOrEmpty(subaccount.Address))
        {
            throw new ValidationException("Address is required");
        }

        if (string.IsNullOrEmpty(subaccount.AddressNumber))
        {
            throw new ValidationException("Address number is required");
        }

        if (string.IsNullOrEmpty(subaccount.Province))
        {
            throw new ValidationException("Province (neighborhood) is required");
        }

        if (string.IsNullOrEmpty(subaccount.PostalCode))
        {
            throw new ValidationException("Postal code is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/accounts")
            .SetBody(subaccount);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountSaveResponseDto>(request);
    }

    /// <summary>
    /// Update an existing subaccount
    /// </summary>
    /// <param name="id">Subaccount identifier</param>
    /// <param name="subaccount">Updated subaccount details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated subaccount details</returns>
    public async Task<AccountGetResponseDto> UpdateSubaccountAsync(
        string id,
        AccountUpdateRequestDto subaccount,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Subaccount ID is required");
        }

        if (subaccount == null)
        {
            throw new ValidationException("Subaccount data is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/accounts/{id}")
            .SetBody(subaccount);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<AccountGetResponseDto>(request);
    }
}
