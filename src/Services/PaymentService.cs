using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Payment service for managing payments
/// </summary>
public class PaymentService : BaseService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentService"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client instance</param>
    /// <param name="config">SDK configuration</param>
    public PaymentService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List payments with optional filters
    /// </summary>
    /// <param name="parameters">Query parameters for filtering</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of payments</returns>
    public async Task<PaymentListResponseDto> ListPaymentsAsync(
        ListPaymentsParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/payments");

        if (parameters != null)
        {
            requestBuilder
                .AddQueryParam("customer", parameters.Customer)
                .AddQueryParam("billingType", parameters.BillingType)
                .AddQueryParam("status", parameters.Status)
                .AddQueryParam("subscription", parameters.Subscription)
                .AddQueryParam("installment", parameters.Installment)
                .AddQueryParam("externalReference", parameters.ExternalReference)
                .AddQueryParam("offset", parameters.Offset)
                .AddQueryParam("limit", parameters.Limit);
        }

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentListResponseDto>(request);
    }

    /// <summary>
    /// Get payment by ID
    /// </summary>
    /// <param name="id">Payment identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment details</returns>
    public async Task<PaymentGetResponseDto> GetPaymentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Create a new payment
    /// </summary>
    /// <param name="payment">Payment details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created payment</returns>
    public async Task<PaymentGetResponseDto> CreatePaymentAsync(
        PaymentSaveRequestDto payment,
        CancellationToken cancellationToken = default)
    {
        if (payment == null)
        {
            throw new ValidationException("Payment data is required");
        }

        if (string.IsNullOrEmpty(payment.Customer))
        {
            throw new ValidationException("Customer is required");
        }

        if (payment.Value <= 0)
        {
            throw new ValidationException("Value must be greater than zero");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/payments")
            .SetBody(payment);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Update an existing payment
    /// </summary>
    /// <param name="id">Payment identifier</param>
    /// <param name="payment">Updated payment details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated payment</returns>
    public async Task<PaymentGetResponseDto> UpdatePaymentAsync(
        string id,
        PaymentSaveRequestDto payment,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        if (payment == null)
        {
            throw new ValidationException("Payment data is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Put, $"v3/payments/{id}")
            .SetBody(payment);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Delete a payment
    /// </summary>
    /// <param name="id">Payment identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task representing the asynchronous operation</returns>
    public async Task DeletePaymentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/payments/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request);
    }
}
