using System.Net;
using System.Text;
using System.Text.Json;
using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Services;
using Asaas.Sdk.Tests.Services;
using Environment = Asaas.Sdk.Http.Environment;

namespace Asaas.Sdk.Tests.Http;

/// <summary>
/// The pieces every request goes through: the environment URLs, the request builder, the default headers, the JSON
/// conversion and the string-or-array converter.
/// </summary>
public sealed class HttpInfrastructureTests
{
    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    [Theory]
    [InlineData(Environment.Default, "https://api.asaas.com/")]
    [InlineData(Environment.Production, "https://api.asaas.com/")]
    [InlineData(Environment.Sandbox, "https://api-sandbox.asaas.com/")]
    [InlineData((Environment)99, "https://api.asaas.com/")]
    public void GetUrl_ShouldGiveTheEnvironmentHost_AndProductionForAnUnknownValue(Environment environment, string url)
    {
        // Act & Assert
        Assert.Equal(url, environment.GetUrl());
    }

    [Fact]
    public async Task Service_ShouldCallProduction_WhenNoBaseUrlIsSet()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new FinanceService(new HttpClient(handler), new AsaasSdkConfig());

        // Act
        await service.GetBalanceAsync(Ct);

        // Assert
        Assert.Equal("https://api.asaas.com/v3/finance/balance", Assert.Single(handler.Requests).AbsoluteUri);
    }

    [Fact]
    public async Task Service_ShouldFollowSetEnvironmentAndSetBaseUrl_ForTheNextRequests()
    {
        // Arrange
        var handler = new CaptureHandler();
        var config = new AsaasSdkConfig();
        var service = new FinanceService(new HttpClient(handler), config);

        // Act
        service.SetEnvironment(Environment.Sandbox);
        await service.GetBalanceAsync(Ct);
        service.SetBaseUrl("https://proxy.example.invalid/asaas/");
        await service.GetBalanceAsync(Ct);

        // Assert: the setting lives in the shared config
        Assert.Equal(
            ["https://api-sandbox.asaas.com/v3/finance/balance", "https://proxy.example.invalid/asaas/v3/finance/balance"],
            handler.Requests.Select(request => request.AbsoluteUri));
        Assert.Equal("https://proxy.example.invalid/asaas/", config.BaseUrl);
    }

    [Fact]
    public void Sdk_ShouldCreateEveryService_WithTheDefaultConfiguration()
    {
        // Act
        using var sdk = new AsaasSdk();

        // Assert
        var services = typeof(AsaasSdk).GetProperties()
            .Where(property => typeof(BaseService).IsAssignableFrom(property.PropertyType))
            .Select(property => property.GetValue(sdk))
            .ToList();
        Assert.NotEmpty(services);
        Assert.All(services, Assert.NotNull);
    }

    [Fact]
    public void Sdk_ShouldWriteItsSettingsToTheConfig()
    {
        // Arrange
        var config = new AsaasSdkConfig();
        using var sdk = new AsaasSdk(config);

        // Act & Assert
        sdk.SetEnvironment(Environment.Sandbox);
        Assert.Equal("https://api-sandbox.asaas.com/", config.BaseUrl);

        sdk.SetBaseUrl("https://proxy.example.invalid/");
        Assert.Equal("https://proxy.example.invalid/", config.BaseUrl);

        sdk.SetApiKey("$aact_key");
        Assert.Equal("$aact_key", config.ApiKeyAuthConfig.ApiKey);

        sdk.SetApiKeyHeader("x-api-key");
        Assert.Equal("x-api-key", config.ApiKeyAuthConfig.ApiKeyHeader);
    }

    [Fact]
    public void Sdk_ShouldRejectNullSettings_AndANullConfig()
    {
        // Arrange
        using var sdk = new AsaasSdk(new AsaasSdkConfig());

        // Act & Assert
        Assert.Equal("config", Assert.Throws<ArgumentNullException>(() => new AsaasSdk(null!)).ParamName);
        Assert.Equal("baseUrl", Assert.Throws<ArgumentNullException>(() => sdk.SetBaseUrl(null!)).ParamName);
        Assert.Equal("apiKey", Assert.Throws<ArgumentNullException>(() => sdk.SetApiKey(null!)).ParamName);
        Assert.Equal("apiKeyHeader", Assert.Throws<ArgumentNullException>(() => sdk.SetApiKeyHeader(null!)).ParamName);
    }

    [Fact]
    public void Sdk_ShouldAllowDisposingTwice()
    {
        // Arrange
        var sdk = new AsaasSdk(new AsaasSdkConfig());

        // Act
        sdk.Dispose();
        var exception = Record.Exception(sdk.Dispose);

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Build_ShouldJoinTheBaseUrlAndPath_WithOneSlash_AndEncodeTheQuery()
    {
        // Act
        using var request = new RequestBuilder(HttpMethod.Get, "/v3/customers")
            .AddQueryParam("name", "Ana & Bia")
            .AddQueryParam("skipped", null)
            .AddQueryParam("limit", 10)
            .Build("https://api.example.invalid/");

        // Assert
        Assert.Equal("https://api.example.invalid/v3/customers?name=Ana+%26+Bia&limit=10", request.RequestUri!.AbsoluteUri);
        Assert.Null(request.Content);
    }

    [Fact]
    public void Build_ShouldAddTheHeaders_KeepingTheLastValueOfARepeatedKey()
    {
        // Act
        using var request = new RequestBuilder(HttpMethod.Get, "v3/payments")
            .AddHeader("X-Trace", "first")
            .AddHeader("X-Trace", "second")
            .AddHeader("X-Other", "value")
            .Build("https://api.example.invalid");

        // Assert
        Assert.Equal(["second"], request.Headers.GetValues("X-Trace"));
        Assert.Equal(["value"], request.Headers.GetValues("X-Other"));
    }

    [Fact]
    public async Task Build_ShouldSendTheLastContentSet()
    {
        // Arrange
        using var multipart = new MultipartFormDataContent();

        // Act
        using var json = new RequestBuilder(HttpMethod.Post, "v3/x").SetMultipartContent(multipart).SetJsonContent(new { Name = "a" }).Build("https://h");
        using var form = new RequestBuilder(HttpMethod.Post, "v3/x").SetBody(new { Name = "a" }).SetMultipartContent(multipart).Build("https://h");

        // Assert
        Assert.Equal("""{"name":"a"}""", await json.Content!.ReadAsStringAsync(Ct));
        Assert.Equal("application/json", json.Content.Headers.ContentType!.MediaType);
        Assert.Same(multipart, form.Content);
    }

    [Fact]
    public async Task DefaultHeaders_ShouldAddTheUserAgentApiKeyAndAccept()
    {
        // Arrange
        var config = new AsaasSdkConfig { ApiKeyAuthConfig = { ApiKey = "$aact_key" } };
        var inner = new HeaderRecorder();
        using var client = new HttpClient(new DefaultHeadersHandler(config) { InnerHandler = inner });

        // Act
        await client.GetAsync("https://api.example.invalid/v3/payments", Ct);

        // Assert
        var request = Assert.Single(inner.Requests);
        Assert.Equal(config.UserAgent, request.Headers.UserAgent.ToString());
        Assert.Equal(["$aact_key"], request.Headers.GetValues("access_token"));
        Assert.Equal("application/json", Assert.Single(request.Headers.Accept).MediaType);
    }

    [Fact]
    public async Task DefaultHeaders_ShouldKeepWhatTheRequestAlreadyHas()
    {
        // Arrange
        var config = new AsaasSdkConfig { ApiKeyAuthConfig = { ApiKey = "$aact_key", ApiKeyHeader = "x-api-key" } };
        var inner = new HeaderRecorder();
        using var client = new HttpClient(new DefaultHeadersHandler(config) { InnerHandler = inner });
        using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.example.invalid/v3/payments")
        {
            Content = new StringContent("a,b", Encoding.UTF8, "text/csv")
        };
        request.Headers.Add("User-Agent", "caller/1.0");
        request.Headers.Add("x-api-key", "caller-key");
        request.Headers.Add("Accept", "text/plain");

        // Act
        await client.SendAsync(request, Ct);

        // Assert
        var sent = Assert.Single(inner.Requests);
        Assert.Equal("caller/1.0", sent.Headers.UserAgent.ToString());
        Assert.Equal(["caller-key"], sent.Headers.GetValues("x-api-key"));
        Assert.False(sent.Headers.Contains("access_token"));
        Assert.Equal("text/plain", Assert.Single(sent.Headers.Accept).MediaType);
        Assert.Equal("text/csv", sent.Content!.Headers.ContentType!.MediaType);
    }

    [Fact]
    public async Task DefaultHeaders_ShouldSendNoApiKey_WhenNoneIsSet_AndMarkUntypedContentAsJson()
    {
        // Arrange
        var inner = new HeaderRecorder();
        using var client = new HttpClient(new DefaultHeadersHandler(new AsaasSdkConfig()) { InnerHandler = inner });

        // Act
        await client.PostAsync("https://api.example.invalid/v3/payments", new ByteArrayContent("{}"u8.ToArray()), Ct);

        // Assert
        var sent = Assert.Single(inner.Requests);
        Assert.False(sent.Headers.Contains("access_token"));
        Assert.Equal("application/json", sent.Content!.Headers.ContentType!.MediaType);
    }

    [Theory]
    [InlineData("")]
    [InlineData("null")]
    public async Task ConvertAsync_ShouldThrow_WhenTheBodyHoldsNoModel(string body)
    {
        // Arrange
        using var response = new HttpResponseMessage { Content = new StringContent(body) };

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => ModelConverter.ConvertAsync<Asaas.Sdk.Models.PaymentGetResponseDto>(response, Ct));

        // Assert
        Assert.Equal(
            body == "" ? "Response content is empty" : "Failed to deserialize response to PaymentGetResponseDto",
            exception.Message);
    }

    [Fact]
    public void ToJson_ShouldWriteCamelCase_WithoutNulls()
    {
        // Act & Assert
        Assert.Equal("""{"customerId":"cus_1","value":1.50}""",
            ModelConverter.ToJson(new { CustomerId = "cus_1", Description = (string?)null, Value = 1.50m }));
    }

    [Fact]
    public void SingleOrArray_ShouldReadNull_WhenCalledOnANullToken()
    {
        // Arrange: the serializer handles a JSON null itself, so only a direct call reaches this branch
        var reader = new Utf8JsonReader("null"u8);
        reader.Read();

        // Act
        var result = new SingleOrArrayStringListConverter().Read(ref reader, typeof(List<string>), new JsonSerializerOptions());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void SingleOrArray_ShouldSkipTheItemsThatAreNotStrings()
    {
        // Arrange
        var options = new JsonSerializerOptions { Converters = { new SingleOrArrayStringListConverter() } };

        // Act
        var result = JsonSerializer.Deserialize<List<string>>("""["A", 1, null, {"b": ["c"]}, [2], "B"]""", options);

        // Assert
        Assert.Equal(["A", "B"], result);
    }

    [Fact]
    public void SingleOrArray_ShouldWriteAnArray_OrNull()
    {
        // Arrange
        var converter = new SingleOrArrayStringListConverter();
        using var stream = new MemoryStream();

        // Act
        using (var writer = new Utf8JsonWriter(stream))
        {
            writer.WriteStartArray();
            converter.Write(writer, ["ASSOCIATION", "DIRECTOR"], new JsonSerializerOptions());
            converter.Write(writer, null, new JsonSerializerOptions());
            converter.Write(writer, [], new JsonSerializerOptions());
            writer.WriteEndArray();
        }

        // Assert
        Assert.Equal("""[["ASSOCIATION","DIRECTOR"],null,[]]""", Encoding.UTF8.GetString(stream.ToArray()));
    }

    private sealed class HeaderRecorder : HttpMessageHandler
    {
        public List<HttpRequestMessage> Requests { get; } = [];

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add(request);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}
