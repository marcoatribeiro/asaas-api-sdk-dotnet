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
    /// <param name="name">Filter by customer name</param>
    /// <param name="email">Filter by customer email</param>
    /// <param name="cpfCnpj">Filter by CPF or CNPJ</param>
    /// <param name="groupName">Filter by group name</param>
    /// <param name="externalReference">Filter by external reference</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of customers</returns>
    public async Task<CustomerListResponseDto> ListCustomersAsync(
        long? offset = null,
        long? limit = null,
        string? name = null,
        string? email = null,
        string? cpfCnpj = null,
        string? groupName = null,
        string? externalReference = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/customers")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit)
            .AddQueryParam("name", name)
            .AddQueryParam("email", email)
            .AddQueryParam("cpfCnpj", cpfCnpj)
            .AddQueryParam("groupName", groupName)
            .AddQueryParam("externalReference", externalReference);

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

    /// <summary>
    /// Create new customer
    /// </summary>
    /// <param name="customerSaveRequestDto">Customer data to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created customer details</returns>
    public async Task<CustomerGetResponseDto> CreateNewCustomerAsync(
        CustomerSaveRequestDto customerSaveRequestDto,
        CancellationToken cancellationToken = default)
    {
        if (customerSaveRequestDto == null)
        {
            throw new ValidationException("Customer data is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/customers")
            .SetJsonContent(customerSaveRequestDto);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<CustomerGetResponseDto>(request);
    }

    /// <summary>
    /// Update existing customer
    /// </summary>
    /// <param name="id">Unique identifier of the customer to be updated</param>
    /// <param name="customerUpdateRequestDto">Customer data to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated customer details</returns>
    public async Task<CustomerGetResponseDto> UpdateExistingCustomerAsync(
        string id,
        CustomerUpdateRequestDto customerUpdateRequestDto,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Customer ID is required");
        }

        if (customerUpdateRequestDto == null)
        {
            throw new ValidationException("Customer data is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/customers/{id}")
            .SetJsonContent(customerUpdateRequestDto);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<CustomerGetResponseDto>(request);
    }

    /// <summary>
    /// Remove customer
    /// </summary>
    /// <param name="id">Unique identifier of the customer to be removed</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Delete confirmation response</returns>
    public async Task<CustomerDeleteResponseDto> RemoveCustomerAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Customer ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/customers/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<CustomerDeleteResponseDto>(request);
    }

    /// <summary>
    /// Restore removed customer
    /// </summary>
    /// <param name="id">Unique identifier of the customer to be restored</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Restored customer details</returns>
    public async Task<CustomerGetResponseDto> RestoreRemovedCustomerAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Customer ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/customers/{id}/restore")
            .SetJsonContent(new { });

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<CustomerGetResponseDto>(request);
    }

    /// <summary>
    /// Retrieve notifications from a customer
    /// </summary>
    /// <param name="id">Unique customer identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of customer notifications</returns>
    public async Task<NotificationListResponseDto> RetrieveNotificationsFromCustomerAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Customer ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/customers/{id}/notifications");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<NotificationListResponseDto>(request);
    }
}
