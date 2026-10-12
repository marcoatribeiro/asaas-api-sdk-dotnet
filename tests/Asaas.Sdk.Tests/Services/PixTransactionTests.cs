using System.Web;
using Asaas.Sdk.Config;
using Asaas.Sdk.Models;
using Asaas.Sdk.Services;

namespace Asaas.Sdk.Tests.Services;

/// <summary>
/// Pix transactions and recurring Pix call the routes the Asaas documents and return typed responses (issue #7).
/// </summary>
public sealed class PixTransactionTests
{
    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    private const string TransactionJson = """
        { "id": "tx_1", "endToEndIdentifier": "E123", "finality": "WITHDRAWAL", "value": 10.5, "refundedValue": 0,
          "effectiveDate": "2024-03-10 10:00:00", "scheduledDate": "2024-03-15", "status": "SCHEDULED_SOMEDAY",
          "type": "DEBIT", "originType": "DYNAMIC_QRCODE", "canBeCanceled": true, "payment": null,
          "externalAccount": { "ispb": "0", "name": "Shop", "cpfCnpj": "123", "addressKeyType": "EVP" },
          "qrCode": { "payer": { "name": "Ann" }, "originalValue": 10, "dueDate": "2024-03-20", "interest": 0.5 },
          "originalTransaction": { "id": "tx_0", "value": 1 } }
        """;

    [Fact]
    public async Task PayQrCode_ShouldPostToTheDocumentedRoute_AndReadTheTransaction()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: TransactionJson);
        var service = new PixTransactionService(new HttpClient(handler), Config());

        // Act
        var transaction = await service.PayQrCodeAsync(new PixTransactionSaveRequestDto
        {
            QrCode = new PixTransactionQrCodeRequestDto { Payload = "000201" },
            Value = 10.5m,
            ScheduleDate = "2024-03-15"
        }, Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/v3/pix/qrCodes/pay", request.Path);
        Assert.Equal("""{"qrCode":{"payload":"000201"},"value":10.5,"scheduleDate":"2024-03-15"}""", request.Body);
        Assert.Equal("tx_1", transaction.Id);
        Assert.Equal(10.5m, transaction.Value);
        Assert.Equal("SCHEDULED_SOMEDAY", transaction.Status);
        Assert.True(transaction.CanBeCanceled);
        Assert.Null(transaction.Payment);
        Assert.Equal("EVP", transaction.ExternalAccount?.AddressKeyType);
        Assert.Equal("Ann", transaction.QrCode?.Payer?.Name);
        Assert.Equal(0.5m, transaction.QrCode?.Interest);
        Assert.Equal("tx_0", transaction.OriginalTransaction?.Id);
    }

    [Fact]
    public async Task GetCancelAndList_ShouldCallTheDocumentedRoutes_WithEscapedIds()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: TransactionJson);
        var list = new CaptureHandler(responseBody: $$"""{ "object": "list", "hasMore": false, "totalCount": 1, "limit": 10, "offset": 0, "data": [ {{TransactionJson}} ] }""");

        // Act
        var service = new PixTransactionService(new HttpClient(handler), Config());
        await service.GetTransactionAsync("tx/1", Ct);
        await service.CancelTransactionAsync("tx/1", Ct);
        var page = await new PixTransactionService(new HttpClient(list), Config()).ListTransactionsAsync(
            new ListPixTransactionsParameters { Limit = 10, Status = "DONE", Type = "DEBIT", EndToEndIdentifier = "E123" }, Ct);

        // Assert
        Assert.Equal([HttpMethod.Get, HttpMethod.Post], handler.Requests.Select(r => r.Method));
        Assert.Equal(["/v3/pix/transactions/tx%2F1", "/v3/pix/transactions/tx%2F1/cancel"], handler.Requests.Select(r => r.Path));
        var query = HttpUtility.ParseQueryString(Assert.Single(list.Requests).Query);
        Assert.Equal("DONE", query["status"]);
        Assert.Equal("DEBIT", query["type"]);
        Assert.Equal("E123", query["endToEndIdentifier"]);
        Assert.Equal("tx_1", Assert.Single(page.Data).Id);
    }

    [Fact]
    public async Task DecodeQrCode_ShouldPostTypedRequest_AndReadTheResponse()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """
            { "payload": "000201", "type": "DYNAMIC", "transactionOriginType": "DYNAMIC_QRCODE", "value": 10,
              "totalValue": 10.5, "canBePaid": false, "cannotBePaidReason": "expired", "dueDate": "2024-03-20",
              "receiver": { "name": "Shop", "personType": "JURIDICA", "accountType": "PAYMENT_ACCOUNT" },
              "payer": { "name": "Ann", "cpfCnpj": "123" } }
            """);
        var service = new PixTransactionService(new HttpClient(handler), Config());

        // Act
        var decoded = await service.DecodeQrCodeAsync(
            new PixQrCodeDecodeRequestDto { Payload = "000201", ExpectedPaymentDate = "2024-03-20" }, Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal("/v3/pix/qrCodes/decode", request.Path);
        Assert.Equal("""{"payload":"000201","expectedPaymentDate":"2024-03-20"}""", request.Body);
        Assert.Equal("DYNAMIC", decoded.Type);
        Assert.Equal(10.5m, decoded.TotalValue);
        Assert.False(decoded.CanBePaid);
        Assert.Equal("expired", decoded.CannotBePaidReason);
        Assert.Equal("JURIDICA", decoded.Receiver?.PersonType);
        Assert.Equal("Ann", decoded.Payer?.Name);
    }

    [Fact]
    public async Task Recurring_ShouldCallThePluralRoutes_AndReadTheResponses()
    {
        // Arrange
        var recurrence = """{ "id": "rec_1", "status": "SCHEDULED", "origin": "PIX", "value": 0.02, "frequency": "WEEKLY", "quantity": 2, "startDate": "2024-03-01", "finishDate": "2024-03-08", "canBeCancelled": true, "externalAccount": { "name": "Shop", "financialInstitutionName": "Bank", "cpfCnpj": "123", "pixKey": "abc" } }""";
        var handler = new CaptureHandler(responseBody: recurrence);
        var service = new RecurringPixService(new HttpClient(handler), Config());

        // Act
        var got = await service.GetRecurringTransactionAsync("rec/1", Ct);
        await service.CancelRecurringTransactionAsync("rec/1", Ct);

        // Assert
        Assert.Equal(["/v3/pix/transactions/recurrings/rec%2F1", "/v3/pix/transactions/recurrings/rec%2F1/cancel"],
            handler.Requests.Select(r => r.Path));
        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
        Assert.Equal("WEEKLY", got.Frequency);
        Assert.Equal(0.02m, got.Value);
        Assert.Equal(2, got.Quantity);
        Assert.True(got.CanBeCancelled);
        Assert.Equal("abc", got.ExternalAccount?.PixKey);
    }

    [Fact]
    public async Task ListRecurring_ShouldSendFiltersWithInvariantNumbers_AndReadTheList()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """
            { "object": "list", "hasMore": true, "totalCount": 5, "limit": 1, "offset": 0,
              "data": [ { "id": "rec_1", "status": "DONE" } ] }
            """);
        var service = new RecurringPixService(new HttpClient(handler), Config());
        var previous = System.Globalization.CultureInfo.CurrentCulture;

        // Act
        PixRecurringTransactionListResponseDto list;
        try
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            list = await service.ListRecurringTransactionsAsync(
                new ListPixRecurringTransactionsParameters { Status = "DONE", Value = 100.5m, SearchText = "Shop" }, Ct);
        }
        finally
        {
            System.Globalization.CultureInfo.CurrentCulture = previous;
        }

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal("/v3/pix/transactions/recurrings", request.Path);
        var query = HttpUtility.ParseQueryString(request.Query);
        Assert.Equal("100.5", query["value"]);
        Assert.Equal("DONE", query["status"]);
        Assert.Equal("Shop", query["searchText"]);
        Assert.True(list.HasMore);
        Assert.Equal("rec_1", Assert.Single(list.Data).Id);
    }

    [Fact]
    public async Task RecurringItems_ShouldBeListed_AndCancelledOnTheDocumentedRoutes()
    {
        // Arrange
        var item = """{ "id": "it_1", "status": "REFUSED", "scheduledDate": "2024-03-08", "canBeCancelled": false, "recurrenceNumber": 2, "quantity": 2, "value": 0.02, "refusalReasonDescription": "no funds" }""";
        var listHandler = new CaptureHandler(responseBody: $$"""{ "data": [ {{item}} ] }""");
        var cancelHandler = new CaptureHandler(responseBody: item);

        // Act
        var items = await new RecurringPixService(new HttpClient(listHandler), Config())
            .ListRecurringTransactionItemsAsync("rec/1", limit: 5, cancellationToken: Ct);
        var cancelled = await new RecurringPixService(new HttpClient(cancelHandler), Config())
            .CancelRecurringTransactionItemAsync("it/1", Ct);

        // Assert
        Assert.Equal("/v3/pix/transactions/recurrings/rec%2F1/items", Assert.Single(listHandler.Requests).Path);
        Assert.Equal("5", HttpUtility.ParseQueryString(listHandler.Requests[0].Query)["limit"]);
        Assert.Equal("no funds", Assert.Single(items.Data).RefusalReasonDescription);
        var cancel = Assert.Single(cancelHandler.Requests);
        Assert.Equal(HttpMethod.Post, cancel.Method);
        Assert.Equal("/v3/pix/transactions/recurrings/items/it%2F1/cancel", cancel.Path);
        Assert.Equal(2, cancelled.RecurrenceNumber);
    }

    [Fact]
    public async Task PixMethods_ShouldHonourTheCancellationToken_AndRejectBlankIds()
    {
        // Arrange
        var handler = new CaptureHandler();
        var client = new HttpClient(handler);
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        // Act & Assert
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => new PixTransactionService(client, Config()).GetTransactionAsync("tx_1", cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => new RecurringPixService(client, Config()).CancelRecurringTransactionItemAsync("it_1", cts.Token));
        await Assert.ThrowsAsync<ArgumentException>(() => new PixTransactionService(client, Config()).CancelTransactionAsync(" ", Ct));
        await Assert.ThrowsAsync<ArgumentException>(() => new RecurringPixService(client, Config()).GetRecurringTransactionAsync("", Ct));
        Assert.Empty(handler.Requests);
    }

    private static AsaasSdkConfig Config() => new() { BaseUrl = "https://api.example.invalid" };
}
