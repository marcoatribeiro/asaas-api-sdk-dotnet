# Asaas SDK Examples

This project contains examples demonstrating how to use the Asaas .NET SDK.

## Running the Examples

### Prerequisites

- .NET 8.0 SDK or higher
- An Asaas API key (get one from [Asaas](https://www.asaas.com))

### Setup

1. Set your API key as an environment variable:

   **Windows (PowerShell):**
   ```powershell
   $env:ASAAS_API_KEY = "your_api_key_here"
   ```

   **Windows (Command Prompt):**
   ```cmd
   set ASAAS_API_KEY=your_api_key_here
   ```

   **Linux/macOS:**
   ```bash
   export ASAAS_API_KEY=your_api_key_here
   ```

2. Or edit `Program.cs` and replace `"YOUR_API_KEY_HERE"` with your actual API key.

### Build and Run

Navigate to the examples directory and run:

```bash
cd examples/Asaas.Sdk.Examples
dotnet run
```

Or from the solution root:

```bash
dotnet run --project examples/Asaas.Sdk.Examples/Asaas.Sdk.Examples.csproj
```

## Examples Included

The example application demonstrates:

1. **Listing Payments** - How to retrieve a paginated list of payments
2. **Getting a Payment** - How to fetch details of a specific payment
3. **Creating a Payment** - How to create a new payment (commented out by default)
4. **Listing Customers** - How to retrieve a list of customers

## Using the Sandbox Environment

The examples use the Asaas Sandbox environment by default for safe testing:

```csharp
asaasSdk.SetEnvironment(Http.Environment.Sandbox);
```

To use the production environment, change to:

```csharp
asaasSdk.SetEnvironment(Http.Environment.Production);
```

## Error Handling

The examples demonstrate proper error handling:

- **ApiException** - For API-related errors (HTTP errors, server errors)
- **ValidationException** - For input validation errors
- **General Exception** - For unexpected errors

## Customization

Feel free to modify the examples to test different scenarios:

- Change pagination parameters
- Filter payments by status, customer, etc.
- Create payments with different billing types (BOLETO, CREDIT_CARD, PIX)
- Test error handling with invalid data

## Additional Resources

- [Asaas API Documentation](https://docs.asaas.com)
- [SDK README](../../README.md)
