# Asaas .NET SDK - Quick Reference

## Installation

```bash
dotnet add package Asaas.Sdk --version 1.0.3
```

## Basic Setup

```csharp
using Asaas.Sdk;
using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

var config = new AsaasSdkConfig
{
    ApiKeyAuthConfig = new ApiKeyAuthConfig { ApiKey = "YOUR_API_KEY" }
};

var sdk = new AsaasSdk(config);
sdk.SetEnvironment(Environment.Sandbox); // or Environment.Production
```

## Available Services

| Service | Property | Description |
|---------|----------|-------------|
| **Payment** | `sdk.Payment` | Create, list, update, delete payments |
| **Customer** | `sdk.Customer` | Manage customers |
| **Subscription** | `sdk.Subscription` | Recurring subscriptions |
| **Installment** | `sdk.Installment` | Installment payments |
| **Pix** | `sdk.Pix` | PIX keys and QR codes |
| **PixTransaction** | `sdk.PixTransaction` | PIX transactions |
| **RecurringPix** | `sdk.RecurringPix` | Recurring PIX payments |
| **Transfer** | `sdk.Transfer` | Transfers and withdrawals |
| **Anticipation** | `sdk.Anticipation` | Payment anticipations |
| **PaymentLink** | `sdk.PaymentLink` | Payment link generation |
| **Checkout** | `sdk.Checkout` | Checkout sessions |
| **PaymentDunning** | `sdk.PaymentDunning` | Collection management |
| **Bill** | `sdk.Bill` | Bill payments |
| **MobilePhoneRecharge** | `sdk.MobilePhoneRecharge` | Mobile recharges |
| **Invoice** | `sdk.Invoice` | Invoice generation |
| **Notification** | `sdk.Notification` | Customer notifications |
| **Webhook** | `sdk.Webhook` | Webhook configuration |
| **Subaccount** | `sdk.Subaccount` | Subaccount management |
| **Finance** | `sdk.Finance` | Balance and statistics |
| **FinancialTransaction** | `sdk.FinancialTransaction` | Transaction tracking |
| **AccountInfo** | `sdk.AccountInfo` | Account information |
| **AccountDocument** | `sdk.AccountDocument` | Account documents |
| **FiscalInfo** | `sdk.FiscalInfo` | Fiscal information |
| **PaymentRefund** | `sdk.PaymentRefund` | Payment refunds |
| **PaymentSplit** | `sdk.PaymentSplit` | Payment splitting |
| **PaymentDocument** | `sdk.PaymentDocument` | Payment documents |
| **PaymentWithSummaryData** | `sdk.PaymentWithSummaryData` | Payments with summary |
| **EscrowAccount** | `sdk.EscrowAccount` | Escrow management |
| **Chargeback** | `sdk.Chargeback` | Chargeback disputes |
| **CreditCard** | `sdk.CreditCard` | Credit card tokenization |
| **CreditBureauReport** | `sdk.CreditBureauReport` | Credit reports |
| **SandboxActions** | `sdk.SandboxActions` | Sandbox testing |

## Common Operations

### Create Payment
```csharp
var payment = await sdk.Payment.CreatePaymentAsync(new
{
    customer = "cus_000000000001",
    billingType = "BOLETO",
    value = 100.00,
    dueDate = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd")
});
```

### List Payments
```csharp
var payments = await sdk.Payment.ListPaymentsAsync(offset: 0, limit: 10);
```

### Create Customer
```csharp
var customer = await sdk.Customer.CreateCustomerAsync(new
{
    name = "John Doe",
    cpfCnpj = "12345678900",
    email = "john@example.com"
});
```

### Create Subscription
```csharp
var subscription = await sdk.Subscription.CreateSubscriptionAsync(new
{
    customer = "cus_000000000001",
    billingType = "CREDIT_CARD",
    value = 49.90,
    nextDueDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd"),
    cycle = "MONTHLY"
});
```

### Create PIX Transaction
```csharp
var transaction = await sdk.PixTransaction.CreatePixTransactionAsync(new
{
    value = 100.00,
    pixAddressKey = "recipient-key",
    description = "Payment via PIX"
});
```

### Create Webhook
```csharp
var webhook = await sdk.Webhook.CreateWebhookAsync(new
{
    url = "https://your-domain.com/webhook",
    email = "admin@your-domain.com",
    enabled = true,
    events = new[] { "PAYMENT_CREATED", "PAYMENT_CONFIRMED" }
});
```

### Get Balance
```csharp
var balance = await sdk.Finance.GetBalanceAsync();
```

### Sandbox Testing
```csharp
// Simulate payment received
await sdk.SandboxActions.SimulatePaymentReceivedAsync("pay_000000000001");

// Simulate payment confirmation
await sdk.SandboxActions.SimulatePaymentConfirmationAsync("pay_000000000001");
```

## Error Handling

```csharp
using Asaas.Sdk.Exceptions;

try
{
    var payment = await sdk.Payment.CreatePaymentAsync(paymentData);
}
catch (ApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
    
    if (ex.Errors != null)
    {
        foreach (var error in ex.Errors)
        {
            Console.WriteLine($"- {error.Code}: {error.Description}");
        }
    }
}
catch (ValidationException ex)
{
    Console.WriteLine($"Validation Error: {ex.Message}");
}
```

## Configuration Options

```csharp
var config = new AsaasSdkConfig
{
    // API Key (Required)
    ApiKeyAuthConfig = new ApiKeyAuthConfig
    {
        ApiKey = "YOUR_API_KEY",
        ApiKeyHeader = "access_token" // Optional, defaults to "access_token"
    },
    
    // Timeout (Optional, defaults to 100 seconds)
    Timeout = TimeSpan.FromSeconds(30),
    
    // Retry Configuration (Optional)
    RetryConfig = new RetryConfig
    {
        MaxRetries = 3,
        InitialDelayMs = 1000,
        MaxDelayMs = 10000,
        UseExponentialBackoff = true
    }
};
```

## Environments

```csharp
// Production (default)
sdk.SetEnvironment(Environment.Production);
// https://api.asaas.com/

// Sandbox
sdk.SetEnvironment(Environment.Sandbox);
// https://api-sandbox.asaas.com/
```

## Best Practices

1. **Use Sandbox for Testing**: Always test in sandbox before production
2. **Handle Errors**: Always wrap API calls in try-catch blocks
3. **Dispose SDK**: Use `using` statements or call `Dispose()` when done
4. **Configure Retries**: Adjust retry settings based on your needs
5. **Set Timeouts**: Configure appropriate timeout values
6. **Validate Input**: Validate data before sending to API
7. **Log Errors**: Log API errors for debugging
8. **Use Async/Await**: All methods are async, use await properly

## Resources

- **Full Documentation**: See `README.md`
- **Usage Examples**: See `USAGE_EXAMPLES.md`
- **Implementation Details**: See `IMPLEMENTATION_SUMMARY.md`
- **API Documentation**: https://docs.asaas.com/
- **Getting Started**: See `GETTING_STARTED.md`
