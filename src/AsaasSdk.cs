using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Services;
using Polly;
using Polly.Extensions.Http;
using Environment = Asaas.Sdk.Http.Environment;

namespace Asaas.Sdk;

/// <summary>
/// Main SDK client for Asaas API
/// API pública de integração com a plataforma Asaas.
/// </summary>
public class AsaasSdk : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly AsaasSdkConfig _config;
    private bool _disposed;

    /// <summary>
    /// Payment service for managing payments
    /// </summary>
    public PaymentService Payment { get; }

    /// <summary>
    /// Customer service for managing customers
    /// </summary>
    public CustomerService Customer { get; }

    /// <summary>
    /// Subscription service for managing subscriptions
    /// </summary>
    public SubscriptionService Subscription { get; }

    /// <summary>
    /// Installment service for managing installments
    /// </summary>
    public InstallmentService Installment { get; }

    /// <summary>
    /// PIX service for managing PIX operations
    /// </summary>
    public PixService Pix { get; }

    /// <summary>
    /// PIX transaction service for managing PIX transactions
    /// </summary>
    public PixTransactionService PixTransaction { get; }

    /// <summary>
    /// Transfer service for managing transfers
    /// </summary>
    public TransferService Transfer { get; }

    /// <summary>
    /// Notification service for managing notifications
    /// </summary>
    public NotificationService Notification { get; }

    /// <summary>
    /// Invoice service for managing invoices
    /// </summary>
    public InvoiceService Invoice { get; }

    /// <summary>
    /// Webhook service for managing webhooks
    /// </summary>
    public WebhookService Webhook { get; }

    /// <summary>
    /// Subaccount service for managing subaccounts
    /// </summary>
    public SubaccountService Subaccount { get; }

    /// <summary>
    /// Anticipation service for managing anticipations
    /// </summary>
    public AnticipationService Anticipation { get; }

    /// <summary>
    /// Recurring PIX service for managing recurring PIX
    /// </summary>
    public RecurringPixService RecurringPix { get; }

    /// <summary>
    /// Payment link service for managing payment links
    /// </summary>
    public PaymentLinkService PaymentLink { get; }

    /// <summary>
    /// Checkout service for managing checkout sessions
    /// </summary>
    public CheckoutService Checkout { get; }

    /// <summary>
    /// Payment dunning service for managing payment dunnings
    /// </summary>
    public PaymentDunningService PaymentDunning { get; }

    /// <summary>
    /// Bill service for managing bill payments
    /// </summary>
    public BillService Bill { get; }

    /// <summary>
    /// Mobile phone recharge service for managing mobile phone recharges
    /// </summary>
    public MobilePhoneRechargeService MobilePhoneRecharge { get; }

    /// <summary>
    /// Credit bureau report service for managing credit reports
    /// </summary>
    public CreditBureauReportService CreditBureauReport { get; }

    /// <summary>
    /// Financial transaction service for managing financial transactions
    /// </summary>
    public FinancialTransactionService FinancialTransaction { get; }

    /// <summary>
    /// Finance service for managing financial information
    /// </summary>
    public FinanceService Finance { get; }

    /// <summary>
    /// Account info service for managing account information
    /// </summary>
    public AccountInfoService AccountInfo { get; }

    /// <summary>
    /// Fiscal info service for managing fiscal information
    /// </summary>
    public FiscalInfoService FiscalInfo { get; }

    /// <summary>
    /// Account document service for managing account documents
    /// </summary>
    public AccountDocumentService AccountDocument { get; }

    /// <summary>
    /// Chargeback service for managing chargebacks
    /// </summary>
    public ChargebackService Chargeback { get; }

    /// <summary>
    /// Credit card service for managing credit card operations
    /// </summary>
    public CreditCardService CreditCard { get; }

    /// <summary>
    /// Payment refund service for managing payment refunds
    /// </summary>
    public PaymentRefundService PaymentRefund { get; }

    /// <summary>
    /// Payment split service for managing payment splits
    /// </summary>
    public PaymentSplitService PaymentSplit { get; }

    /// <summary>
    /// Escrow account service for managing escrow accounts
    /// </summary>
    public EscrowAccountService EscrowAccount { get; }

    /// <summary>
    /// Payment document service for managing payment documents
    /// </summary>
    public PaymentDocumentService PaymentDocument { get; }

    /// <summary>
    /// Payment with summary data service for payment operations with summary information
    /// </summary>
    public PaymentWithSummaryDataService PaymentWithSummaryData { get; }

    /// <summary>
    /// Sandbox actions service for sandbox environment operations
    /// </summary>
    public SandboxActionsService SandboxActions { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsaasSdk"/> class with default configuration.
    /// </summary>
    public AsaasSdk() : this(new AsaasSdkConfig())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsaasSdk"/> class.
    /// </summary>
    /// <param name="config">SDK configuration</param>
    public AsaasSdk(AsaasSdkConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));

        // Create retry policy
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => (int)msg.StatusCode == 429) // Too Many Requests
            .WaitAndRetryAsync(
                config.RetryConfig.MaxRetries,
                retryAttempt => config.RetryConfig.UseExponentialBackoff
                    ? TimeSpan.FromMilliseconds(Math.Min(
                        config.RetryConfig.InitialDelayMs * Math.Pow(2, retryAttempt - 1),
                        config.RetryConfig.MaxDelayMs))
                    : TimeSpan.FromMilliseconds(config.RetryConfig.InitialDelayMs)
            );

        // Create HTTP client with handlers
        var handler = new DefaultHeadersHandler(config)
        {
            InnerHandler = new HttpClientHandler()
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(config.BaseUrl ?? Environment.Default.GetUrl()),
            Timeout = config.Timeout
        };

        // Initialize services
        Payment = new PaymentService(_httpClient, _config);
        Customer = new CustomerService(_httpClient, _config);
        Subscription = new SubscriptionService(_httpClient, _config);
        Installment = new InstallmentService(_httpClient, _config);
        Pix = new PixService(_httpClient, _config);
        PixTransaction = new PixTransactionService(_httpClient, _config);
        Transfer = new TransferService(_httpClient, _config);
        Notification = new NotificationService(_httpClient, _config);
        Invoice = new InvoiceService(_httpClient, _config);
        Webhook = new WebhookService(_httpClient, _config);
        Subaccount = new SubaccountService(_httpClient, _config);
        Anticipation = new AnticipationService(_httpClient, _config);
        RecurringPix = new RecurringPixService(_httpClient, _config);
        PaymentLink = new PaymentLinkService(_httpClient, _config);
        Checkout = new CheckoutService(_httpClient, _config);
        PaymentDunning = new PaymentDunningService(_httpClient, _config);
        Bill = new BillService(_httpClient, _config);
        MobilePhoneRecharge = new MobilePhoneRechargeService(_httpClient, _config);
        CreditBureauReport = new CreditBureauReportService(_httpClient, _config);
        FinancialTransaction = new FinancialTransactionService(_httpClient, _config);
        Finance = new FinanceService(_httpClient, _config);
        AccountInfo = new AccountInfoService(_httpClient, _config);
        FiscalInfo = new FiscalInfoService(_httpClient, _config);
        AccountDocument = new AccountDocumentService(_httpClient, _config);
        Chargeback = new ChargebackService(_httpClient, _config);
        CreditCard = new CreditCardService(_httpClient, _config);
        PaymentRefund = new PaymentRefundService(_httpClient, _config);
        PaymentSplit = new PaymentSplitService(_httpClient, _config);
        EscrowAccount = new EscrowAccountService(_httpClient, _config);
        PaymentDocument = new PaymentDocumentService(_httpClient, _config);
        PaymentWithSummaryData = new PaymentWithSummaryDataService(_httpClient, _config);
        SandboxActions = new SandboxActionsService(_httpClient, _config);
    }

    /// <summary>
    /// Set the environment for API requests
    /// </summary>
    /// <param name="environment">Environment to use</param>
    public void SetEnvironment(Http.Environment environment)
    {
        _config.SetEnvironment(environment);
    }

    /// <summary>
    /// Set the base URL for API requests
    /// </summary>
    /// <param name="baseUrl">Base URL</param>
    public void SetBaseUrl(string baseUrl)
    {
        _config.BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
    }

    /// <summary>
    /// Set the API key for authentication
    /// </summary>
    /// <param name="apiKey">API key</param>
    public void SetApiKey(string apiKey)
    {
        _config.ApiKeyAuthConfig.ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
    }

    /// <summary>
    /// Set the API key header name
    /// </summary>
    /// <param name="apiKeyHeader">Header name</param>
    public void SetApiKeyHeader(string apiKeyHeader)
    {
        _config.ApiKeyAuthConfig.ApiKeyHeader = apiKeyHeader ?? throw new ArgumentNullException(nameof(apiKeyHeader));
    }

    /// <summary>
    /// Dispose of resources
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose of resources
    /// </summary>
    /// <param name="disposing">Whether to dispose managed resources</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
            }
            _disposed = true;
        }
    }
}
