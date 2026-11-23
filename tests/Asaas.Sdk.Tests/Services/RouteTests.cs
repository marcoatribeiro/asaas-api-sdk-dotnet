using System.Web;
using Asaas.Sdk.Config;
using Asaas.Sdk.Models;
using Asaas.Sdk.Services;

namespace Asaas.Sdk.Tests.Services;

/// <summary>
/// Every endpoint that has no dedicated test sends its documented method, path, query and JSON body. A row's body is
/// either the exact JSON sent, <c>""</c> when the request must have no body, or <c>null</c> when only the route is checked.
/// </summary>
public sealed class RouteTests
{
    private const string BaseUrl = "https://api.example.invalid";

    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    public static TheoryData<string, Func<HttpClient, AsaasSdkConfig, Task>, string, string, string, string?> Routes => new()
    {
        // Account info
        { "AccountInfo.Get", (http, config) => new AccountInfoService(http, config).GetAccountInfoAsync(Ct), "GET", "/v3/myAccount", "", "" },
        { "AccountInfo.GetCommercialInfo", (http, config) => new AccountInfoService(http, config).GetCommercialInfoAsync(Ct), "GET", "/v3/myAccount/commercialInfo", "", "" },
        { "AccountInfo.Update", (http, config) => new AccountInfoService(http, config).UpdateAccountInfoAsync(new AccountInfoSaveRequestDto { Email = "a@b.c", IncomeValue = 1500.5m }, Ct), "POST", "/v3/myAccount", "", """{"incomeValue":1500.5,"email":"a@b.c"}""" },
        { "AccountInfo.GetStatus", (http, config) => new AccountInfoService(http, config).GetAccountStatusAsync(Ct), "GET", "/v3/myAccount/status", "", "" },
        { "AccountInfo.GetWhiteLabel", (http, config) => new AccountInfoService(http, config).GetWhiteLabelAsync(Ct), "GET", "/v3/myAccount/whiteLabel", "", "" },
        { "AccountInfo.GetPaymentEscrowConfig", (http, config) => new AccountInfoService(http, config).GetPaymentEscrowConfigAsync(Ct), "GET", "/v3/myAccount/paymentEscrowConfig", "", "" },
        { "AccountInfo.UpdatePaymentEscrowConfig", (http, config) => new AccountInfoService(http, config).UpdatePaymentEscrowConfigAsync(new AccountSaveOrUpdatePaymentEscrowConfigRequestDto { DaysToExpire = 30, Enabled = true }, Ct), "POST", "/v3/myAccount/paymentEscrowConfig", "", """{"daysToExpire":30,"enabled":true}""" },
        { "AccountDocument.List", (http, config) => new AccountDocumentService(http, config).ListAccountDocumentsAsync(5, 10, Ct), "GET", "/v3/myAccount/documents", "?offset=5&limit=10", "" },

        // Anticipations
        { "Anticipation.List", (http, config) => new AnticipationService(http, config).ListAnticipationsAsync(5, 10, Ct), "GET", "/v3/anticipations", "?offset=5&limit=10", "" },
        { "Anticipation.Get", (http, config) => new AnticipationService(http, config).GetAnticipationAsync("ant_1", Ct), "GET", "/v3/anticipations/ant_1", "", "" },
        { "Anticipation.Create", (http, config) => new AnticipationService(http, config).CreateAnticipationAsync(new { payment = "pay_1" }, Ct), "POST", "/v3/anticipations", "", """{"payment":"pay_1"}""" },
        { "Anticipation.Simulate", (http, config) => new AnticipationService(http, config).SimulateAnticipationAsync(new { payment = "pay_1" }, Ct), "POST", "/v3/anticipations/simulate", "", """{"payment":"pay_1"}""" },
        { "Anticipation.GetConfiguration", (http, config) => new AnticipationService(http, config).GetConfigurationAsync(Ct), "GET", "/v3/anticipations/configuration", "", "" },
        { "Anticipation.UpdateConfiguration", (http, config) => new AnticipationService(http, config).UpdateConfigurationAsync(new { creditCardAutomaticEnabled = true }, Ct), "PUT", "/v3/anticipations/configuration", "", """{"creditCardAutomaticEnabled":true}""" },
        { "Anticipation.GetLimits", (http, config) => new AnticipationService(http, config).GetLimitsAsync(Ct), "GET", "/v3/anticipations/limits", "", "" },

        // Bills, recharges, credit bureau, checkout, credit card
        { "Bill.List", (http, config) => new BillService(http, config).ListBillsAsync(5, 10, Ct), "GET", "/v3/bill", "?offset=5&limit=10", "" },
        { "Bill.Get", (http, config) => new BillService(http, config).GetBillAsync("bill_1", Ct), "GET", "/v3/bill/bill_1", "", "" },
        { "Bill.Create", (http, config) => new BillService(http, config).CreateBillAsync(new { identificationField = "123" }, Ct), "POST", "/v3/bill", "", """{"identificationField":"123"}""" },
        { "Bill.Simulate", (http, config) => new BillService(http, config).SimulateBillAsync(new { barCode = "456" }, Ct), "POST", "/v3/bill/simulate", "", """{"barCode":"456"}""" },
        { "Bill.Cancel", (http, config) => new BillService(http, config).CancelBillAsync("bill_1", Ct), "POST", "/v3/bill/bill_1/cancel", "", "" },
        { "MobilePhoneRecharge.List", (http, config) => new MobilePhoneRechargeService(http, config).ListMobilePhoneRechargesAsync(5, 10, Ct), "GET", "/v3/mobilePhoneRecharges", "?offset=5&limit=10", "" },
        { "MobilePhoneRecharge.Create", (http, config) => new MobilePhoneRechargeService(http, config).CreateMobilePhoneRechargeAsync(new { value = 20m, phoneNumber = "11999999999" }, Ct), "POST", "/v3/mobilePhoneRecharges", "", """{"value":20,"phoneNumber":"11999999999"}""" },
        { "MobilePhoneRecharge.GetProviders", (http, config) => new MobilePhoneRechargeService(http, config).GetProvidersAsync(Ct), "GET", "/v3/mobilePhoneRecharges/providers", "", "" },
        { "CreditBureauReport.List", (http, config) => new CreditBureauReportService(http, config).ListCreditBureauReportsAsync(5, 10, Ct), "GET", "/v3/creditBureauReport", "?offset=5&limit=10", "" },
        { "CreditBureauReport.Get", (http, config) => new CreditBureauReportService(http, config).GetCreditBureauReportAsync("rep_1", Ct), "GET", "/v3/creditBureauReport/rep_1", "", "" },
        { "CreditBureauReport.Create", (http, config) => new CreditBureauReportService(http, config).CreateCreditBureauReportAsync(new { cpfCnpj = "12345678909" }, Ct), "POST", "/v3/creditBureauReport", "", """{"cpfCnpj":"12345678909"}""" },
        { "Checkout.CreateSession", (http, config) => new CheckoutService(http, config).CreateCheckoutSessionAsync(new { billingTypes = new[] { "PIX" } }, Ct), "POST", "/v3/checkout/sessions", "", """{"billingTypes":["PIX"]}""" },
        { "CreditCard.Tokenize", (http, config) => new CreditCardService(http, config).TokenizeCreditCardAsync(new { customer = "cus_1" }, Ct), "POST", "/v3/creditCard/tokenize", "", """{"customer":"cus_1"}""" },

        // Customers
        { "Customer.List", (http, config) => new CustomerService(http, config).ListCustomersAsync(5, 10, "Ana", "a@b.c", "123", "vip", "ref", Ct), "GET", "/v3/customers", "?offset=5&limit=10&name=Ana&email=a%40b.c&cpfCnpj=123&groupName=vip&externalReference=ref", "" },
        { "Customer.List.NoFilters", (http, config) => new CustomerService(http, config).ListCustomersAsync(cancellationToken: Ct), "GET", "/v3/customers", "", "" },
        { "Customer.Create", (http, config) => new CustomerService(http, config).CreateNewCustomerAsync(new CustomerSaveRequestDto { Name = "Ana", CpfCnpj = "123" }, Ct), "POST", "/v3/customers", "", """{"name":"Ana","cpfCnpj":"123"}""" },

        // Escrow, dunnings, splits
        { "EscrowAccount.Get", (http, config) => new EscrowAccountService(http, config).GetEscrowAccountAsync(Ct), "GET", "/v3/paymentEscrowAccount", "", "" },
        { "EscrowAccount.ListItems", (http, config) => new EscrowAccountService(http, config).ListEscrowAccountItemsAsync(5, 10, Ct), "GET", "/v3/paymentEscrowAccount/items", "?offset=5&limit=10", "" },
        { "PaymentDunning.List", (http, config) => new PaymentDunningService(http, config).ListPaymentDunningsAsync(5, 10, Ct), "GET", "/v3/paymentDunnings", "?offset=5&limit=10", "" },
        { "PaymentDunning.Create", (http, config) => new PaymentDunningService(http, config).CreatePaymentDunningAsync(new { payment = "pay_1" }, Ct), "POST", "/v3/paymentDunnings", "", """{"payment":"pay_1"}""" },
        { "PaymentDunning.Simulate", (http, config) => new PaymentDunningService(http, config).SimulatePaymentDunningAsync(new { payment = "pay_1" }, Ct), "POST", "/v3/paymentDunnings/simulate", "", """{"payment":"pay_1"}""" },
        { "PaymentSplit.List", (http, config) => new PaymentSplitService(http, config).ListPaymentSplitsAsync(5, 10, Ct), "GET", "/v3/paymentSplits", "?offset=5&limit=10", "" },

        // Finance and financial transactions
        { "Finance.GetBalance", (http, config) => new FinanceService(http, config).GetBalanceAsync(Ct), "GET", "/v3/finance/balance", "", "" },
        { "Finance.GetCurrentBalance", (http, config) => new FinanceService(http, config).GetCurrentBalanceAsync(Ct), "GET", "/v3/finance/getCurrentBalance", "", "" },
        { "Finance.GetStatistics", (http, config) => new FinanceService(http, config).GetStatisticsAsync(Ct), "GET", "/v3/finance/payment/statistics", "", "" },
        { "Finance.GetSplitStatistics", (http, config) => new FinanceService(http, config).GetSplitStatisticsAsync(Ct), "GET", "/v3/finance/split/statistics", "", "" },
        { "FinancialTransaction.List", (http, config) => new FinancialTransactionService(http, config).ListFinancialTransactionsAsync(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31), 5, 10, Ct), "GET", "/v3/financialTransactions", "?startDate=2024-01-01&finishDate=2024-01-31&offset=5&limit=10", "" },
        { "FinancialTransaction.List.NoFilters", (http, config) => new FinancialTransactionService(http, config).ListFinancialTransactionsAsync(cancellationToken: Ct), "GET", "/v3/financialTransactions", "", "" },
        { "FinancialTransaction.ByAnticipation", (http, config) => new FinancialTransactionService(http, config).GetPaymentAnticipationFinancialTransactionAsync("ant_1", Ct), "GET", "/v3/financialTransactions", "?anticipation=ant_1", "" },
        { "FinancialTransaction.ByPayment", (http, config) => new FinancialTransactionService(http, config).GetPaymentFinancialTransactionAsync("pay_1", Ct), "GET", "/v3/financialTransactions", "?payment=pay_1", "" },

        // Fiscal info and invoices
        { "FiscalInfo.Get", (http, config) => new FiscalInfoService(http, config).GetFiscalInfoAsync(Ct), "GET", "/v3/fiscalInfo", "", "" },
        { "FiscalInfo.GetMunicipality", (http, config) => new FiscalInfoService(http, config).GetMunicipalityAsync("3550308", Ct), "GET", "/v3/fiscalInfo/municipality", "?municipalityInscription=3550308", "" },
        { "FiscalInfo.Update", (http, config) => new FiscalInfoService(http, config).UpdateFiscalInfoAsync(new FiscalInfoSaveRequestDto { Email = "nf@b.c" }, Ct), "PUT", "/v3/fiscalInfo", "", null },
        { "FiscalInfo.ListMunicipalConfigurations", (http, config) => new FiscalInfoService(http, config).ListMunicipalConfigurationsAsync(Ct), "GET", "/v3/fiscalInfo/municipalOptions", "", "" },
        { "FiscalInfo.RetrieveTaxInformation", (http, config) => new FiscalInfoService(http, config).RetrieveTaxInformationAsync(Ct), "GET", "/v3/fiscalInfo/", "", "" },
        { "FiscalInfo.ListMunicipalServices", (http, config) => new FiscalInfoService(http, config).ListMunicipalServicesAsync(new ListMunicipalServicesParameters { Offset = 5, Limit = 10, Description = "dev" }, Ct), "GET", "/v3/fiscalInfo/services", "?offset=5&limit=10&description=dev", "" },
        { "FiscalInfo.ListMunicipalServices.NoFilters", (http, config) => new FiscalInfoService(http, config).ListMunicipalServicesAsync(cancellationToken: Ct), "GET", "/v3/fiscalInfo/services", "", "" },
        { "FiscalInfo.ListNbsCodes", (http, config) => new FiscalInfoService(http, config).ListNbsCodesAsync(new ListNbsCodesParameters { Offset = 5, Limit = 10, CodeDescription = "software" }, Ct), "GET", "/v3/fiscalInfo/nbsCodes", "?offset=5&limit=10&codeDescription=software", "" },
        { "FiscalInfo.ListNbsCodes.NoFilters", (http, config) => new FiscalInfoService(http, config).ListNbsCodesAsync(cancellationToken: Ct), "GET", "/v3/fiscalInfo/nbsCodes", "", "" },
        { "FiscalInfo.ConfigurePortal", (http, config) => new FiscalInfoService(http, config).ConfigureInvoiceIssuingPortalAsync(new FiscalInfoUpdateUseNationalPortalRequestDto { UseNationalPortal = true }, Ct), "POST", "/v3/fiscalInfo/nationalPortal", "", """{"useNationalPortal":true}""" },
        { "FiscalInfo.ConfigurePortal.Default", (http, config) => new FiscalInfoService(http, config).ConfigureInvoiceIssuingPortalAsync(cancellationToken: Ct), "POST", "/v3/fiscalInfo/nationalPortal", "", "{}" },
        { "Invoice.List", (http, config) => new InvoiceService(http, config).ListInvoicesAsync(5, 10, Ct), "GET", "/v3/invoices", "?offset=5&limit=10", "" },
        { "Invoice.Create", (http, config) => new InvoiceService(http, config).CreateInvoiceAsync(new InvoiceSaveRequestDto(), Ct), "POST", "/v3/invoices", "", null },
        { "Invoice.Cancel", (http, config) => new InvoiceService(http, config).CancelInvoiceAsync("inv_1", new InvoiceCancelRequestDto { CancelOnlyOnAsaas = true }, Ct), "POST", "/v3/invoices/inv_1/cancel", "", """{"cancelOnlyOnAsaas":true}""" },
        { "Invoice.Cancel.NoReason", (http, config) => new InvoiceService(http, config).CancelInvoiceAsync("inv_1", cancellationToken: Ct), "POST", "/v3/invoices/inv_1/cancel", "", "" },

        // Payments
        { "Payment.List", (http, config) => new PaymentService(http, config).ListPaymentsAsync(new ListPaymentsParameters { Customer = "cus_1", BillingType = "PIX", Status = "PENDING", Subscription = "sub_1", Installment = "ins_1", ExternalReference = "ref", Offset = 5, Limit = 10 }, Ct), "GET", "/v3/payments", "?customer=cus_1&billingType=PIX&status=PENDING&subscription=sub_1&installment=ins_1&externalReference=ref&offset=5&limit=10", "" },
        { "Payment.List.NoFilters", (http, config) => new PaymentService(http, config).ListPaymentsAsync(cancellationToken: Ct), "GET", "/v3/payments", "", "" },
        { "Payment.Create", (http, config) => new PaymentService(http, config).CreatePaymentAsync(new PaymentSaveRequestDto { Customer = "cus_1", BillingType = "PIX", Value = 10.5m }, Ct), "POST", "/v3/payments", "", """{"customer":"cus_1","billingType":"PIX","value":10.5}""" },
        { "Payment.CreateWithCreditCard", (http, config) => new PaymentService(http, config).CreateNewPaymentWithCreditCardAsync(new PaymentSaveWithCreditCardRequestDto(), Ct), "POST", "/v3/payments/", "", null },
        { "Payment.Simulate", (http, config) => new PaymentService(http, config).SalesSimulatorAsync(new PaymentSimulateRequestDto { Value = 100m, BillingType = "BOLETO" }, Ct), "POST", "/v3/payments/simulate", "", """{"value":100,"billingType":"BOLETO"}""" },
        { "Payment.Simulate.Default", (http, config) => new PaymentService(http, config).SalesSimulatorAsync(cancellationToken: Ct), "POST", "/v3/payments/simulate", "", """{"value":0}""" },
        { "Payment.GetLimits", (http, config) => new PaymentService(http, config).RecoveringPaymentLimitsAsync(Ct), "GET", "/v3/payments/limits", "", "" },

        // Typed list overloads that take offset and limit
        { "Installment.List", (http, config) => new InstallmentService(http, config).ListInstallmentsAsync(5, 10, Ct), "GET", "/v3/installments", "?offset=5&limit=10", "" },
        { "PaymentLink.List", (http, config) => new PaymentLinkService(http, config).ListPaymentLinksAsync(5, 10, Ct), "GET", "/v3/paymentLinks", "?offset=5&limit=10", "" },
        { "Subscription.List", (http, config) => new SubscriptionService(http, config).ListSubscriptionsAsync(5, 10, Ct), "GET", "/v3/subscriptions", "?offset=5&limit=10", "" },
        { "Pix.CreateKey", (http, config) => new PixService(http, config).CreateKeyAsync(new PixAddressKeySaveRequestDto { Type = "EVP" }, Ct), "POST", "/v3/pix/addressKeys", "", """{"type":"EVP"}""" },
        { "Pix.ListKeys",(http, config) => new PixService(http, config).ListKeysAsync(5, 10, Ct), "GET", "/v3/pix/addressKeys", "?offset=5&limit=10", "" },
        { "Pix.ListKeys.Parameters", (http, config) => new PixService(http, config).ListKeysAsync(new ListPixAddressKeysParameters { Offset = 5, Limit = 10, Status = "ACTIVE", StatusList = "ACTIVE,AWAITING_ACTIVATION" }, Ct), "GET", "/v3/pix/addressKeys", "?offset=5&limit=10&status=ACTIVE&statusList=ACTIVE%2cAWAITING_ACTIVATION", "" },
        { "PixTransaction.List", (http, config) => new PixTransactionService(http, config).ListTransactionsAsync(5, 10, Ct), "GET", "/v3/pix/transactions", "?offset=5&limit=10", "" },
        { "PixTransaction.List.Parameters", (http, config) => new PixTransactionService(http, config).ListTransactionsAsync(new ListPixTransactionsParameters { Offset = 5, Limit = 10, Status = "DONE", Type = "DEBIT", EndToEndIdentifier = "E2E" }, Ct), "GET", "/v3/pix/transactions", "?offset=5&limit=10&status=DONE&type=DEBIT&endToEndIdentifier=E2E", "" },
        { "RecurringPix.List", (http, config) => new RecurringPixService(http, config).ListRecurringTransactionsAsync(5, 10, Ct), "GET", "/v3/pix/transactions/recurrings", "?offset=5&limit=10", "" },
        { "Transfer.List", (http, config) => new TransferService(http, config).ListTransfersAsync(5, 10, Ct), "GET", "/v3/transfers", "?offset=5&limit=10", "" },
        { "Webhook.List", (http, config) => new WebhookService(http, config).ListWebhooksAsync(5, 10, Ct), "GET", "/v3/webhooks", "?offset=5&limit=10", "" },
        { "Subaccount.List", (http, config) => new SubaccountService(http, config).ListSubaccountsAsync(5, 10, Ct), "GET", "/v3/accounts", "?offset=5&limit=10", "" },
        { "Subaccount.List.CpfCnpj", (http, config) => new SubaccountService(http, config).ListSubaccountsAsync(cancellationToken: Ct, cpfCnpj: "12345678909"), "GET", "/v3/accounts", "?cpfCnpj=12345678909", "" },
        { "Chargeback.List.Legacy", (http, config) => new ChargebackService(http, config).ListChargebacksAsync(5, 10, "REQUESTED", Ct), "GET", "/v3/chargebacks", "?offset=5&limit=10&status=REQUESTED", "" },

        // Creates whose bodies are checked
        { "Transfer.Create", (http, config) => new TransferService(http, config).CreateTransferAsync(new TransferSaveRequestDto { Value = 50m, PixAddressKey = "key", PixAddressKeyType = "EMAIL" }, Ct), "POST", "/v3/transfers", "", """{"value":50,"pixAddressKey":"key","pixAddressKeyType":"EMAIL"}""" },
        { "Transfer.Create.BankAccount", (http, config) => new TransferService(http, config).CreateTransferAsync(new TransferSaveRequestDto { Value = 50m, BankAccount = new TransferBankAccountSaveRequestDto { Bank = new TransferBankSaveRequestDto { Code = "341" }, Agency = "0001", Account = "12345" } }, Ct), "POST", "/v3/transfers", "", """{"value":50,"bankAccount":{"bank":{"code":"341"},"agency":"0001","account":"12345"}}""" },
        { "Transfer.CreateInternal", (http, config) => new TransferService(http, config).CreateInternalTransferAsync(new TransferSaveInternalTransferRequestDto { Value = 50m, Account = new TransferSaveInternalTransferAccountDto { WalletId = "w1" } }, Ct), "POST", "/v3/transfers/internal", "", """{"value":50,"account":{"walletId":"w1"}}""" },
        { "Webhook.Create", (http, config) => new WebhookService(http, config).CreateWebhookAsync(new WebhookConfigSaveRequestDto { Url = "https://hook.example", Email = "a@b.c", Events = ["PAYMENT_RECEIVED"] }, Ct), "POST", "/v3/webhooks", "", """{"url":"https://hook.example","email":"a@b.c","events":["PAYMENT_RECEIVED"]}""" },
        { "Subaccount.Create", (http, config) => new SubaccountService(http, config).CreateSubaccountAsync(ValidSubaccount(), Ct), "POST", "/v3/accounts", "", null },
        { "Sandbox.ReceivePix", (http, config) => new SandboxActionsService(http, config).SimulatePixTransactionReceivedAsync(new { value = 10m }, Ct), "POST", "/v3/sandbox/pix/transactions/receive", "", """{"value":10}""" },
#pragma warning disable CS0618 // the obsolete route is still shipped, so it is still tested
        { "PixTransaction.Create.Obsolete", (http, config) => new PixTransactionService(http, config).CreateTransactionAsync(new { value = 10m }, Ct), "POST", "/v3/pix/transactions", "", """{"value":10}""" },
#pragma warning restore CS0618
    };

    [Theory]
    [MemberData(nameof(Routes), DisableDiscoveryEnumeration = true)]
    public async Task Endpoint_ShouldSendItsDocumentedRoute(
        string name, Func<HttpClient, AsaasSdkConfig, Task> call, string method, string path, string query, string? body)
    {
        // Arrange
        var handler = new CaptureHandler();

        // Act
        await call(new HttpClient(handler), new AsaasSdkConfig { BaseUrl = BaseUrl });

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.True(method == request.Method.Method, $"{name}: expected {method}, sent {request.Method}");
        Assert.Equal(path, request.Path);
        Assert.Equal(query, request.Query);
        if (body is not null)
        {
            Assert.Equal(body, request.Body);
            Assert.Equal(body == "" ? null : "application/json", request.MediaType);
        }
    }

    [Fact]
    public async Task ListPaymentsWithSummaryData_ShouldSendTheFilters_AndAskForTheSummary()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new PaymentWithSummaryDataService(new HttpClient(handler), new AsaasSdkConfig { BaseUrl = BaseUrl });

        // Act
        await service.ListPaymentsWithSummaryDataAsync(
            "cus_1", "RECEIVED", new DateTime(2024, 1, 1), new DateTime(2024, 1, 31), 5, 10, Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/v3/payments", request.Path);
        var query = HttpUtility.ParseQueryString(request.Query);
        Assert.Equal("cus_1", query["customer"]);
        Assert.Equal("RECEIVED", query["status"]);
        Assert.Equal("2024-01-01", query["dueDateGe"]);
        Assert.Equal("2024-01-31", query["dueDateLe"]);
        Assert.Equal("5", query["offset"]);
        Assert.Equal("10", query["limit"]);
        Assert.Equal("true", query["includeSummary"], ignoreCase: true);
    }

    [Fact]
    public async Task GetPaymentSummary_ShouldSendOnlyTheFiltersGiven()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new PaymentWithSummaryDataService(new HttpClient(handler), new AsaasSdkConfig { BaseUrl = BaseUrl });

        // Act
        await service.GetPaymentSummaryAsync(status: "OVERDUE", dueDateLe: new DateTime(2024, 12, 31), cancellationToken: Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal("/v3/payments/summary", request.Path);
        Assert.Equal("?status=OVERDUE&dueDateLe=2024-12-31", request.Query);
    }

    internal static AccountSaveRequestDto ValidSubaccount() => new()
    {
        Name = "Condomínio", Email = "c@b.c", CpfCnpj = "12345678000199", MobilePhone = "11999999999",
        IncomeValue = 5000m, Address = "Rua A", AddressNumber = "10", Province = "Centro", PostalCode = "01001000"
    };
}
