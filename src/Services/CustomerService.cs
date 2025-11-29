using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Customer service for managing customers
/// </summary>
public class CustomerService : BaseService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerService"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client instance</param>
    /// <param name="config">SDK configuration</param>
    public CustomerService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List customers
    /// </summary>
    /// <param name="offset">Pagination offset</param>
    /// <param name="limit">Pagination limit</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of customers</returns>
    public async Task<CustomerListResponseDto> ListCustomersAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/customers")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<CustomerListResponseDto>(request);
    }

    /// <summary>
    /// Get customer by ID
    /// </summary>
    /// <param name="id">Customer identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Customer details</returns>
    public async Task<CustomerGetResponseDto> GetCustomerAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Customer ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/customers/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<CustomerGetResponseDto>(request);
    }
}
