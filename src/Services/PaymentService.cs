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
    /// <returns>Payment delete response</returns>
    public async Task<PaymentDeleteResponseDto> DeletePaymentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/payments/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentDeleteResponseDto>(request);
    }

    /// <summary>
    /// Create new payment with credit card
    /// </summary>
    /// <param name="payment">Payment details with credit card information</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created payment</returns>
    public async Task<PaymentGetResponseDto> CreateNewPaymentWithCreditCardAsync(
        PaymentSaveWithCreditCardRequestDto payment,
        CancellationToken cancellationToken = default)
    {
        if (payment == null)
        {
            throw new ValidationException("Payment data is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/payments/")
            .SetJsonContent(payment);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Capture payment with Pre-Authorization
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Captured payment details</returns>
    public async Task<PaymentGetResponseDto> CapturePaymentWithPreAuthorizationAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{id}/captureAuthorizedPayment")
            .SetJsonContent(new { });

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Pay a charge with a credit card
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="paymentRequest">Credit card payment details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment details</returns>
    public async Task<PaymentGetResponseDto> PayAChargeWithCreditCardAsync(
        string id,
        PaymentPayWithCreditCardRequestDto paymentRequest,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        if (paymentRequest == null)
        {
            throw new ValidationException("Payment request data is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{id}/payWithCreditCard")
            .SetJsonContent(paymentRequest);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Retrieve payment billing information
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment billing information</returns>
    public async Task<PaymentBillingInfoResponseDto> RetrievePaymentBillingInformationAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{id}/billingInfo");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentBillingInfoResponseDto>(request);
    }

    /// <summary>
    /// Payment viewing information
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment viewing information</returns>
    public async Task<PaymentViewingInfoResponseDto> PaymentViewingInformationAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{id}/viewingInfo");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentViewingInfoResponseDto>(request);
    }

    /// <summary>
    /// Retrieve a single payment
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment details</returns>
    public async Task<PaymentGetResponseDto> RetrieveASinglePaymentAsync(
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
    /// Update existing payment
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="payment">Updated payment details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated payment</returns>
    public async Task<PaymentGetResponseDto> UpdateExistingPaymentAsync(
        string id,
        PaymentUpdateRequestDto payment,
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
            .SetJsonContent(payment);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Restore removed payment
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Restored payment details</returns>
    public async Task<PaymentGetResponseDto> RestoreRemovedPaymentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{id}/restore")
            .SetJsonContent(new { });

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Retrieve status of a payment
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment status</returns>
    public async Task<PaymentStatusResponseDto> RetrieveStatusOfAPaymentAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{id}/status");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentStatusResponseDto>(request);
    }

    /// <summary>
    /// Refund payment
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="refundRequest">Refund details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Refunded payment details</returns>
    public async Task<PaymentGetResponseDto> RefundPaymentAsync(
        string id,
        PaymentRefundRequestDto refundRequest,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        if (refundRequest == null)
        {
            throw new ValidationException("Refund request data is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{id}/refund")
            .SetJsonContent(refundRequest);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Get digitable bill line
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment identification field (digitable line)</returns>
    public async Task<PaymentIdentificationFieldResponseDto> GetDigitableBillLineAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{id}/identificationField");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentIdentificationFieldResponseDto>(request);
    }

    /// <summary>
    /// Get QR Code for Pix payments
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>PIX QR Code information</returns>
    public async Task<PaymentPixQrCodeResponseDto> GetQrCodeForPixPaymentsAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{id}/pixQrCode");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentPixQrCodeResponseDto>(request);
    }

    /// <summary>
    /// Confirm cash receipt
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="receiptRequest">Cash receipt details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated payment details</returns>
    public async Task<PaymentGetResponseDto> ConfirmCashReceiptAsync(
        string id,
        PaymentReceiveInCashRequestDto receiptRequest,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        if (receiptRequest == null)
        {
            throw new ValidationException("Receipt request data is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{id}/receiveInCash")
            .SetJsonContent(receiptRequest);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Undo cash receipt confirmation
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated payment details</returns>
    public async Task<PaymentGetResponseDto> UndoCashReceiptConfirmationAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/payments/{id}/undoReceivedInCash")
            .SetJsonContent(new { });

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentGetResponseDto>(request);
    }

    /// <summary>
    /// Sales simulator
    /// </summary>
    /// <param name="simulateRequest">Simulation parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Simulation results</returns>
    public async Task<PaymentSimulateResponseDto> SalesSimulatorAsync(
        PaymentSimulateRequestDto? simulateRequest = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/payments/simulate")
            .SetJsonContent(simulateRequest ?? new PaymentSimulateRequestDto());

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentSimulateResponseDto>(request);
    }

    /// <summary>
    /// Retrieve payment escrow in the Escrow Account
    /// </summary>
    /// <param name="id">Unique payment identifier in Asaas</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment escrow details</returns>
    public async Task<PaymentEscrowGetResponseDto> RetrievePaymentEscrowInTheEscrowAccountAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException("Payment ID is required");
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/payments/{id}/escrow");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentEscrowGetResponseDto>(request);
    }

    /// <summary>
    /// Recovering payment limits
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Payment limits information</returns>
    public async Task<PaymentLimitsResponseDto> RecoveringPaymentLimitsAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/payments/limits");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentLimitsResponseDto>(request);
    }
}
