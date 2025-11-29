using System.Text.Json.Serialization;

namespace Asaas.Sdk.Models;

/// <summary>
/// Base response DTO with pagination support
/// </summary>
/// <typeparam name="T">Type of data items</typeparam>
public class ListResponseDto<T>
{
    /// <summary>
    /// Object type identifier
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Indicates if there are more items available
    /// </summary>
    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    /// <summary>
    /// Total number of items
    /// </summary>
    [JsonPropertyName("totalCount")]
    public int? TotalCount { get; set; }

    /// <summary>
    /// Maximum number of items per page
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Offset for pagination
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    /// <summary>
    /// List of data items
    /// </summary>
    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = new();
}

/// <summary>
/// Payment list response
/// </summary>
public class PaymentListResponseDto : ListResponseDto<PaymentGetResponseDto>
{
}

/// <summary>
/// Payment details response
/// </summary>
public class PaymentGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Unique payment identifier in Asaas
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Payment creation date
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Unique identifier of the customer to whom the payment belongs
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Unique subscription identifier (when recurring billing)
    /// </summary>
    [JsonPropertyName("subscription")]
    public string? Subscription { get; set; }

    /// <summary>
    /// Unique installment identifier (when installment payment)
    /// </summary>
    [JsonPropertyName("installment")]
    public string? Installment { get; set; }

    /// <summary>
    /// Payment due date
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    /// <summary>
    /// Net value after fees
    /// </summary>
    [JsonPropertyName("netValue")]
    public decimal? NetValue { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Payment status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Payment description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// External reference
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Confirmed date
    /// </summary>
    [JsonPropertyName("confirmedDate")]
    public string? ConfirmedDate { get; set; }

    /// <summary>
    /// Invoice URL
    /// </summary>
    [JsonPropertyName("invoiceUrl")]
    public string? InvoiceUrl { get; set; }

    /// <summary>
    /// Bank slip URL
    /// </summary>
    [JsonPropertyName("bankSlipUrl")]
    public string? BankSlipUrl { get; set; }

    /// <summary>
    /// Transaction receipt URL
    /// </summary>
    [JsonPropertyName("transactionReceiptUrl")]
    public string? TransactionReceiptUrl { get; set; }
}

/// <summary>
/// Payment save request
/// </summary>
public class PaymentSaveRequestDto
{
    /// <summary>
    /// Customer identifier
    /// </summary>
    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    /// <summary>
    /// Payment value
    /// </summary>
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    /// <summary>
    /// Due date
    /// </summary>
    [JsonPropertyName("dueDate")]
    public string? DueDate { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// External reference
    /// </summary>
    [JsonPropertyName("externalReference")]
    public string? ExternalReference { get; set; }
}

/// <summary>
/// Parameters for listing payments
/// </summary>
public class ListPaymentsParameters
{
    /// <summary>
    /// Customer identifier
    /// </summary>
    public string? Customer { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    public string? BillingType { get; set; }

    /// <summary>
    /// Status filter
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Subscription identifier
    /// </summary>
    public string? Subscription { get; set; }

    /// <summary>
    /// Installment identifier
    /// </summary>
    public string? Installment { get; set; }

    /// <summary>
    /// External reference
    /// </summary>
    public string? ExternalReference { get; set; }

    /// <summary>
    /// Offset for pagination
    /// </summary>
    public long? Offset { get; set; }

    /// <summary>
    /// Limit for pagination
    /// </summary>
    public long? Limit { get; set; }
}

/// <summary>
/// Customer response
/// </summary>
public class CustomerGetResponseDto
{
    /// <summary>
    /// Object type
    /// </summary>
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    /// <summary>
    /// Customer identifier
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Date created
    /// </summary>
    [JsonPropertyName("dateCreated")]
    public string? DateCreated { get; set; }

    /// <summary>
    /// Customer name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// CPF or CNPJ
    /// </summary>
    [JsonPropertyName("cpfCnpj")]
    public string? CpfCnpj { get; set; }

    /// <summary>
    /// Phone
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Mobile phone
    /// </summary>
    [JsonPropertyName("mobilePhone")]
    public string? MobilePhone { get; set; }
}

/// <summary>
/// Customer list response
/// </summary>
public class CustomerListResponseDto : ListResponseDto<CustomerGetResponseDto>
{
}

/// <summary>
/// Error response
/// </summary>
public class ErrorResponseDto
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// List of errors
    /// </summary>
    [JsonPropertyName("errors")]
    public List<ErrorItemDto>? Errors { get; set; }
}

/// <summary>
/// Error item
/// </summary>
public class ErrorItemDto
{
    /// <summary>
    /// Error code
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Error description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
