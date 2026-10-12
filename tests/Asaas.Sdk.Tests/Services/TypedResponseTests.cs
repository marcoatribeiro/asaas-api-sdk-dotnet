using System.Net;
using System.Text.Json;
using System.Web;
using Asaas.Sdk.Config;
using Asaas.Sdk.Models;
using Asaas.Sdk.Services;

namespace Asaas.Sdk.Tests.Services;

/// <summary>
/// Subscription, installment, payment link and Pix responses are typed, instead of a dynamic that holds a JsonElement
/// and throws when a JSON field is read as a property (issue #7).
/// </summary>
public sealed class TypedResponseTests
{
    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    [Fact]
    public async Task Dynamic_ShouldHoldAJsonElement_ReadableThroughTheDocumentedCast()
    {
        // Arrange: every method that still returns dynamic goes through this conversion
        using var response = new HttpResponseMessage { Content = new StringContent("""{ "id": "pay_1" }""") };

        // Act
        dynamic result = await Asaas.Sdk.Http.ModelConverter.ConvertAsync<dynamic>(response, Ct);

        // Assert: the cast shown in the method docs works, JSON-property binding does not
        Assert.IsType<JsonElement>((object)result);
        Assert.Equal("pay_1", ((JsonElement)result).GetProperty("id").GetString());
        Assert.ThrowsAny<Exception>(() => { string id = result.id; });
    }

    [Fact]
    public async Task Subscription_ShouldReadTheDocumentedResponse_IncludingFutureStatusValues()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """
            { "object": "subscription", "id": "sub_1", "dateCreated": "2024-01-02", "customer": "cus_1",
              "paymentLink": null, "billingType": "CREDIT_CARD", "cycle": "MONTHLY", "value": 49.9,
              "nextDueDate": "2024-02-02", "endDate": null, "description": "Plan", "status": "SUSPENDED_SOMEDAY",
              "discount": { "value": 10, "dueDateLimitDays": 0, "type": "PERCENTAGE" },
              "fine": { "value": 2 }, "interest": { "value": 1 }, "deleted": false, "maxPayments": 12,
              "externalReference": "ref", "checkoutSession": "chk",
              "split": [ { "walletId": "w1", "fixedValue": 5, "status": "DISABLED", "disabledReason": "VALUE_DIVERGENCE" } ] }
            """);
        var service = new SubscriptionService(new HttpClient(handler), Config());

        // Act
        var subscription = await service.GetSubscriptionAsync("sub/1", Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/v3/subscriptions/sub%2F1", request.Path);
        Assert.Equal("sub_1", subscription.Id);
        Assert.Equal("MONTHLY", subscription.Cycle);
        Assert.Equal(49.9m, subscription.Value);
        Assert.Equal("SUSPENDED_SOMEDAY", subscription.Status);
        Assert.Null(subscription.PaymentLink);
        Assert.Equal("PERCENTAGE", subscription.Discount?.Type);
        Assert.Equal(1m, subscription.Interest?.Value);
        Assert.Equal("VALUE_DIVERGENCE", Assert.Single(subscription.Split!).DisabledReason);
    }

    [Fact]
    public async Task CreateSubscription_ShouldPostJson_WithoutTheFieldsLeftNull()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """{ "id": "sub_1" }""");
        var service = new SubscriptionService(new HttpClient(handler), Config());

        // Act
        var created = await service.CreateSubscriptionAsync(new SubscriptionSaveRequestDto
        {
            Customer = "cus_1",
            BillingType = "PIX",
            Value = 20,
            NextDueDate = "2024-05-01",
            Cycle = "MONTHLY",
            Split = [new SubscriptionSplitRequestDto { WalletId = "w1", PercentualValue = 10 }]
        }, Ct);

        // Assert
        Assert.Equal("sub_1", created.Id);
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/v3/subscriptions", request.Path);
        Assert.Equal("application/json", request.MediaType);
        using var body = JsonDocument.Parse(request.Body);
        var root = body.RootElement;
        Assert.Equal("cus_1", root.GetProperty("customer").GetString());
        Assert.Equal(20m, root.GetProperty("value").GetDecimal());
        Assert.Equal("w1", root.GetProperty("split")[0].GetProperty("walletId").GetString());
        Assert.False(root.TryGetProperty("description", out _));
        Assert.False(root.TryGetProperty("callback", out _));
    }

    [Fact]
    public async Task UpdateSubscription_ShouldPutOnlyWhatWasSet()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new SubscriptionService(new HttpClient(handler), Config());

        // Act
        await service.UpdateSubscriptionAsync(
            "sub_1", new SubscriptionUpdateRequestDto { Status = "ACTIVE", NextDueDate = "2024-06-01", UpdatePendingPayments = false }, Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.Equal("/v3/subscriptions/sub_1", request.Path);
        using var body = JsonDocument.Parse(request.Body);
        Assert.Equal(["status", "nextDueDate", "updatePendingPayments"],
            body.RootElement.EnumerateObject().Select(property => property.Name));
        Assert.False(body.RootElement.GetProperty("updatePendingPayments").GetBoolean());
    }

    [Fact]
    public async Task ListSubscriptions_ShouldSendTheFilters_WithLowercaseBooleans()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """
            { "object": "list", "hasMore": false, "totalCount": 1, "limit": 10, "offset": 0,
              "data": [ { "id": "sub_1", "status": "ACTIVE" } ] }
            """);
        var service = new SubscriptionService(new HttpClient(handler), Config());

        // Act
        var list = await service.ListSubscriptionsAsync(new ListSubscriptionsParameters
        {
            Limit = 10, Customer = "cus_1", Status = "ACTIVE", IncludeDeleted = true, DeletedOnly = false
        }, Ct);

        // Assert
        var query = HttpUtility.ParseQueryString(Assert.Single(handler.Requests).Query);
        Assert.Equal("10", query["limit"]);
        Assert.Equal("cus_1", query["customer"]);
        Assert.Equal("true", query["includeDeleted"]);
        Assert.Equal("false", query["deletedOnly"]);
        Assert.Null(query["offset"]);
        Assert.Equal("sub_1", Assert.Single(list.Data).Id);
    }

    [Fact]
    public async Task DeleteAndListPayments_ShouldReturnTypedResults()
    {
        // Arrange
        var deleted = new CaptureHandler(responseBody: """{ "deleted": true, "id": "sub_1" }""");
        var payments = new CaptureHandler(responseBody: """
            { "object": "list", "hasMore": false, "totalCount": 1, "limit": 10, "offset": 0,
              "data": [ { "id": "pay_1", "subscription": "sub_1", "status": "PENDING", "value": 49.9 } ] }
            """);

        // Act
        var deleteResult = await new SubscriptionService(new HttpClient(deleted), Config()).DeleteSubscriptionAsync("sub_1", Ct);
        var paymentList = await new SubscriptionService(new HttpClient(payments), Config())
            .ListSubscriptionPaymentsAsync("sub_1", status: "PENDING", cancellationToken: Ct);

        // Assert
        Assert.True(deleteResult.Deleted);
        Assert.Equal("sub_1", deleteResult.Id);
        Assert.Equal(HttpMethod.Delete, Assert.Single(deleted.Requests).Method);
        Assert.Equal("/v3/subscriptions/sub_1/payments", Assert.Single(payments.Requests).Path);
        Assert.Equal("PENDING", HttpUtility.ParseQueryString(payments.Requests[0].Query)["status"]);
        Assert.Equal("pay_1", Assert.Single(paymentList.Data).Id);
    }

    [Fact]
    public async Task Installment_ShouldReadTheDocumentedResponse()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """
            { "object": "installment", "id": "ins_1", "value": 300, "netValue": 290.5, "paymentValue": 100,
              "installmentCount": 3, "billingType": "BOLETO", "paymentDate": null, "expirationDay": 10,
              "dateCreated": "2024-01-02", "customer": "cus_1", "deleted": false, "threeDSecureChallengeUrl": null,
              "creditCard": { "creditCardNumber": "8829", "creditCardBrand": "VISA", "creditCardToken": "tok" },
              "chargeback": { "id": "cb_1", "status": "REQUESTED" },
              "refunds": [ { "status": "DONE", "value": 10, "paymentId": "pay_1",
                             "refundedSplits": [ { "id": "s1", "value": 2, "done": true } ] } ] }
            """);
        var service = new InstallmentService(new HttpClient(handler), Config());

        // Act
        var installment = await service.GetInstallmentAsync("ins_1", Ct);

        // Assert
        Assert.Equal(3, installment.InstallmentCount);
        Assert.Equal(290.5m, installment.NetValue);
        Assert.Equal("VISA", installment.CreditCard?.CreditCardBrand);
        Assert.Equal("cb_1", installment.Chargeback?.Id);
        var refund = Assert.Single(installment.Refunds!);
        Assert.Equal("pay_1", refund.PaymentId);
        Assert.True(Assert.Single(refund.RefundedSplits!).Done);
    }

    [Fact]
    public async Task CreateInstallment_ShouldPostJson_WithTheSplits()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """{ "id": "ins_1" }""");
        var service = new InstallmentService(new HttpClient(handler), Config());

        // Act
        await service.CreateInstallmentAsync(new InstallmentSaveRequestDto
        {
            InstallmentCount = 3, Customer = "cus_1", Value = 100, BillingType = "BOLETO", DueDate = "2024-05-01",
            Splits = [new InstallmentSplitRequestDto { WalletId = "w1", InstallmentNumber = 2, FixedValue = 5 }]
        }, Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal("/v3/installments", request.Path);
        using var body = JsonDocument.Parse(request.Body);
        Assert.Equal(3, body.RootElement.GetProperty("installmentCount").GetInt32());
        Assert.Equal(2, body.RootElement.GetProperty("splits")[0].GetProperty("installmentNumber").GetInt32());
        Assert.False(body.RootElement.TryGetProperty("totalValue", out _));
    }

    [Fact]
    public async Task PaymentLink_ShouldReadTheDocumentedResponse_AndSendLowercaseFilters()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """
            { "object": "list", "hasMore": false, "totalCount": 1, "limit": 10, "offset": 0,
              "data": [ { "id": "725104409743", "name": "Shop", "value": 99.9, "active": true, "chargeType": "DETACHED",
                          "url": "https://example.invalid/i/1", "billingType": "UNDEFINED", "viewCount": 4,
                          "notificationEnabled": true, "isAddressRequired": false, "endDate": null } ] }
            """);
        var service = new PaymentLinkService(new HttpClient(handler), Config());

        // Act
        var list = await service.ListPaymentLinksAsync(
            new ListPaymentLinksParameters { Active = true, IncludeDeleted = false, Name = "Shop" }, Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        var query = HttpUtility.ParseQueryString(request.Query);
        Assert.Equal("true", query["active"]);
        Assert.Equal("false", query["includeDeleted"]);
        var link = Assert.Single(list.Data);
        Assert.Equal("725104409743", link.Id);
        Assert.Equal(99.9m, link.Value);
        Assert.Equal(4, link.ViewCount);
        Assert.Null(link.EndDate);
    }

    [Fact]
    public async Task PaymentLinkImages_ShouldListFromImages_AndReadTheUploadResponse()
    {
        // Arrange
        var image = """{ "id": "img_1", "main": true, "image": { "originalName": "cover.png", "size": 50859, "extension": "png", "previewUrl": "p", "downloadUrl": "d" } }""";
        var listHandler = new CaptureHandler(responseBody: $$"""{ "object": "list", "hasMore": false, "totalCount": 1, "limit": 10, "offset": 0, "data": [ {{image}} ] }""");
        var addHandler = new CaptureHandler(responseBody: image);

        // Act
        var images = await new PaymentLinkService(new HttpClient(listHandler), Config()).ListPaymentLinkImagesAsync("link_1", Ct);
        var added = await new PaymentLinkService(new HttpClient(addHandler), Config())
            .AddPaymentLinkImageAsync("link_1", new MemoryStream([1, 2]), "cover.png", "image/png", cancellationToken: Ct);

        // Assert
        Assert.Equal("/v3/paymentLinks/link_1/images", Assert.Single(listHandler.Requests).Path);
        Assert.Equal("cover.png", Assert.Single(images.Data).Image?.OriginalName);
        Assert.True(added.Main);
        Assert.Equal(50859, added.Image?.Size);
    }

    [Fact]
    public async Task PaymentLink_ShouldCreateUpdateAndDelete_WithTypedBodies()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """{ "id": "link_1", "deleted": true }""");
        var service = new PaymentLinkService(new HttpClient(handler), Config());

        // Act
        await service.CreatePaymentLinkAsync(new PaymentLinkSaveRequestDto
        {
            Name = "Shop", BillingType = "UNDEFINED", ChargeType = "INSTALLMENT", MaxInstallmentCount = 3,
            Callback = new PaymentCallbackRequestDto { SuccessUrl = "https://example.invalid/ok", AutoRedirect = false }
        }, Ct);
        await service.UpdatePaymentLinkAsync("link_1", new PaymentLinkUpdateRequestDto { Active = false }, Ct);
        var deleted = await service.DeletePaymentLinkAsync("link_1", Ct);

        // Assert
        Assert.Equal([HttpMethod.Post, HttpMethod.Put, HttpMethod.Delete], handler.Requests.Select(r => r.Method));
        using var created = JsonDocument.Parse(handler.Requests[0].Body);
        Assert.Equal("INSTALLMENT", created.RootElement.GetProperty("chargeType").GetString());
        Assert.False(created.RootElement.GetProperty("callback").GetProperty("autoRedirect").GetBoolean());
        Assert.Equal("""{"active":false}""", handler.Requests[1].Body);
        Assert.True(deleted.Deleted);
    }

    [Fact]
    public async Task Pix_ShouldReadKeys_AndCallTheDocumentedQrCodeRoutes()
    {
        // Arrange
        var key = """{ "id": "k1", "key": "abc", "type": "EVP", "status": "AWAITING_ACTIVATION", "dateCreated": "2022-02-07 17:17:48", "canBeDeleted": false, "cannotBeDeletedReason": null, "qrCode": { "encodedImage": "aW1n", "payload": "000201" } }""";
        var keys = new CaptureHandler(responseBody: $$"""{ "data": [ {{key}} ] }""");
        var qr = new CaptureHandler(responseBody: """{ "id": "qr_1", "encodedImage": "aW1n", "payload": "000201", "allowsMultiplePayments": true, "expirationDate": "2030-01-01 10:00:00" }""");
        var deleted = new CaptureHandler(responseBody: """{ "id": "qr_1", "deleted": true }""");

        // Act
        var list = await new PixService(new HttpClient(keys), Config())
            .ListKeysAsync(new ListPixAddressKeysParameters { StatusList = "ACTIVE,AWAITING_ACTIVATION" }, Ct);
        var created = await new PixService(new HttpClient(qr), Config()).CreateQrCodeAsync(
            new PixQrCodeSaveRequestDto { AddressKey = "abc", Value = 50, AllowsMultiplePayments = false }, Ct);
        var removed = await new PixService(new HttpClient(deleted), Config()).DeleteQrCodeAsync("qr/1", Ct);

        // Assert
        var found = Assert.Single(list.Data);
        Assert.Equal("EVP", found.Type);
        Assert.False(found.CanBeDeleted);
        Assert.Equal("000201", found.QrCode?.Payload);
        Assert.Equal("ACTIVE,AWAITING_ACTIVATION", HttpUtility.ParseQueryString(keys.Requests[0].Query)["statusList"]);
        Assert.Equal("/v3/pix/qrCodes/static", qr.Requests[0].Path);
        Assert.Equal("qr_1", created.Id);
        Assert.Equal("""{"addressKey":"abc","value":50,"allowsMultiplePayments":false}""", qr.Requests[0].Body);
        Assert.Equal("/v3/pix/qrCodes/static/qr%2F1", deleted.Requests[0].Path);
        Assert.True(removed.Deleted);
    }

    [Fact]
    public async Task TypedMethods_ShouldHonourTheCancellationToken()
    {
        // Arrange
        var handler = new CaptureHandler();
        var client = new HttpClient(handler);
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        // Act & Assert
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => new SubscriptionService(client, Config()).GetSubscriptionAsync("sub_1", cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => new InstallmentService(client, Config()).GetInstallmentAsync("ins_1", cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => new PaymentLinkService(client, Config()).GetPaymentLinkAsync("link_1", cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => new PixService(client, Config()).GetKeyAsync("k1", cts.Token));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task TypedMethods_ShouldRejectABlankId_BeforeSending()
    {
        // Arrange
        var handler = new CaptureHandler();
        var client = new HttpClient(handler);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => new SubscriptionService(client, Config()).GetSubscriptionAsync(" ", Ct));
        await Assert.ThrowsAsync<ArgumentException>(() => new PaymentLinkService(client, Config()).DeletePaymentLinkAsync("", Ct));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task ApiErrors_ShouldStillSurfaceAsApiException()
    {
        // Arrange
        var handler = new CaptureHandler(HttpStatusCode.BadRequest, """{ "errors": [ { "code": "invalid_object", "description": "bad" } ] }""");

        // Act & Assert
        await Assert.ThrowsAsync<Asaas.Sdk.Exceptions.ApiException>(() =>
            new SubscriptionService(new HttpClient(handler), Config()).GetSubscriptionAsync("sub_1", Ct));
    }

    private static AsaasSdkConfig Config() => new() { BaseUrl = "https://api.example.invalid" };
}
