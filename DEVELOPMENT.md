# Asaas .NET SDK - Development Guide

## Overview

This is a comprehensive .NET 8 SDK for the Asaas Payment Gateway API, providing a strongly-typed, async-first interface to all Asaas API endpoints.

## Project Structure

```
asaas-sdk-dotnet/
├── src/
│   └── Asaas.Sdk/              # Main SDK library
│       ├── Config/             # Configuration classes
│       ├── Exceptions/         # Exception types
│       ├── Http/               # HTTP infrastructure
│       ├── Models/             # Data transfer objects
│       ├── Services/           # API service implementations
│       └── AsaasSdk.cs         # Main SDK client
├── examples/
│   └── Asaas.Sdk.Examples/     # Usage examples
├── Asaas.Sdk.sln              # Visual Studio solution
├── README.md                   # User documentation
├── DEVELOPMENT.md              # This file
├── build.ps1                   # Windows build script
└── build.sh                    # Linux/macOS build script
```

## Architecture

### Core Components

1. **AsaasSdk** - Main entry point, initializes all services
2. **BaseService** - Abstract base class for all API services
3. **Config Classes** - Configuration for API key, retry policy, timeouts
4. **HTTP Infrastructure**:
   - `DefaultHeadersHandler` - Adds authentication and standard headers
   - `RequestBuilder` - Fluent API for building HTTP requests
   - `ModelConverter` - JSON serialization/deserialization
5. **Services** - One service per API resource (Payment, Customer, etc.)
6. **Models** - DTOs for requests and responses

### Design Patterns

- **Builder Pattern** - Used in RequestBuilder for fluent request construction
- **Service Pattern** - Each API resource has its own service class
- **Configuration Pattern** - Centralized configuration through AsaasSdkConfig
- **Dependency Injection Ready** - Services accept HttpClient and config via constructor

## Building the SDK

### Prerequisites

- .NET 8.0 SDK or higher
- Visual Studio 2022, VS Code, or JetBrains Rider (optional)

### Build Commands

**Using the build script (Windows):**
```powershell
.\build.ps1
```

**Using the build script (Linux/macOS):**
```bash
chmod +x build.sh
./build.sh
```

**Using .NET CLI:**
```bash
# Restore dependencies
dotnet restore

# Build in Release mode
dotnet build --configuration Release

# Run tests (when available)
dotnet test

# Create NuGet package
dotnet pack --configuration Release
```

## Adding New Services

To add a new service (e.g., SubscriptionService):

1. **Create the service class** in `src/Asaas.Sdk/Services/`:

```csharp
using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

public class SubscriptionService : BaseService
{
    public SubscriptionService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    public async Task<SubscriptionListResponseDto> ListSubscriptionsAsync(
        ListSubscriptionsParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/subscriptions");
        
        if (parameters != null)
        {
            // Add query parameters
        }
        
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<SubscriptionListResponseDto>(request);
    }
}
```

2. **Create model classes** in `src/Asaas.Sdk/Models/`:

```csharp
public class SubscriptionGetResponseDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    // Add other properties
}

public class SubscriptionListResponseDto : ListResponseDto<SubscriptionGetResponseDto>
{
}
```

3. **Add to AsaasSdk** main client:

```csharp
public class AsaasSdk
{
    // Add property
    public SubscriptionService Subscription { get; }
    
    public AsaasSdk(AsaasSdkConfig config)
    {
        // Initialize in constructor
        Subscription = new SubscriptionService(_httpClient, _config);
    }
}
```

## Testing

### Manual Testing

Use the examples project to manually test the SDK:

```bash
cd examples/Asaas.Sdk.Examples
dotnet run
```

### Unit Testing (To Be Added)

Create a test project:

```bash
dotnet new xunit -n Asaas.Sdk.Tests
cd Asaas.Sdk.Tests
dotnet add reference ../../src/Asaas.Sdk/Asaas.Sdk.csproj
dotnet add package Moq
dotnet add package FluentAssertions
```

## Publishing

### Creating a NuGet Package

```bash
# Pack the library
dotnet pack src/Asaas.Sdk/Asaas.Sdk.csproj --configuration Release --output ./packages

# Push to NuGet (requires API key)
dotnet nuget push ./packages/Asaas.Sdk.1.0.3.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
```

### Version Management

Update version in `src/Asaas.Sdk/Asaas.Sdk.csproj`:

```xml
<Version>1.0.4</Version>
```

## Best Practices

1. **Async/Await** - All API calls are asynchronous
2. **CancellationToken** - Support cancellation for all async operations
3. **Nullable Reference Types** - Enabled for better null safety
4. **JSON Serialization** - Use System.Text.Json with camelCase naming
5. **Error Handling** - Use custom exceptions (ApiException, ValidationException)
6. **Logging** - Consider adding ILogger support for production use
7. **Retry Policy** - Built-in retry with exponential backoff using Polly
8. **Dispose Pattern** - Properly dispose HttpClient resources

## Future Enhancements

1. Add remaining API services (all 30+ services from Java SDK)
2. Add comprehensive XML documentation
3. Add unit tests with high coverage
4. Add integration tests
5. Add logging support (Microsoft.Extensions.Logging)
6. Add metrics and telemetry
7. Add request/response interceptors
8. Add webhook signature validation
9. Consider adding source generators for models
10. Add more examples (webhooks, bulk operations, etc.)

## Contributing

When adding features:

1. Follow existing code style and patterns
2. Add XML documentation to public APIs
3. Update README.md if adding user-facing features
4. Add examples demonstrating new features
5. Ensure backward compatibility

## Resources

- [Asaas API Documentation](https://docs.asaas.com)
- [Original Java SDK](https://github.com/asaasdev/asaas-api-sdk-java)
- [.NET API Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/)
