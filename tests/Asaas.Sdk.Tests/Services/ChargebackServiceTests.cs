using System.Net;
using System.Web;
using Asaas.Sdk.Config;
using Asaas.Sdk.Models;
using Asaas.Sdk.Services;

namespace Asaas.Sdk.Tests.Services;

/// <summary>
/// The chargeback operations must call the routes the Asaas documents (issue #6).
/// </summary>
public sealed class ChargebackServiceTests
{
    private static readonly byte[] FileBytes = [0x25, 0x50, 0x44, 0x46, 0x00, 0xFF];

    [Fact]
    public async Task GetPaymentChargeback_ShouldGetTheChargebackOfThePayment_AndReadTheResponse()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """
            {
              "id": "8e784c3e", "payment": "pay_1", "installment": null, "customerAccount": "cus_1",
              "status": "IN_DISPUTE", "reason": "COMMERCIAL_DISAGREEMENT", "disputeStartDate": "2024-11-10",
              "value": 2323.45, "paymentDate": "2024-03-10",
              "creditCard": { "number": "8829", "brand": "VISA" },
              "disputeStatus": "REQUESTED", "deadlineToSendDisputeDocuments": "2024-12-10"
            }
            """);
        var service = new ChargebackService(new HttpClient(handler), Config());

        // Act
        var chargeback = await service.GetPaymentChargebackAsync("pay/1 x", TestContext.Current.CancellationToken);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/v3/payments/pay%2F1%20x/chargeback", request.Path);
        Assert.Equal("8e784c3e", chargeback.Id);
        Assert.Equal("IN_DISPUTE", chargeback.Status);
        Assert.Equal("REQUESTED", chargeback.DisputeStatus);
        Assert.Equal(2323.45m, chargeback.Value);
        Assert.Equal("2024-12-10", chargeback.DeadlineToSendDisputeDocuments);
        Assert.Equal("8829", chargeback.CreditCard?.Number);
        Assert.Equal("VISA", chargeback.CreditCard?.Brand);
    }

    [Fact]
    public async Task CreateChargebackDispute_ShouldPostEveryFileAsAPartNamedFiles()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """
            { "chargebackId": "cb/1", "status": "REQUESTED", "files": ["a.pdf", "b.jpg"] }
            """);
        var service = new ChargebackService(new HttpClient(handler), Config());
        var other = new byte[] { 1, 2, 3 };

        // Act
        var dispute = await service.CreateChargebackDisputeAsync(
            "cb/1",
            [(new MemoryStream(FileBytes), "a.pdf", "application/pdf"), (new MemoryStream(other), "b.jpg", "image/jpeg")],
            TestContext.Current.CancellationToken);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/v3/chargebacks/cb%2F1/dispute", request.Path);
        Assert.Equal("multipart/form-data", request.MediaType);
        Assert.Equal(["files", "files"], request.PartNames);
        var files = request.Files("files");
        Assert.Equal(["a.pdf", "b.jpg"], files.Select(file => file.FileName));
        Assert.Equal(["application/pdf", "image/jpeg"], files.Select(file => file.ContentType));
        Assert.Equal(FileBytes, files[0].Bytes);
        Assert.Equal(other, files[1].Bytes);
        Assert.Equal("cb/1", dispute.ChargebackId);
        Assert.Equal("REQUESTED", dispute.Status);
        Assert.Equal(["a.pdf", "b.jpg"], dispute.Files);
    }

    [Fact]
    public async Task CreateChargebackDispute_ShouldAcceptEleven_AndRejectTwelveOrNone_BeforeSending()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new ChargebackService(new HttpClient(handler), Config());
        List<(Stream, string, string)> Files(int count) =>
            Enumerable.Range(0, count).Select(i => ((Stream)new MemoryStream(FileBytes), $"{i}.pdf", "application/pdf")).ToList();

        // Act
        await service.CreateChargebackDisputeAsync("cb_1", Files(11), TestContext.Current.CancellationToken);
        var tooMany = Files(12);
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateChargebackDisputeAsync("cb_1", tooMany, TestContext.Current.CancellationToken));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateChargebackDisputeAsync("cb_1", [], TestContext.Current.CancellationToken));

        // Assert: only the valid dispute was sent
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task CreateChargebackDispute_ShouldDisposeTheStreams_WhenSent_AndLeaveThemWhenRejected()
    {
        // Arrange
        var service = new ChargebackService(new HttpClient(new CaptureHandler()), Config());
        var sent = new TrackedStream(FileBytes);
        var rejected = new TrackedStream(FileBytes);

        // Act
        await service.CreateChargebackDisputeAsync(
            "cb_1", [(sent, "a.pdf", "application/pdf")], TestContext.Current.CancellationToken);
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateChargebackDisputeAsync(
            "cb_1", [(rejected, "", "application/pdf")], TestContext.Current.CancellationToken));

        // Assert
        Assert.True(sent.Disposed);
        Assert.False(rejected.Disposed);
    }

    [Fact]
    public async Task CreateChargebackDispute_ShouldDisposeTheStreams_WhenTheRequestFails()
    {
        // Arrange
        var service = new ChargebackService(new HttpClient(new CaptureHandler(HttpStatusCode.BadRequest)), Config());
        var stream = new TrackedStream(FileBytes);

        // Act & Assert
        await Assert.ThrowsAsync<Asaas.Sdk.Exceptions.ApiException>(() => service.CreateChargebackDisputeAsync(
            "cb_1", [(stream, "a.png", "image/png")], TestContext.Current.CancellationToken));
        Assert.True(stream.Disposed);
    }

    [Fact]
    public async Task ChargebackOperations_ShouldHonourTheCancellationToken_AndDisposeTheStreams()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new ChargebackService(new HttpClient(handler), Config());
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var stream = new TrackedStream(FileBytes);

        // Act & Assert
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => service.CreateChargebackDisputeAsync(
            "cb_1", [(stream, "a.pdf", "application/pdf")], cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            service.GetPaymentChargebackAsync("pay_1", cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            service.ListChargebacksAsync(new ListChargebacksParameters(), cts.Token));

        Assert.Empty(handler.Requests);
        Assert.True(stream.Disposed);
    }

    [Fact]
    public async Task ListChargebacks_ShouldSendTheBrandAndDateRangeFilters_WithTheirBracketedKeys()
    {
        // Arrange
        var handler = new CaptureHandler(responseBody: """
            { "object": "list", "hasMore": true, "totalCount": 3, "limit": 1, "offset": 2,
              "data": [ { "id": "cb_1", "status": "DONE", "disputeStatus": "ACCEPTED" } ] }
            """);
        var service = new ChargebackService(new HttpClient(handler), Config());
        var previous = System.Globalization.CultureInfo.CurrentCulture;

        // Act
        ChargebackListResponseDto list;
        try
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("ar-SA");
            list = await service.ListChargebacksAsync(
                new ListChargebacksParameters
                {
                    Offset = 2,
                    Limit = 1,
                    Status = "IN_DISPUTE",
                    CreditCardBrand = "ELO",
                    OriginDisputeDateGreaterOrEqual = new DateOnly(2023, 11, 1),
                    OriginDisputeDateLessOrEqual = new DateOnly(2024, 12, 5),
                    OriginTransactionDateGreaterOrEqual = new DateOnly(2024, 10, 10),
                    OriginTransactionDateLessOrEqual = new DateOnly(2024, 12, 6)
                },
                TestContext.Current.CancellationToken);
        }
        finally
        {
            System.Globalization.CultureInfo.CurrentCulture = previous;
        }

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/v3/chargebacks", request.Path);
        var query = HttpUtility.ParseQueryString(request.Query);
        Assert.Equal("2", query["offset"]);
        Assert.Equal("1", query["limit"]);
        Assert.Equal("IN_DISPUTE", query["status"]);
        Assert.Equal("ELO", query["creditCardBrand"]);
        Assert.Equal("2023-11-01", query["originDisputeDate[ge]"]);
        Assert.Equal("2024-12-05", query["originDisputeDate[le]"]);
        Assert.Equal("2024-10-10", query["originTransactionDate[ge]"]);
        Assert.Equal("2024-12-06", query["originTransactionDate[le]"]);
        Assert.True(list.HasMore);
        Assert.Equal(3, list.TotalCount);
        Assert.Equal("ACCEPTED", Assert.Single(list.Data).DisputeStatus);
    }

    [Fact]
    public async Task ListChargebacks_ShouldSendNoQuery_WhenNothingIsFiltered()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new ChargebackService(new HttpClient(handler), Config());

        // Act
        await service.ListChargebacksAsync(new ListChargebacksParameters(), TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal("", Assert.Single(handler.Requests).Query);
    }

    private static AsaasSdkConfig Config() => new() { BaseUrl = "https://api.example.invalid" };
}
