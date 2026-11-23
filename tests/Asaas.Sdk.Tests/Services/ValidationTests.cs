using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Models;
using Asaas.Sdk.Services;

namespace Asaas.Sdk.Tests.Services;

/// <summary>
/// The services that check their input throw <see cref="ValidationException"/> with the reason, before anything is sent.
/// </summary>
public sealed class ValidationTests
{
    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    public static TheoryData<string, Func<HttpClient, AsaasSdkConfig, Task>> Invalid => new()
    {
        // Customers
        { "Customer data is required", (http, config) => new CustomerService(http, config).CreateNewCustomerAsync(null!, Ct) },
        { "Customer data is required", (http, config) => new CustomerService(http, config).UpdateExistingCustomerAsync("cus_1", null!, Ct) },

        // Payments
        { "Payment data is required", (http, config) => new PaymentService(http, config).CreatePaymentAsync(null!, Ct) },
        { "Customer is required", (http, config) => new PaymentService(http, config).CreatePaymentAsync(new PaymentSaveRequestDto { Value = 10 }, Ct) },
        { "Value must be greater than zero", (http, config) => new PaymentService(http, config).CreatePaymentAsync(new PaymentSaveRequestDto { Customer = "cus_1" }, Ct) },
        { "Value must be greater than zero", (http, config) => new PaymentService(http, config).CreatePaymentAsync(new PaymentSaveRequestDto { Customer = "cus_1", Value = -1 }, Ct) },
        { "Payment data is required", (http, config) => new PaymentService(http, config).UpdatePaymentAsync("pay_1", null!, Ct) },
        { "Payment data is required", (http, config) => new PaymentService(http, config).CreateNewPaymentWithCreditCardAsync(null!, Ct) },
        { "Payment request data is required", (http, config) => new PaymentService(http, config).PayAChargeWithCreditCardAsync("pay_1", null!, Ct) },
        { "Payment data is required", (http, config) => new PaymentService(http, config).UpdateExistingPaymentAsync("pay_1", null!, Ct) },
        { "Refund request data is required", (http, config) => new PaymentService(http, config).RefundPaymentAsync("pay_1", null!, Ct) },
        { "Receipt request data is required", (http, config) => new PaymentService(http, config).ConfirmCashReceiptAsync("pay_1", null!, Ct) },

        // Financial transactions
        { "Anticipation ID is required", (http, config) => new FinancialTransactionService(http, config).GetPaymentAnticipationFinancialTransactionAsync("", Ct) },
        { "Payment ID is required", (http, config) => new FinancialTransactionService(http, config).GetPaymentFinancialTransactionAsync(null!, Ct) },

        // Subaccounts: each required field, in the order they are checked
        { "Subaccount data is required", (http, config) => new SubaccountService(http, config).CreateSubaccountAsync(null!, Ct) },
        { "Subaccount name is required", (http, config) => CreateSubaccount(http, config, account => account.Name = "") },
        { "Subaccount email is required", (http, config) => CreateSubaccount(http, config, account => account.Email = "") },
        { "CPF or CNPJ is required", (http, config) => CreateSubaccount(http, config, account => account.CpfCnpj = "") },
        { "Mobile phone is required", (http, config) => CreateSubaccount(http, config, account => account.MobilePhone = "") },
        { "Income value must be greater than zero", (http, config) => CreateSubaccount(http, config, account => account.IncomeValue = 0) },
        { "Address is required", (http, config) => CreateSubaccount(http, config, account => account.Address = "") },
        { "Address number is required", (http, config) => CreateSubaccount(http, config, account => account.AddressNumber = "") },
        { "Province (neighborhood) is required", (http, config) => CreateSubaccount(http, config, account => account.Province = "") },
        { "Postal code is required", (http, config) => CreateSubaccount(http, config, account => account.PostalCode = "") },
        { "Subaccount data is required", (http, config) => new SubaccountService(http, config).UpdateSubaccountAsync("acc_1", null!, Ct) },

        // Transfers
        { "Transfer data is required", (http, config) => new TransferService(http, config).CreateTransferAsync(null!, Ct) },
        { "Transfer value must be greater than zero", (http, config) => new TransferService(http, config).CreateTransferAsync(new TransferSaveRequestDto { PixAddressKey = "key" }, Ct) },
        { "Either bank account or Pix address key must be provided", (http, config) => new TransferService(http, config).CreateTransferAsync(new TransferSaveRequestDto { Value = 10 }, Ct) },
        { "Transfer data is required", (http, config) => new TransferService(http, config).CreateInternalTransferAsync(null!, Ct) },
        { "Transfer value must be greater than zero", (http, config) => new TransferService(http, config).CreateInternalTransferAsync(new TransferSaveInternalTransferRequestDto { Account = new() { WalletId = "w1" } }, Ct) },
        { "Destination account wallet ID is required", (http, config) => new TransferService(http, config).CreateInternalTransferAsync(new TransferSaveInternalTransferRequestDto { Value = 10 }, Ct) },
        { "Destination account wallet ID is required", (http, config) => new TransferService(http, config).CreateInternalTransferAsync(new TransferSaveInternalTransferRequestDto { Value = 10, Account = new() }, Ct) },

        // Webhooks
        { "Webhook data is required", (http, config) => new WebhookService(http, config).CreateWebhookAsync(null!, Ct) },
        { "Webhook URL is required", (http, config) => new WebhookService(http, config).CreateWebhookAsync(new WebhookConfigSaveRequestDto { Email = "a@b.c" }, Ct) },
        { "Webhook email is required", (http, config) => new WebhookService(http, config).CreateWebhookAsync(new WebhookConfigSaveRequestDto { Url = "https://hook.example" }, Ct) },
        { "Webhook data is required", (http, config) => new WebhookService(http, config).UpdateWebhookAsync("wh_1", null!, Ct) },
    };

    [Theory]
    [MemberData(nameof(Invalid), DisableDiscoveryEnumeration = true)]
    public async Task InvalidInput_ShouldThrowItsReason_WithoutSendingARequest(string reason, Func<HttpClient, AsaasSdkConfig, Task> call)
    {
        // Arrange
        var handler = new CaptureHandler();

        // Act
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => call(new HttpClient(handler), new AsaasSdkConfig { BaseUrl = "https://api.example.invalid" }));

        // Assert
        Assert.Equal("Validation failed", exception.Message);
        Assert.Equal([reason], exception.Errors);
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public void ValidationException_ShouldKeepEveryError_InOrder()
    {
        // Act
        var exception = new ValidationException(new[] { "first", "second" });

        // Assert
        Assert.Equal("Validation failed", exception.Message);
        Assert.Equal(["first", "second"], exception.Errors);
    }

    private static Task CreateSubaccount(HttpClient http, AsaasSdkConfig config, Action<AccountSaveRequestDto> breakIt)
    {
        var account = RouteTests.ValidSubaccount();
        breakIt(account);
        return new SubaccountService(http, config).CreateSubaccountAsync(account, Ct);
    }
}
