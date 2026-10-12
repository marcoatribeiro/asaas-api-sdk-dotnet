using System.Text;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Tests.Models;

/// <summary>
/// Values the Asaas documents in SNAKE_CASE or upper case must reach the caller as sent, and go back to the Asaas
/// as given (issue #1). They used to be enums, but System.Text.Json ignores <c>EnumMemberAttribute</c>, so an
/// underscore value threw on read and the member name was sent on write.
/// </summary>
public sealed class SnakeCaseValuesTests
{
    [Theory]
    [InlineData("AWAITING_ACTION_AUTHORIZATION")]
    [InlineData("APPROVED")]
    public async Task ConvertAsync_ShouldReadTheAccountInfoStatus_AsSent(string status)
    {
        // Act
        var accountInfo = await ReadAsync<AccountInfoGetResponseDto>($$"""{ "status": "{{status}}" }""");

        // Assert
        Assert.Equal(status, accountInfo.Status);
    }

    [Theory]
    [InlineData("PROCESSING_CANCELLATION")]
    [InlineData("CANCELLATION_DENIED")]
    [InlineData("AUTHORIZED")]
    public async Task ConvertAsync_ShouldReadTheInvoiceStatus_AsSent(string status)
    {
        // Act
        var invoice = await ReadAsync<InvoiceGetResponseDto>($$"""{ "status": "{{status}}" }""");

        // Assert
        Assert.Equal(status, invoice.Status);
    }

    [Fact]
    public async Task ConvertAsync_ShouldReadTheAuthenticationType_AsSent()
    {
        // Act
        var options = await ReadAsync<FiscalInfoMunicipalOptionsGetResponseDto>(
            """{ "authenticationType": "USER_AND_PASSWORD" }""");

        // Assert
        Assert.Equal("USER_AND_PASSWORD", options.AuthenticationType);
    }

    [Fact]
    public async Task ConvertAsync_ShouldReadThePersonAndCompanyTypes_AsSent()
    {
        // Act
        var accountInfo = await ReadAsync<AccountInfoGetResponseDto>(
            """{ "personType": "JURIDICA", "companyType": "MEI" }""");

        // Assert
        Assert.Equal("JURIDICA", accountInfo.PersonType);
        Assert.Equal("MEI", accountInfo.CompanyType);
    }

    [Fact]
    public async Task ConvertAsync_ShouldReadTheCityState_AsSent()
    {
        // Act
        var city = await ReadAsync<AccountInfoCityDto>("""{ "state": "SC" }""");

        // Assert
        Assert.Equal("SC", city.State);
    }

    [Fact]
    public void ToJson_ShouldSendThePersonAndCompanyTypes_AsGiven()
    {
        // Arrange
        var request = new AccountInfoSaveRequestDto { PersonType = "JURIDICA", CompanyType = "LIMITED" };

        // Act
        var json = ModelConverter.ToJson(request);

        // Assert
        Assert.Contains("\"personType\":\"JURIDICA\"", json);
        Assert.Contains("\"companyType\":\"LIMITED\"", json);
    }

    private static async Task<T> ReadAsync<T>(string json)
    {
        using var response = new HttpResponseMessage { Content = new StringContent(json, Encoding.UTF8, "application/json") };

        return await ModelConverter.ConvertAsync<T>(response);
    }
}
