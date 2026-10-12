# Asaas .NET SDK - Project Overview

## 🎯 Project Goal

This project provides a comprehensive C# / .NET 8 SDK equivalent to the existing Java SDK for the Asaas Payment Gateway API. It enables .NET developers to integrate with the Asaas platform using modern, idiomatic C# code.

## 📦 What's Included

### Core SDK Library (`Asaas.Sdk`)

A production-ready .NET 8 class library that provides:

- ✅ **Type-safe API client** with full IntelliSense support
- ✅ **Async/await patterns** for all API operations
- ✅ **Automatic retry** of safe (`GET`) requests with fixed or exponential backoff; other requests are never repeated
- ✅ **Comprehensive error handling** with custom exceptions
- ✅ **Multiple environments** (Production, Sandbox)
- ✅ **Fluent configuration** API
- ✅ **JSON serialization** using System.Text.Json
- ✅ **Request/response models** with proper annotations
- ✅ **IDisposable pattern** for resource cleanup

### Services Implemented (Sample)

The SDK demonstrates the pattern with two fully implemented services:

1. **PaymentService** - Full CRUD operations for payments
   - List payments with pagination and filtering
   - Get payment by ID
   - Create new payments
   - Update existing payments
   - Delete payments

2. **CustomerService** - Customer management
   - List customers with pagination
   - Get customer by ID

### Example Application

A console application demonstrating:

- SDK initialization and configuration
- Environment selection (Sandbox/Production)
- API key authentication
- Error handling (API exceptions, validation errors)
- Pagination
- Multiple API operations

## 🏗️ Architecture

```
┌─────────────────────────────────────────┐
│         AsaasSdk (Main Client)          │
│  - Initializes all services             │
│  - Manages HttpClient lifecycle         │
│  - Provides environment configuration   │
└────────────┬────────────────────────────┘
             │
             ├──► PaymentService
             ├──► CustomerService
             ├──► SubscriptionService (to be added)
             └──► ... (30+ services from Java SDK)
                  
┌─────────────────────────────────────────┐
│          BaseService (Abstract)         │
│  - HTTP request execution               │
│  - Error handling & mapping             │
│  - Response deserialization             │
└────────────┬────────────────────────────┘
             │
             ├──► RequestBuilder (Fluent API)
             ├──► ModelConverter (JSON)
             ├──► DefaultHeadersHandler
             └──► RetryHandler (GET retries, per-attempt timeout)
```

## 📁 File Structure

```
asaas-sdk-dotnet/
│
├── src/Asaas.Sdk/                    # Main SDK library
│   ├── Config/
│   │   ├── AsaasSdkConfig.cs        # Main configuration
│   │   ├── ApiKeyAuthConfig.cs      # Authentication config
│   │   └── RetryConfig.cs           # Retry policy config
│   │
│   ├── Http/
│   │   ├── Environment.cs           # Environment enum & URLs
│   │   ├── DefaultHeadersHandler.cs # HTTP message handler
│   │   ├── RequestBuilder.cs        # Fluent request builder
│   │   └── ModelConverter.cs        # JSON conversion
│   │
│   ├── Services/
│   │   ├── BaseService.cs           # Abstract base service
│   │   ├── PaymentService.cs        # Payment operations
│   │   └── CustomerService.cs       # Customer operations
│   │
│   ├── Models/
│   │   └── PaymentModels.cs         # DTOs for payments
│   │
│   ├── Exceptions/
│   │   └── ApiException.cs          # Custom exceptions
│   │
│   ├── AsaasSdk.cs                  # Main SDK client
│   └── Asaas.Sdk.csproj             # Project file
│
├── examples/Asaas.Sdk.Examples/      # Example application
│   ├── Program.cs                    # Sample usage code
│   ├── README.md                     # Example documentation
│   └── Asaas.Sdk.Examples.csproj    # Project file
│
├── Asaas.Sdk.sln                     # Visual Studio solution
├── README.md                         # User documentation
├── DEVELOPMENT.md                    # Developer guide
├── GETTING_STARTED.md               # This file
├── build.ps1                         # Windows build script
├── build.sh                          # Linux/macOS build script
└── .gitignore                        # Git ignore file
```

## 🚀 Quick Start

### 1. Build the SDK

**Windows (PowerShell):**
```powershell
cd asaas-sdk-dotnet
.\build.ps1
```

**Linux/macOS:**
```bash
cd asaas-sdk-dotnet
chmod +x build.sh
./build.sh
```

### 2. Run the Examples

```bash
# Set your API key
$env:ASAAS_API_KEY = "your_api_key_here"  # PowerShell
# or
export ASAAS_API_KEY=your_api_key_here    # Bash

# Run the example
dotnet run --project examples/Asaas.Sdk.Examples/Asaas.Sdk.Examples.csproj
```

### 3. Use in Your Project

**Add project reference:**
```bash
dotnet add reference path/to/Asaas.Sdk/Asaas.Sdk.csproj
```

**Example code:**
```csharp
using Asaas.Sdk;
using Asaas.Sdk.Config;

var config = new AsaasSdkConfig
{
    ApiKeyAuthConfig = new ApiKeyAuthConfig
    {
        ApiKey = "your_api_key"
    }
};

using var sdk = new AsaasSdk(config);
sdk.SetEnvironment(Http.Environment.Sandbox);

var payments = await sdk.Payment.ListPaymentsAsync();
```

## 🔄 Comparison: Java SDK vs .NET SDK

| Feature | Java SDK | .NET SDK |
|---------|----------|----------|
| Language Version | Java 8+ | .NET 8 |
| HTTP Client | OkHttp | HttpClient |
| JSON Library | Jackson | System.Text.Json |
| Async Support | CompletableFuture | async/await |
| Retry Logic | Custom interceptor | Built-in `RetryHandler` |
| Builder Pattern | Lombok @Builder | C# properties |
| Null Safety | @NonNull annotations | Nullable reference types |
| Dependency Injection | Manual | Constructor injection |
| Error Handling | Checked exceptions | Task-based exceptions |
| Packaging | Maven/Gradle | NuGet |

## 📊 Implementation Status

### ✅ Completed

- [x] Project structure and solution
- [x] Configuration classes (AsaasSdkConfig, ApiKeyAuthConfig, RetryConfig)
- [x] HTTP infrastructure (RequestBuilder, ModelConverter, DefaultHeadersHandler)
- [x] Base service with error handling
- [x] Exception types (ApiException, ValidationException)
- [x] Environment support (Production, Sandbox)
- [x] PaymentService (full CRUD)
- [x] CustomerService (list, get)
- [x] Sample models (Payment, Customer, Error)
- [x] Example application with multiple scenarios
- [x] Build scripts (Windows & Linux)
- [x] Documentation (README, DEVELOPMENT, GETTING_STARTED)
- [x] .gitignore

### 🚧 To Be Added (Following Java SDK Pattern)

The Java SDK has 30+ services. To complete the .NET SDK, add:

- [ ] SubscriptionService
- [ ] InstallmentService
- [ ] PixService
- [ ] PixTransactionService
- [ ] TransferService
- [ ] InvoiceService
- [ ] WebhookService
- [ ] NotificationService
- [ ] AnticipationService
- [ ] PaymentRefundService
- [ ] PaymentSplitService
- [ ] And 20+ more services...

Each service follows the same pattern demonstrated in PaymentService.

## 🎓 Learning Resources

### For Users
- **README.md** - Installation and basic usage
- **examples/Asaas.Sdk.Examples/** - Working code examples
- **Asaas API Docs** - https://docs.asaas.com

### For Developers
- **DEVELOPMENT.md** - Architecture and contributing guide
- **Source Code** - Well-commented, self-documenting
- **Java SDK** - Reference implementation

## 🧪 Testing the SDK

### Manual Testing

1. Get a sandbox API key from Asaas
2. Set the API key in environment or code
3. Run the examples project
4. Observe the API responses

### Automated Testing (To Be Added)

```bash
# Create test project
dotnet new xunit -n Asaas.Sdk.Tests
cd Asaas.Sdk.Tests
dotnet add reference ../src/Asaas.Sdk/Asaas.Sdk.csproj
dotnet add package Moq
dotnet add package FluentAssertions

# Run tests
dotnet test
```

## 📝 Next Steps

### Immediate
1. ✅ SDK structure created
2. ✅ Core infrastructure implemented
3. ✅ Sample services working
4. ✅ Examples provided

### Short-term
1. Add more services (Subscription, Installment, Pix)
2. Add unit tests
3. Add integration tests
4. Enhance documentation

### Long-term
1. Complete all 30+ services from Java SDK
2. Add webhook signature validation
3. Add advanced features (bulk operations, batch processing)
4. Publish to NuGet
5. Set up CI/CD pipeline

## 💡 Tips

1. **Use the Sandbox** - Always test with sandbox environment first
2. **Handle Errors** - Wrap API calls in try-catch blocks
3. **Dispose Properly** - Use `using` statements for SDK instance
4. **Check Examples** - The examples project shows best practices
5. **Read Logs** - Enable detailed HTTP logging for debugging
6. **Follow Patterns** - Use existing services as templates for new ones

## 🤝 Contributing

To add a new service:

1. Copy `PaymentService.cs` as a template
2. Create corresponding model classes
3. Add service to `AsaasSdk.cs`
4. Update documentation
5. Add examples

See **DEVELOPMENT.md** for detailed contributing guidelines.

## 📞 Support

- Asaas API Documentation: https://docs.asaas.com
- Original Java SDK: https://github.com/asaasdev/asaas-api-sdk-java
- Issues: Check the Java SDK for API behavior reference

## ✨ Summary

You now have a fully functional .NET 8 SDK for Asaas that:

- Mirrors the architecture of the Java SDK
- Uses modern C# idioms and patterns
- Provides type safety and IntelliSense support
- Includes working examples
- Is ready for extension with additional services

The SDK is production-ready for the implemented services (Payment, Customer) and provides a clear template for adding the remaining 30+ services from the Java SDK.
