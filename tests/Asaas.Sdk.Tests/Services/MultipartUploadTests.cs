using System.Globalization;
using System.Net;
using System.Text;
using Asaas.Sdk.Config;
using Asaas.Sdk.Models;
using Asaas.Sdk.Services;

namespace Asaas.Sdk.Tests.Services;

/// <summary>
/// Uploads the Asaas documents as multipart/form-data with a binary file part must not go out as JSON (issue #5).
/// </summary>
public sealed class MultipartUploadTests
{
    private static readonly byte[] FileBytes = [0x00, 0x01, 0xFE, 0xFF, 0x50, 0x4B];

    [Fact]
    public async Task CreateAndUpdateTaxInformation_ShouldPostMultipart_WithScalarFieldsAndTheCertificateBytes()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new FiscalInfoService(new HttpClient(handler), Config());
        var fiscalInfo = new FiscalInfoSaveRequestDto
        {
            Email = "test@example.invalid",
            SimplesNacional = false,
            CulturalProjectsPromoter = true,
            RpsNumber = 1234567,
            CertificatePassword = "secret",
            CertificateFile = FileBytes
        };

        // Act
        await service.CreateAndUpdateTaxInformationAsync(fiscalInfo, TestContext.Current.CancellationToken);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/v3/fiscalInfo/", request.Path);
        Assert.Equal("multipart/form-data", request.MediaType);
        Assert.Equal("test@example.invalid", request.Text("email"));
        Assert.Equal("false", request.Text("simplesNacional"));
        Assert.Equal("true", request.Text("culturalProjectsPromoter"));
        Assert.Equal("1234567", request.Text("rpsNumber"));
        Assert.Equal("secret", request.Text("certificatePassword"));
        var certificate = request.File("certificateFile");
        Assert.Equal("certificate.pfx", certificate.FileName);
        Assert.Equal("application/x-pkcs12", certificate.ContentType);
        Assert.Equal(FileBytes, certificate.Bytes);
    }

    [Fact]
    public async Task CreateAndUpdateTaxInformation_ShouldOmitTheFieldsLeftNull()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new FiscalInfoService(new HttpClient(handler), Config());

        // Act
        await service.CreateAndUpdateTaxInformationAsync(
            new FiscalInfoSaveRequestDto { Email = "test@example.invalid" }, TestContext.Current.CancellationToken);

        // Assert: nothing but the two non-nullable fields, so no remote value is overwritten
        var request = Assert.Single(handler.Requests);
        Assert.Equal(["email", "simplesNacional"], request.PartNames);
    }

    [Fact]
    public async Task CreateAndUpdateTaxInformation_ShouldSendServiceProvisionCityDefaultType_WhenGiven()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new FiscalInfoService(new HttpClient(handler), Config());

        // Act
        await service.CreateAndUpdateTaxInformationAsync(new FiscalInfoSaveRequestDto
        {
            Email = "test@example.invalid",
            ServiceProvisionCityDefaultType = "CUSTOMER_CITY"
        }, TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal("CUSTOMER_CITY", Assert.Single(handler.Requests).Text("serviceProvisionCityDefaultType"));
    }

    [Fact]
    public async Task CreateAndUpdateTaxInformation_ShouldSendTheCertificateStream_AndDisposeIt()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new FiscalInfoService(new HttpClient(handler), Config());
        var stream = new TrackedStream(FileBytes);

        // Act
        await service.CreateAndUpdateTaxInformationAsync(
            new FiscalInfoSaveRequestDto { Email = "test@example.invalid", CertificateFile = [9, 9, 9] },
            stream, "my cert.p12", "application/x-pkcs12", TestContext.Current.CancellationToken);

        // Assert: the stream wins over the DTO bytes, and is the SDK's to dispose afterwards
        var certificate = Assert.Single(handler.Requests).File("certificateFile");
        Assert.Equal("my cert.p12", certificate.FileName);
        Assert.Equal("application/x-pkcs12", certificate.ContentType);
        Assert.Equal(FileBytes, certificate.Bytes);
        Assert.True(stream.Disposed);
    }

    [Fact]
    public async Task UploadPaymentDocument_ShouldPostMultipart_WithFileAndRequiredFields()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new PaymentDocumentService(new HttpClient(handler), Config());

        // Act
        await service.UploadPaymentDocumentAsync(
            "pay/test 1", new MemoryStream(FileBytes), "contract.pdf", "application/pdf", true, "CONTRACT", TestContext.Current.CancellationToken);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/v3/payments/pay%2Ftest%201/documents", request.Path);
        Assert.Equal("multipart/form-data", request.MediaType);
        Assert.Equal(["file", "availableAfterPayment", "type"], request.PartNames);
        var file = request.File("file");
        Assert.Equal("contract.pdf", file.FileName);
        Assert.Equal("application/pdf", file.ContentType);
        Assert.Equal(FileBytes, file.Bytes);
        Assert.Equal("true", request.Text("availableAfterPayment"));
        Assert.Equal("CONTRACT", request.Text("type"));
    }

    [Fact]
    public async Task UploadPaymentDocument_ShouldSendFalseInLowercase_AndIgnoreTheCurrentCulture()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new PaymentDocumentService(new HttpClient(handler), Config());
        var previous = CultureInfo.CurrentCulture;

        // Act
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("tr-TR");
            await service.UploadPaymentDocumentAsync(
                "pay_test", new MemoryStream(FileBytes), "a.pdf", "application/pdf", false, "OTHER", TestContext.Current.CancellationToken);
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }

        // Assert
        Assert.Equal("false", Assert.Single(handler.Requests).Text("availableAfterPayment"));
    }

    [Fact]
    public async Task AddPaymentLinkImage_ShouldPostMultipartToImages_WithImageAndMain()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new PaymentLinkService(new HttpClient(handler), Config());

        // Act
        await service.AddPaymentLinkImageAsync("link/test", new MemoryStream(FileBytes), "logo.png", "image/png", main: true, cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/v3/paymentLinks/link%2Ftest/images", request.Path);
        Assert.Equal("multipart/form-data", request.MediaType);
        Assert.Equal(["image", "main"], request.PartNames);
        var image = request.File("image");
        Assert.Equal("logo.png", image.FileName);
        Assert.Equal("image/png", image.ContentType);
        Assert.Equal(FileBytes, image.Bytes);
        Assert.Equal("true", request.Text("main"));
    }

    [Fact]
    public async Task AddPaymentLinkImage_ShouldOmitMain_WhenNotGiven()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new PaymentLinkService(new HttpClient(handler), Config());

        // Act
        await service.AddPaymentLinkImageAsync("link_test", new MemoryStream(FileBytes), "logo.png", "image/png", cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(["image"], Assert.Single(handler.Requests).PartNames);
    }

    [Fact]
    public async Task Uploads_ShouldDisposeTheStream_WhenTheRequestFails()
    {
        // Arrange
        var handler = new CaptureHandler(HttpStatusCode.BadRequest);
        var service = new PaymentLinkService(new HttpClient(handler), Config());
        var stream = new TrackedStream(FileBytes);

        // Act & Assert
        await Assert.ThrowsAsync<Asaas.Sdk.Exceptions.ApiException>(() =>
            service.AddPaymentLinkImageAsync(
                "link_test", stream, "logo.png", "image/png", cancellationToken: TestContext.Current.CancellationToken));
        Assert.True(stream.Disposed);
    }

    [Fact]
    public async Task Uploads_ShouldHonourTheCancellationToken_AndDisposeTheStream()
    {
        // Arrange
        var handler = new CaptureHandler();
        var client = new HttpClient(handler);
        var config = Config();
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var streams = Enumerable.Range(0, 3).Select(_ => new TrackedStream(FileBytes)).ToArray();

        // Act & Assert
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            new FiscalInfoService(client, config).CreateAndUpdateTaxInformationAsync(
                new FiscalInfoSaveRequestDto { Email = "test@example.invalid" },
                streams[0], "c.pfx", "application/x-pkcs12", cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            new PaymentDocumentService(client, config).UploadPaymentDocumentAsync(
                "pay_test", streams[1], "a.pdf", "application/pdf", true, "OTHER", cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            new PaymentLinkService(client, config).AddPaymentLinkImageAsync(
                "link_test", streams[2], "a.png", "image/png", null, cts.Token));

        Assert.Empty(handler.Requests);
        Assert.All(streams, stream => Assert.True(stream.Disposed));
    }

    private static AsaasSdkConfig Config() => new() { BaseUrl = "https://api.example.invalid" };
}
