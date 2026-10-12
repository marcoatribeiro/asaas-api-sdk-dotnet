# Comprehensive Service Usage Examples

This document provides examples of using various Asaas SDK services.

## Table of Contents

- [Reading responses](#reading-responses)
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

## Reading responses

Subscription, installment, payment link and Pix key/QR Code methods take typed requests and return typed responses
(`SubscriptionGetResponseDto`, `InstallmentGetResponseDto`, ...). Status, billing type and other enum-like values are
`string`s, so a value the Asaas adds later does not break reading.

Methods that still return `dynamic` (anticipations, bills, escrow, notifications, Pix transactions, ...) hold a boxed
`System.Text.Json.JsonElement`, **not** an object with one property per JSON field. `result.id` throws a
`RuntimeBinderException`; read the field through `JsonElement` instead:

```csharp
using System.Text.Json;

dynamic result = await sdk.Anticipation.GetConfigurationAsync();

JsonElement json = result;                       // or: (JsonElement)result
string? id = json.GetProperty("id").GetString();

if (json.TryGetProperty("status", out var status))
{
    Console.WriteLine(status.GetString());
}
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
var subscription = await sdk.Subscription.CreateSubscriptionAsync(new SubscriptionSaveRequestDto
{
    Customer = "cus_000000000001",
    BillingType = "CREDIT_CARD",
    Value = 49.90m,
    NextDueDate = DateTime.Today.AddMonths(1).ToString("yyyy-MM-dd"),
    Cycle = "MONTHLY",
    Description = "Monthly subscription"
});

Console.WriteLine($"{subscription.Id}: {subscription.Status}");
```

### List Subscriptions

```csharp
var subscriptions = await sdk.Subscription.ListSubscriptionsAsync(new ListSubscriptionsParameters
{
    Limit = 10,
    Status = "ACTIVE"
});

foreach (var item in subscriptions.Data)
{
    Console.WriteLine($"{item.Id}: {item.Value} {item.Cycle}");
}
```

### Cancel a Subscription

```csharp
var removed = await sdk.Subscription.DeleteSubscriptionAsync("sub_000000000001");
Console.WriteLine(removed.Deleted);
```

## PIX

### Create PIX Key

```csharp
// Only a random (EVP) key can be created
var pixKey = await sdk.Pix.CreateKeyAsync(new PixAddressKeySaveRequestDto { Type = "EVP" });
Console.WriteLine($"{pixKey.Id}: {pixKey.Status}");
```

### Create PIX QR Code

```csharp
var qrCode = await sdk.Pix.CreateQrCodeAsync(new PixQrCodeSaveRequestDto
{
    AddressKey = "your-pix-key",
    Description = "Payment via PIX",
    Value = 50.00m
});

Console.WriteLine(qrCode.Payload);
```

### Pay a PIX QR Code

```csharp
var decoded = await sdk.PixTransaction.DecodeQrCodeAsync(new PixQrCodeDecodeRequestDto
{
    Payload = "00020126330014BR.GOV.BCB.PIX..."
});

if (decoded.CanBePaid == true)
{
    var transaction = await sdk.PixTransaction.PayQrCodeAsync(new PixTransactionSaveRequestDto
    {
        QrCode = new PixTransactionQrCodeRequestDto { Payload = decoded.Payload },
        Value = decoded.TotalValue ?? decoded.Value,
        Description = "Transfer via PIX"
    });

    // The response is not the final confirmation: read the transaction again later
    var current = await sdk.PixTransaction.GetTransactionAsync(transaction.Id!);
    Console.WriteLine(current.Status);
}
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
var paymentLink = await sdk.PaymentLink.CreatePaymentLinkAsync(new PaymentLinkSaveRequestDto
{
    Name = "Product Purchase",
    Description = "Purchase of Product XYZ",
    BillingType = "UNDEFINED",
    ChargeType = "DETACHED",
    Value = 99.90m
});

Console.WriteLine(paymentLink.Url);
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
