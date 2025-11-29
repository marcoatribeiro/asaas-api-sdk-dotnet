# Comprehensive Service Usage Examples

This document provides examples of using various Asaas SDK services.

## Table of Contents

- [Payments](#payments)
- [Customers](#customers)
- [Subscriptions](#subscriptions)
- [PIX](#pix)
- [Transfers](#transfers)
- [Webhooks](#webhooks)
- [Subaccounts](#subaccounts)
- [Sandbox Testing](#sandbox-testing)

## Setup

```csharp
using Asaas.Sdk;
using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

var config = new AsaasSdkConfig
{
    ApiKeyAuthConfig = new ApiKeyAuthConfig
    {
        ApiKey = "YOUR_API_KEY"
    }
};

var sdk = new AsaasSdk(config);
sdk.SetEnvironment(Environment.Sandbox); // Use sandbox for testing
```

## Payments

### Create a Payment

```csharp
var payment = await sdk.Payment.CreatePaymentAsync(new
{
    customer = "cus_000000000001",
    billingType = "BOLETO",
    value = 100.00,
    dueDate = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"),
    description = "Payment for services"
});
```

### List Payments

```csharp
var payments = await sdk.Payment.ListPaymentsAsync(
    offset: 0,
    limit: 10,
    status: "PENDING"
);
```

### Update a Payment

```csharp
var updated = await sdk.Payment.UpdatePaymentAsync(
    "pay_000000000001",
    new { description = "Updated description" }
);
```

### Delete a Payment

```csharp
await sdk.Payment.DeletePaymentAsync("pay_000000000001");
```

## Customers

### Create a Customer

```csharp
var customer = await sdk.Customer.CreateCustomerAsync(new
{
    name = "John Doe",
    cpfCnpj = "12345678900",
    email = "john@example.com",
    phone = "11999999999",
    mobilePhone = "11988888888",
    address = "123 Main St",
    addressNumber = "100",
    complement = "Apt 5",
    province = "Center",
    postalCode = "01000-000"
});
```

### List Customers

```csharp
var customers = await sdk.Customer.ListCustomersAsync(
    offset: 0,
    limit: 20
);
```

## Subscriptions

### Create a Subscription

```csharp
var subscription = await sdk.Subscription.CreateSubscriptionAsync(new
{
    customer = "cus_000000000001",
    billingType = "CREDIT_CARD",
    value = 49.90,
    nextDueDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd"),
    cycle = "MONTHLY",
    description = "Monthly subscription"
});
```

### List Subscriptions

```csharp
var subscriptions = await sdk.Subscription.ListSubscriptionsAsync(
    offset: 0,
    limit: 10
);
```

### Cancel a Subscription

```csharp
await sdk.Subscription.DeleteSubscriptionAsync("sub_000000000001");
```

## PIX

### Create PIX Key

```csharp
var pixKey = await sdk.Pix.CreatePixKeyAsync(new
{
    type = "EVP",
    addressKey = null // Auto-generate EVP key
});
```

### Create PIX QR Code

```csharp
var qrCode = await sdk.Pix.CreateQrCodeAsync(new
{
    addressKey = "your-pix-key",
    description = "Payment via PIX",
    value = 50.00
});
```

### Process PIX Transaction

```csharp
var transaction = await sdk.PixTransaction.CreatePixTransactionAsync(new
{
    value = 100.00,
    pixAddressKey = "recipient-pix-key",
    description = "Transfer via PIX"
});
```

### Decode PIX QR Code

```csharp
var decoded = await sdk.PixTransaction.DecodeQrCodeAsync(new
{
    payload = "00020126330014BR.GOV.BCB.PIX..."
});
```

## Transfers

### Create a Transfer

```csharp
var transfer = await sdk.Transfer.CreateTransferAsync(new
{
    value = 1000.00,
    bankAccount = new
    {
        bank = new { code = "341" },
        accountName = "John Doe",
        ownerName = "John Doe",
        ownerBirthDate = "1990-01-01",
        cpfCnpj = "12345678900",
        agency = "1234",
        account = "12345-6",
        accountDigit = "6"
    }
});
```

### List Transfers

```csharp
var transfers = await sdk.Transfer.ListTransfersAsync(
    offset: 0,
    limit: 10
);
```

## Webhooks

### Create a Webhook

```csharp
var webhook = await sdk.Webhook.CreateWebhookAsync(new
{
    url = "https://your-domain.com/webhook",
    email = "admin@your-domain.com",
    enabled = true,
    interrupted = false,
    events = new[]
    {
        "PAYMENT_CREATED",
        "PAYMENT_UPDATED",
        "PAYMENT_CONFIRMED"
    }
});
```

### List Webhooks

```csharp
var webhooks = await sdk.Webhook.ListWebhooksAsync(
    offset: 0,
    limit: 10
);
```

### Update a Webhook

```csharp
var updated = await sdk.Webhook.UpdateWebhookAsync(
    "webhook_id",
    new { enabled = false }
);
```

## Subaccounts

### Create a Subaccount

```csharp
var subaccount = await sdk.Subaccount.CreateSubaccountAsync(new
{
    name = "Partner Store",
    email = "partner@example.com",
    cpfCnpj = "12345678900",
    birthDate = "1990-01-01",
    phone = "11999999999",
    mobilePhone = "11988888888",
    address = "123 Store St",
    addressNumber = "200",
    postalCode = "01000-000"
});
```

### List Subaccounts

```csharp
var subaccounts = await sdk.Subaccount.ListSubaccountsAsync(
    offset: 0,
    limit: 10
);
```

## Invoices

### Create an Invoice

```csharp
var invoice = await sdk.Invoice.CreateInvoiceAsync(new
{
    customer = "cus_000000000001",
    serviceDescription = "Consulting services",
    observations = "Monthly consulting - January 2024",
    value = 1500.00,
    effectiveDate = DateTime.Today.ToString("yyyy-MM-dd")
});
```

### Authorize Invoice

```csharp
var authorized = await sdk.Invoice.AuthorizeInvoiceAsync("invoice_id");
```

## Financial Information

### Get Balance

```csharp
var balance = await sdk.Finance.GetBalanceAsync();
Console.WriteLine($"Current Balance: {balance.balance}");
```

### Get Statistics

```csharp
var stats = await sdk.Finance.GetStatisticsAsync();
Console.WriteLine($"Total received: {stats.value}");
```

### List Financial Transactions

```csharp
var transactions = await sdk.FinancialTransaction.ListFinancialTransactionsAsync(
    startDate: DateTime.Today.AddDays(-30),
    endDate: DateTime.Today,
    offset: 0,
    limit: 50
);
```

## Payment Links

### Create Payment Link

```csharp
var paymentLink = await sdk.PaymentLink.CreatePaymentLinkAsync(new
{
    name = "Product Purchase",
    description = "Purchase of Product XYZ",
    billingType = "UNDEFINED",
    chargeType = "DETACHED",
    value = 99.90
});
```

## Anticipations

### Simulate Anticipation

```csharp
var simulation = await sdk.Anticipation.SimulateAnticipationAsync(new
{
    payment = "pay_000000000001"
});
```

### Create Anticipation

```csharp
var anticipation = await sdk.Anticipation.CreateAnticipationAsync(new
{
    payment = "pay_000000000001"
});
```

## Sandbox Testing

### Simulate Payment Received

```csharp
await sdk.SandboxActions.SimulatePaymentReceivedAsync("pay_000000000001");
```

### Simulate Payment Confirmation

```csharp
await sdk.SandboxActions.SimulatePaymentConfirmationAsync("pay_000000000001");
```

### Simulate Chargeback

```csharp
await sdk.SandboxActions.SimulateChargebackAsync("pay_000000000001");
```

### Simulate PIX Transaction

```csharp
var pixSimulation = await sdk.SandboxActions.SimulatePixTransactionReceivedAsync(new
{
    payment = "pay_000000000001",
    value = 100.00
});
```

## Error Handling

Always wrap SDK calls in try-catch blocks:

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
catch (Exception ex)
{
    Console.WriteLine($"Unexpected Error: {ex.Message}");
}
```

## Additional Resources

- [API Documentation](https://docs.asaas.com/)
- [SDK Repository](https://github.com/your-org/asaas-sdk-dotnet)
- [Support](https://www.asaas.com/suporte)
