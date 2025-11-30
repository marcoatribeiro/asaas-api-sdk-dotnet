# Asaas .NET SDK 1.0.4

Welcome to the official Asaas API SDK for .NET 8 documentation.

This guide will help you get started with integrating and using the Asaas SDK in your .NET project.

## Versions

- API version: `3.0.0`
- SDK version: `1.0.4`
- .NET version: `8.0`

## About the API

API pública de integração com a plataforma Asaas.

## Table of Contents

- [Setup & Configuration](#setup--configuration)
  - [Supported .NET Versions](#supported-net-versions)
  - [Installation](#installation)
- [Authentication](#authentication)
  - [API Key Authentication](#api-key-authentication)
- [Environments](#environments)
  - [Setting an Environment](#setting-an-environment)
- [Setting a Custom Timeout](#setting-a-custom-timeout)
- [Sample Usage](#sample-usage)
- [Services](#services)
- [Models](#models)
- [License](#license)

# Setup & Configuration

## Supported .NET Versions

This SDK is compatible with: `.NET 8.0` or higher

## Installation

### Using .NET CLI

```bash
dotnet add package TorinoInfo.AsaasSdk --version 1.0.4
```

### Using Package Manager Console

```powershell
Install-Package TorinoInfo.AsaasSdk -Version 1.0.4
```

### Using PackageReference

Add the following to your `.csproj` file:

```xml
<PackageReference Include="TorinoInfo.AsaasSdk" Version="1.0.4" />
```

## Authentication

### API Key Authentication

The Asaas SDK uses API keys as a form of authentication. An API key is a unique identifier used to authenticate a user, developer, or a program that is calling the API.

#### Setting the API key

When you initialize the SDK, you can set the API key as follows:

```csharp
using Asaas.Sdk;
using Asaas.Sdk.Config;

var config = new AsaasSdkConfig
{
    ApiKeyAuthConfig = new ApiKeyAuthConfig
    {
        ApiKey = "YOUR_API_KEY",
        ApiKeyHeader = "access_token"
    }
};

var asaasSdk = new AsaasSdk(config);
```

If you need to set or update the API key after initializing the SDK, you can use:

```csharp
asaasSdk.SetApiKey("YOUR_API_KEY");
asaasSdk.SetApiKeyHeader("access_token");
```

## Environments

The SDK supports different environments for various stages of development and deployment.

Here are the available environments:

```csharp
Environment.Default    // https://api.asaas.com/
Environment.Production // https://api.asaas.com/
Environment.Sandbox    // https://api-sandbox.asaas.com/
```

## Setting an Environment

To configure the SDK to use a specific environment, you can set the base URL as follows:

```csharp
using Asaas.Sdk;
using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

var config = new AsaasSdkConfig();
var asaasSdk = new AsaasSdk(config);

asaasSdk.SetEnvironment(Environment.Sandbox);
```

## Setting a Custom Timeout

You can set a custom timeout for the SDK's HTTP requests as follows:

```csharp
using Asaas.Sdk;
using Asaas.Sdk.Config;

var config = new AsaasSdkConfig
{
    Timeout = TimeSpan.FromSeconds(30)
};

var asaasSdk = new AsaasSdk(config);
```

# Sample Usage

Below is a comprehensive example demonstrating how to authenticate and call a simple endpoint:

```csharp
using Asaas.Sdk;
using Asaas.Sdk.Config;
using Asaas.Sdk.Models;
using Asaas.Sdk.Exceptions;

var config = new AsaasSdkConfig
{
    ApiKeyAuthConfig = new ApiKeyAuthConfig
    {
        ApiKey = "YOUR_API_KEY"
    }
};

var asaasSdk = new AsaasSdk(config);

try
{
    var parameters = new ListPaymentsParameters
    {
        Offset = 0,
        Limit = 10
    };

    var response = await asaasSdk.Payment.ListPaymentsAsync(parameters);

    Console.WriteLine($"Total: {response.TotalCount}");
    foreach (var payment in response.Data)
    {
        Console.WriteLine($"Payment ID: {payment.Id}, Value: {payment.Value}");
    }
}
catch (ApiException ex)
{
    Console.WriteLine($"Error: {ex.Message} (Status Code: {ex.StatusCode})");
}
```

## Services

The SDK provides comprehensive services to interact with the Asaas API:

### Payment & Billing Services
- **PaymentService** - Create, update, delete, and manage payments
- **PaymentLinkService** - Generate and manage payment links
- **PaymentDunningService** - Manage payment collection processes
- **PaymentRefundService** - Process payment refunds
- **PaymentSplitService** - Manage payment splits between accounts
- **PaymentDocumentService** - Manage payment-related documents
- **PaymentWithSummaryDataService** - Get payment data with summary information
- **InstallmentService** - Manage installment payments
- **SubscriptionService** - Manage recurring subscriptions
- **CheckoutService** - Manage checkout sessions

### Customer Management
- **CustomerService** - Create and manage customers

### PIX Services
- **PixService** - Manage PIX keys and QR codes
- **PixTransactionService** - Process PIX transactions
- **RecurringPixService** - Manage recurring PIX payments

### Financial Services
- **TransferService** - Manage transfers and withdrawals
- **AnticipationService** - Manage payment anticipations
- **FinancialTransactionService** - Track financial transactions
- **FinanceService** - Get balance and financial statistics
- **BillService** - Pay bills and utilities
- **MobilePhoneRechargeService** - Process mobile phone recharges
- **EscrowAccountService** - Manage escrow accounts

### Account & Configuration
- **AccountInfoService** - Manage account information
- **AccountDocumentService** - Manage account documents
- **FiscalInfoService** - Manage fiscal information
- **SubaccountService** - Manage subaccounts

### Integration & Notifications
- **WebhookService** - Configure webhook endpoints
- **NotificationService** - Manage customer notifications
- **InvoiceService** - Generate and manage invoices

### Security & Risk
- **ChargebackService** - Handle chargeback disputes
- **CreditBureauReportService** - Request credit reports
- **CreditCardService** - Tokenize credit cards

### Testing
- **SandboxActionsService** - Simulate events in sandbox environment

All services are accessible through the main `AsaasSdk` client instance.

## Models

The SDK includes comprehensive models that represent the data structures used in API requests and responses. These models help in organizing and managing the data efficiently.

## License

This SDK is licensed under the MIT License.

See the [LICENSE](../../LICENSE) file for more details.
