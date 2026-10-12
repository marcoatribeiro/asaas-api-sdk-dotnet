using System.Net;
using Asaas.Sdk.Config;
using Asaas.Sdk.Exceptions;
using Asaas.Sdk.Services;

namespace Asaas.Sdk.Tests.Services;

/// <summary>
/// An Asaas error response reports its errors in an <c>errors[]</c> array; the exception must carry them (issue #9).
/// </summary>
public sealed class ApiErrorTests
{
    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    private static async Task<ApiException> FailAsync(HttpStatusCode status, string body)
    {
        var service = new FinanceService(new HttpClient(new CaptureHandler(status, body)), new AsaasSdkConfig { BaseUrl = "https://api.example.invalid" });
        return await Assert.ThrowsAsync<ApiException>(() => service.GetBalanceAsync(Ct));
    }

    [Fact]
    public async Task DocumentedBody_ShouldGiveAllDescriptionsInOrder_AndStructuredItems()
    {
        // Arrange
        const string body = """{"errors":[{"code":"invalid_value","description":"Value is required."},{"code":"invalid_dueDate","description":"Due date is invalid."}]}""";

        // Act
        var exception = await FailAsync(HttpStatusCode.BadRequest, body);

        // Assert
        Assert.Equal("Value is required.; Due date is invalid.", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Equal(body, exception.ResponseBody);
        Assert.Collection(exception.Errors,
            first => { Assert.Equal("invalid_value", first.Code); Assert.Equal("Value is required.", first.Description); },
            second => { Assert.Equal("invalid_dueDate", second.Code); Assert.Equal("Due date is invalid.", second.Description); });
    }

    [Fact]
    public async Task SingleError_ShouldGiveItsDescription_WithUnicodeIntact()
    {
        // Act
        var exception = await FailAsync(HttpStatusCode.BadRequest,
            """{"errors":[{"code":"invalid_dueDate","description":"Não é permitido data de vencimento inferior a hoje."}]}""");

        // Assert
        Assert.Equal("Não é permitido data de vencimento inferior a hoje.", exception.Message);
        Assert.Equal("invalid_dueDate", Assert.Single(exception.Errors).Code);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("<html>Bad gateway</html>")]
    [InlineData("{ not json")]
    [InlineData("[1, 2]")]
    [InlineData("\"just a string\"")]
    [InlineData("null")]
    [InlineData("{}")]
    [InlineData("""{"errors":[]}""")]
    [InlineData("""{"errors":null}""")]
    [InlineData("""{"errors":"boom"}""")]
    [InlineData("""{"errors":{"code":"x"}}""")]
    [InlineData("""{"errors":[null, 1, "text", [], {}]}""")]
    [InlineData("""{"errors":[{"code":"", "description":"  "}]}""")]
    [InlineData("""{"message":7,"error":{"a":1}}""")]
    public async Task UnusableBody_ShouldKeepTheHttpReasonPhrase_AndNotThrowAParserException(string body)
    {
        // Act
        var exception = await FailAsync(HttpStatusCode.BadRequest, body);

        // Assert
        Assert.Equal("Bad Request", exception.Message);
        Assert.Equal(400, exception.StatusCode);
        Assert.Equal(body, exception.ResponseBody);
        Assert.Empty(exception.Errors);
    }

    [Fact]
    public async Task MalformedItems_ShouldBeSkipped_WithoutHidingTheValidOnes()
    {
        // Act
        var exception = await FailAsync(HttpStatusCode.BadRequest,
            """{"errors":[null, {"code":"a","description":"First."}, 3, {"code":12,"description":["x"]}, {"code":"b"}, {"description":"  "}, {"description":"Last."}]}""");

        // Assert: only items that carry a code or a description are kept; only descriptions go in the message
        Assert.Equal("First.; Last.", exception.Message);
        Assert.Equal(["a", "b", null], exception.Errors.Select(error => error.Code));
        Assert.Equal(["First.", null, "Last."], exception.Errors.Select(error => error.Description));
    }

    [Theory]
    [InlineData("""{"message":"Forbidden by policy"}""", "Forbidden by policy")]
    [InlineData("""{"error":"invalid_token"}""", "invalid_token")]
    [InlineData("""{"message":"","error":"fallback"}""", "fallback")]
    [InlineData("""{"errors":[{"code":"only_code"}],"message":"Top level"}""", "Top level")]
    public async Task TopLevelMessageOrError_ShouldStillBeUsed_WhenThereAreNoDescriptions(string body, string expected)
    {
        // Act
        var exception = await FailAsync(HttpStatusCode.Forbidden, body);

        // Assert
        Assert.Equal(expected, exception.Message);
        Assert.Equal(403, exception.StatusCode);
    }

    [Fact]
    public async Task Descriptions_ShouldWinOverATopLevelMessage()
    {
        // Act
        var exception = await FailAsync(HttpStatusCode.BadRequest,
            """{"message":"Top level","errors":[{"code":"a","description":"Detail."}]}""");

        // Assert
        Assert.Equal("Detail.", exception.Message);
    }

    [Fact]
    public async Task OtherStatusCodes_ShouldKeepTheirReasonPhrase()
    {
        // Act
        var exception = await FailAsync(HttpStatusCode.InternalServerError, "");

        // Assert
        Assert.Equal("Internal Server Error", exception.Message);
        Assert.Equal(500, exception.StatusCode);
    }

    [Fact]
    public void ExistingConstructors_ShouldStillWork_WithNoErrors()
    {
        // Act
        var plain = new ApiException("plain", 400);
        var withBody = new ApiException("body", 404, "{}");
        var withInner = new ApiException("inner", 0, null, new InvalidOperationException("x"));
        var withErrors = new ApiException("errors", 400, "{}", [new Asaas.Sdk.Models.ErrorItemDto { Code = "c", Description = "d" }]);

        // Assert
        Assert.All(new[] { plain, withBody, withInner }, exception => Assert.Empty(exception.Errors));
        Assert.Equal("{}", withBody.ResponseBody);
        Assert.IsType<InvalidOperationException>(withInner.InnerException);
        Assert.Equal("c", Assert.Single(withErrors.Errors).Code);
    }
}
