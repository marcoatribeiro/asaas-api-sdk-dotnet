using Asaas.Sdk;
using Asaas.Sdk.Config;
using Asaas.Sdk.Models;
using Asaas.Sdk.Exceptions;

namespace Asaas.Sdk.Examples;

/// <summary>
/// Example application demonstrating the usage of Asaas SDK
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Asaas SDK Example Application");
        Console.WriteLine("==============================\n");

        // Configure the SDK
        var config = new AsaasSdkConfig
        {
            ApiKeyAuthConfig = new ApiKeyAuthConfig
            {
                ApiKey = Environment.GetEnvironmentVariable("ASAAS_API_KEY") ?? "YOUR_API_KEY_HERE",
                ApiKeyHeader = "access_token"
            },
            Timeout = TimeSpan.FromSeconds(30)
        };

        // Create SDK instance
        using var asaasSdk = new AsaasSdk(config);

        // Set environment to sandbox for testing
        asaasSdk.SetEnvironment(Http.Environment.Sandbox);

        try
        {
            // Example 1: List payments
            Console.WriteLine("Example 1: Listing payments...");
            await ListPaymentsExample(asaasSdk);
            Console.WriteLine();

            // Example 2: Get a specific payment (if you have an ID)
            // Console.WriteLine("Example 2: Getting a specific payment...");
            // await GetPaymentExample(asaasSdk, "pay_XXXXXXXXXX");
            // Console.WriteLine();

            // Example 3: Create a new payment
            // Console.WriteLine("Example 3: Creating a new payment...");
            // await CreatePaymentExample(asaasSdk);
            // Console.WriteLine();

            // Example 4: List customers
            Console.WriteLine("Example 4: Listing customers...");
            await ListCustomersExample(asaasSdk);
            Console.WriteLine();
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"API Error: {ex.Message}");
            Console.WriteLine($"Status Code: {ex.StatusCode}");
            if (!string.IsNullOrEmpty(ex.ResponseBody))
            {
                Console.WriteLine($"Response: {ex.ResponseBody}");
            }
        }
        catch (ValidationException ex)
        {
            Console.WriteLine($"Validation Error: {ex.Message}");
            foreach (var error in ex.Errors)
            {
                Console.WriteLine($"  - {error}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    /// <summary>
    /// Example: List payments with pagination
    /// </summary>
    static async Task ListPaymentsExample(AsaasSdk sdk)
    {
        var parameters = new ListPaymentsParameters
        {
            Offset = 0,
            Limit = 10
        };

        var response = await sdk.Payment.ListPaymentsAsync(parameters);

        Console.WriteLine($"Total payments: {response.TotalCount}");
        Console.WriteLine($"Has more: {response.HasMore}");
        Console.WriteLine($"Returned: {response.Data.Count} payments\n");

        foreach (var payment in response.Data)
        {
            Console.WriteLine($"Payment ID: {payment.Id}");
            Console.WriteLine($"  Customer: {payment.Customer}");
            Console.WriteLine($"  Value: R$ {payment.Value:N2}");
            Console.WriteLine($"  Status: {payment.Status}");
            Console.WriteLine($"  Due Date: {payment.DueDate}");
            Console.WriteLine($"  Billing Type: {payment.BillingType}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Example: Get a specific payment by ID
    /// </summary>
    static async Task GetPaymentExample(AsaasSdk sdk, string paymentId)
    {
        var payment = await sdk.Payment.GetPaymentAsync(paymentId);

        Console.WriteLine($"Payment Details:");
        Console.WriteLine($"  ID: {payment.Id}");
        Console.WriteLine($"  Customer: {payment.Customer}");
        Console.WriteLine($"  Value: R$ {payment.Value:N2}");
        Console.WriteLine($"  Net Value: R$ {payment.NetValue:N2}");
        Console.WriteLine($"  Status: {payment.Status}");
        Console.WriteLine($"  Due Date: {payment.DueDate}");
        Console.WriteLine($"  Billing Type: {payment.BillingType}");
        Console.WriteLine($"  Description: {payment.Description}");
        
        if (!string.IsNullOrEmpty(payment.InvoiceUrl))
        {
            Console.WriteLine($"  Invoice URL: {payment.InvoiceUrl}");
        }
        
        if (!string.IsNullOrEmpty(payment.BankSlipUrl))
        {
            Console.WriteLine($"  Bank Slip URL: {payment.BankSlipUrl}");
        }
    }

    /// <summary>
    /// Example: Create a new payment
    /// </summary>
    static async Task CreatePaymentExample(AsaasSdk sdk)
    {
        var newPayment = new PaymentSaveRequestDto
        {
            Customer = "cus_XXXXXXXXXX", // Replace with actual customer ID
            BillingType = "BOLETO",
            Value = 100.00m,
            DueDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"),
            Description = "Test payment from SDK example",
            ExternalReference = $"SDK-TEST-{DateTime.Now:yyyyMMddHHmmss}"
        };

        var createdPayment = await sdk.Payment.CreatePaymentAsync(newPayment);

        Console.WriteLine($"Payment created successfully!");
        Console.WriteLine($"  ID: {createdPayment.Id}");
        Console.WriteLine($"  Status: {createdPayment.Status}");
        Console.WriteLine($"  Value: R$ {createdPayment.Value:N2}");
        Console.WriteLine($"  Due Date: {createdPayment.DueDate}");
        
        if (!string.IsNullOrEmpty(createdPayment.BankSlipUrl))
        {
            Console.WriteLine($"  Bank Slip URL: {createdPayment.BankSlipUrl}");
        }
    }

    /// <summary>
    /// Example: List customers
    /// </summary>
    static async Task ListCustomersExample(AsaasSdk sdk)
    {
        var response = await sdk.Customer.ListCustomersAsync(offset: 0, limit: 10);

        Console.WriteLine($"Total customers: {response.TotalCount}");
        Console.WriteLine($"Has more: {response.HasMore}");
        Console.WriteLine($"Returned: {response.Data.Count} customers\n");

        foreach (var customer in response.Data)
        {
            Console.WriteLine($"Customer ID: {customer.Id}");
            Console.WriteLine($"  Name: {customer.Name}");
            Console.WriteLine($"  Email: {customer.Email}");
            Console.WriteLine($"  CPF/CNPJ: {customer.CpfCnpj}");
            Console.WriteLine($"  Phone: {customer.Phone}");
            Console.WriteLine($"  Mobile: {customer.MobilePhone}");
            Console.WriteLine();
        }
    }
}
