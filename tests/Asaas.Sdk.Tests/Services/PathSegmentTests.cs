using System.Reflection;
using Asaas.Sdk.Config;
using Asaas.Sdk.Services;

namespace Asaas.Sdk.Tests.Services;

/// <summary>
/// A caller-supplied identifier must stay one encoded path segment, and null, blank and dot-segment values must be rejected
/// before anything is sent (issue #8). The SDK sends identifiers raw: it encodes them once, so an identifier that already
/// holds <c>%2F</c> reaches the Asaas as the literal text <c>%252F</c>.
/// </summary>
public sealed class PathSegmentTests
{
    private const string BaseUrl = "https://api.example.invalid";

    // Slash, dot segments, query, fragment, an escaped slash, a backslash, a space, a non-ASCII letter and an emoji
    private const string Hostile = "a/../b?c#d%2F\\ é😀";

    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    [Fact]
    public async Task RemoveCustomer_ShouldKeepAHostileIdOnTheCustomerRoute()
    {
        // Arrange: the id from the issue, which used to resolve to /v3/payments/pay_1
        var handler = new CaptureHandler();
        var service = new CustomerService(new HttpClient(handler), Config());

        // Act
        await service.RemoveCustomerAsync("cus_1/../../payments/pay_1", Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Delete, request.Method);
        Assert.Equal("/v3/customers/cus_1%2F..%2F..%2Fpayments%2Fpay_1", request.Path);
        Assert.Equal($"{BaseUrl}/v3/customers/cus_1%2F..%2F..%2Fpayments%2Fpay_1", request.AbsoluteUri);
    }

    [Fact]
    public async Task NormalIds_ShouldKeepTheirRoute()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new CustomerService(new HttpClient(handler), Config());

        // Act
        await service.RemoveCustomerAsync("cus_000005401844", Ct);
        await service.RemoveCustomerAsync("3f1c2d4e-93e2-11ec-b9d9-96f4053b1bd4", Ct);
        await service.RemoveCustomerAsync("a.b-c_d~e", Ct);

        // Assert
        Assert.Equal(
            ["/v3/customers/cus_000005401844", "/v3/customers/3f1c2d4e-93e2-11ec-b9d9-96f4053b1bd4", "/v3/customers/a.b-c_d~e"],
            handler.Requests.Select(request => request.Path));
    }

    [Theory]
    [InlineData("a/b", "a%2Fb")]
    [InlineData("a\\b", "a%5Cb")]
    [InlineData("a?b=c", "a%3Fb%3Dc")]
    [InlineData("a#b", "a%23b")]
    [InlineData("a%b", "a%25b")]
    [InlineData("a%2Fb", "a%252Fb")]
    [InlineData("a%252Fb", "a%25252Fb")]
    [InlineData("a b", "a%20b")]
    [InlineData("é", "%C3%A9")]
    [InlineData("😀", "%F0%9F%98%80")]
    [InlineData("a/../b", "a%2F..%2Fb")]
    [InlineData("...", "...")]
    [InlineData(".a", ".a")]
    public async Task Id_ShouldBeEncodedOnce_AndStayOneSegment(string id, string encoded)
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new CustomerService(new HttpClient(handler), Config());

        // Act
        await service.RemoveCustomerAsync(id, Ct);

        // Assert: one segment, no query, no fragment, and the text is not decoded twice by the URI
        var request = Assert.Single(handler.Requests);
        Assert.Equal($"/v3/customers/{encoded}", request.Path);
        Assert.Equal($"{BaseUrl}/v3/customers/{encoded}", request.AbsoluteUri);
        Assert.Equal("", request.Query);
    }

    [Fact]
    public async Task SeveralIdsInOneRoute_ShouldEachStayOneSegment()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new PaymentDocumentService(new HttpClient(handler), Config());

        // Act
        await service.DeletePaymentDocumentAsync("pay/../1", "doc?2#3", Ct);

        // Assert
        var request = Assert.Single(handler.Requests);
        Assert.Equal("/v3/payments/pay%2F..%2F1/documents/doc%3F2%233", request.Path);
        Assert.Equal("", request.Query);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(".")]
    [InlineData("..")]
    public async Task Id_ShouldBeRejectedBeforeSending_WhenNullBlankOrADotSegment(string? id)
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new CustomerService(new HttpClient(handler), Config());

        // Act
        var exception = await Record.ExceptionAsync(() => service.RemoveCustomerAsync(id!, Ct));

        // Assert: CustomerService already threw ValidationException for a missing id, and still does
        if (string.IsNullOrEmpty(id))
        {
            Assert.IsType<Asaas.Sdk.Exceptions.ValidationException>(exception);
        }
        else
        {
            Assert.Equal("id", Assert.IsAssignableFrom<ArgumentException>(exception).ParamName);
        }

        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task AccountDocumentCalls_ShouldApplyTheSameValidation()
    {
        // Arrange
        var handler = new CaptureHandler();
        var service = new AccountDocumentService(new HttpClient(handler), Config());
        var upload = new TrackedStream([1, 2, 3]);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.GetAccountDocumentAsync("..", Ct));
        await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAccountDocumentAsync(".", Ct));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.UploadAccountDocumentAsync("..", upload, "a.pdf", "application/pdf", cancellationToken: Ct));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.UpdateAccountDocumentAsync(" ", upload, "a.pdf", "application/pdf", Ct));
        Assert.Empty(handler.Requests);
        Assert.False(upload.Disposed); // rejected before the SDK takes the stream over

        await service.GetAccountDocumentAsync("doc/../1", Ct);
        Assert.Equal("/v3/myAccount/documents/files/doc%2F..%2F1", Assert.Single(handler.Requests).Path);
    }

    [Fact]
    public async Task MultipartUploads_ShouldLeaveTheStreamUntouched_WhenTheIdIsRejected()
    {
        // Arrange
        var handler = new CaptureHandler();
        var client = new HttpClient(handler);
        var streams = Enumerable.Range(0, 3).Select(_ => new TrackedStream([1])).ToArray();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => new PaymentDocumentService(client, Config())
            .UploadPaymentDocumentAsync("..", streams[0], "a.pdf", "application/pdf", true, "OTHER", Ct));
        await Assert.ThrowsAsync<ArgumentException>(() => new PaymentLinkService(client, Config())
            .AddPaymentLinkImageAsync(".", streams[1], "a.png", "image/png", cancellationToken: Ct));
        await Assert.ThrowsAsync<ArgumentException>(() => new ChargebackService(client, Config())
            .CreateChargebackDisputeAsync("..", [(streams[2], "a.pdf", "application/pdf")], Ct));
        Assert.Empty(handler.Requests);
        Assert.All(streams, stream => Assert.False(stream.Disposed));
    }

    [Fact]
    public async Task EveryServiceMethod_ShouldKeepAHostileIdOnItsOwnRoute()
    {
        // Arrange
        var failures = new List<string>();

        // Act: call each method with plain ids, then with hostile ones, and compare the two requests
        var methods = await PathMethodsAsync();
        foreach (var (service, method, idParameters) in methods)
        {
            var benign = new CaptureHandler();
            var hostile = new CaptureHandler();
            var benignIds = idParameters.Select((_, index) => $"id{index}x").ToArray();
            var hostileIds = idParameters.Select((_, index) => $"{Hostile}{index}").ToArray();

            await InvokeAsync(service, benign, method, idParameters, benignIds);
            await InvokeAsync(service, hostile, method, idParameters, hostileIds);

            var name = $"{method.DeclaringType!.Name}.{method.Name}";
            if (benign.Requests.Count != 1 || hostile.Requests.Count != 1)
            {
                failures.Add($"{name}: sent {benign.Requests.Count} request(s) with plain ids and {hostile.Requests.Count} with hostile ones");
                continue;
            }

            // The hostile request must be the plain one with each id replaced by its encoded form, nothing else
            var expected = benign.Requests[0].AbsoluteUri;
            for (var index = 0; index < benignIds.Length; index++)
            {
                expected = expected.Replace(benignIds[index], Uri.EscapeDataString(hostileIds[index]));
            }

            if (hostile.Requests[0].AbsoluteUri != expected || hostile.Requests[0].Method != benign.Requests[0].Method)
            {
                failures.Add($"{name}: expected {expected} but sent {hostile.Requests[0].AbsoluteUri}");
            }
        }

        // Assert
        Assert.True(methods.Count > 60, "The reflection scan found too few methods to be trusted.");
        Assert.Empty(failures);
    }

    [Fact]
    public async Task EveryServiceMethod_ShouldRejectABadIdBeforeSending()
    {
        // Arrange
        var failures = new List<string>();
        string?[] badIds = [null, "", "  ", ".", ".."];

        // Act
        foreach (var (service, method, idParameters) in await PathMethodsAsync())
        {
            for (var position = 0; position < idParameters.Count; position++)
            {
                foreach (var badId in badIds)
                {
                    var handler = new CaptureHandler();
                    var ids = idParameters.Select((_, index) => $"id{index}x").ToArray<string?>();
                    ids[position] = badId;

                    var exception = await InvokeAsync(service, handler, method, idParameters, ids);

                    var name = $"{method.DeclaringType!.Name}.{method.Name}({idParameters[position].Name}: {badId ?? "null"})";
                    var missing = string.IsNullOrEmpty(badId);
                    if (missing && exception is Asaas.Sdk.Exceptions.ValidationException)
                    {
                        // Some services already threw ValidationException for a missing id
                    }
                    else if (exception is not ArgumentException { } argument || argument.ParamName != idParameters[position].Name)
                    {
                        failures.Add($"{name}: threw {exception?.GetType().Name ?? "nothing"} ({(exception as ArgumentException)?.ParamName})");
                    }
                    else if (handler.Requests.Count != 0)
                    {
                        failures.Add($"{name}: sent a request");
                    }
                }
            }
        }

        // Assert
        Assert.Empty(failures);
    }

    [Fact]
    public void EveryPathInterpolation_ShouldGoThroughPathSegment()
    {
        // Arrange
        var directory = FindServicesDirectory();
        if (directory is null)
        {
            return; // the sources are not next to the tests (e.g. a packaged test run)
        }

        // Act: find '{...}' holes in a "v3/..." interpolated string that do not call PathSegment.Escape
        var offenders = new List<string>();
        foreach (var file in Directory.GetFiles(directory, "*.cs"))
        {
            foreach (var (line, number) in File.ReadLines(file).Select((line, index) => (line, index + 1)))
            {
                var trimmed = line.TrimStart();
                if (trimmed.StartsWith("//") || trimmed.StartsWith("[") || !line.Contains("$\"v3/"))
                {
                    continue;
                }

                foreach (System.Text.RegularExpressions.Match hole in System.Text.RegularExpressions.Regex.Matches(line, @"\{([^}]*)\}"))
                {
                    if (!hole.Groups[1].Value.StartsWith("PathSegment.Escape(") && !hole.Groups[1].Value.EndsWith("Segment"))
                    {
                        offenders.Add($"{Path.GetFileName(file)}:{number}: {line.Trim()}");
                    }
                }
            }
        }

        // Assert
        Assert.Empty(offenders);
    }

    private static string? FindServicesDirectory()
    {
        for (var directory = new DirectoryInfo(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            var candidate = Path.Combine(directory.FullName, "src", "Services");
            if (Directory.Exists(candidate))
            {
                return candidate;
            }
        }

        return null;
    }

    /// <summary>
    /// Every public async service method with a string parameter that is a path segment (named <c>id</c>, <c>*Id</c>,
    /// <c>token</c> or <c>provider</c>)
    /// </summary>
    private static IEnumerable<(BaseService Service, MethodInfo Method, IReadOnlyList<ParameterInfo> IdParameters)> IdMethods()
    {
        var serviceTypes = typeof(BaseService).Assembly.GetTypes()
            .Where(type => type is { IsPublic: true, IsAbstract: false } && typeof(BaseService).IsAssignableFrom(type));

        foreach (var type in serviceTypes)
        {
            var service = (BaseService)Activator.CreateInstance(type, new HttpClient(new CaptureHandler()), Config())!;

            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (!typeof(Task).IsAssignableFrom(method.ReturnType))
                {
                    continue;
                }

                var idParameters = method.GetParameters().Where(IsPathParameter).ToList();
                if (idParameters.Count > 0)
                {
                    yield return (service, method, idParameters);
                }
            }
        }
    }

    /// <summary>
    /// The methods of <see cref="IdMethods"/>, narrowed to the parameters whose value really lands in the path (some
    /// <c>*Id</c> parameters are query filters)
    /// </summary>
    private static async Task<List<(BaseService Service, MethodInfo Method, IReadOnlyList<ParameterInfo> IdParameters)>> PathMethodsAsync()
    {
        var result = new List<(BaseService, MethodInfo, IReadOnlyList<ParameterInfo>)>();

        foreach (var (service, method, idParameters) in IdMethods())
        {
            var handler = new CaptureHandler();
            var ids = idParameters.Select((_, index) => $"id{index}x").ToArray();
            await InvokeAsync(service, handler, method, idParameters, ids);

            var path = handler.Requests.FirstOrDefault()?.Path ?? "";
            var inPath = idParameters.Where((_, index) => path.Contains(ids[index])).ToList();
            if (inPath.Count > 0)
            {
                result.Add((service, method, inPath));
            }
        }

        return result;
    }

    private static bool IsPathParameter(ParameterInfo parameter) =>
        parameter.ParameterType == typeof(string) &&
        (parameter.Name is "id" or "token" or "provider" || parameter.Name!.EndsWith("Id", StringComparison.Ordinal));

    /// <summary>
    /// Invoke <paramref name="method"/> on a copy of the service that sends through <paramref name="handler"/>, with the given
    /// path values and harmless values elsewhere. Returns what it threw, if anything.
    /// </summary>
    private static async Task<Exception?> InvokeAsync(
        BaseService original,
        CaptureHandler handler,
        MethodInfo method,
        IReadOnlyList<ParameterInfo> idParameters,
        IReadOnlyList<string?> ids)
    {
        var service = Activator.CreateInstance(original.GetType(), new HttpClient(handler), Config())!;
        var arguments = method.GetParameters().Select(parameter =>
        {
            var position = idParameters.ToList().FindIndex(id => id.Position == parameter.Position);
            return position >= 0 ? ids[position] : HarmlessValue(parameter);
        }).ToArray();

        try
        {
            await (Task)method.Invoke(service, arguments)!;
            return null;
        }
        catch (TargetInvocationException exception)
        {
            return exception.InnerException;
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    private static object? HarmlessValue(ParameterInfo parameter)
    {
        var type = parameter.ParameterType;

        return parameter.Name switch
        {
            "contentType" => "application/octet-stream",
            "fileName" => "file.bin",
            _ when parameter.HasDefaultValue => parameter.DefaultValue is DBNull ? null : parameter.DefaultValue,
            _ when type == typeof(string) => "x",
            _ when type == typeof(CancellationToken) => CancellationToken.None,
            _ when type == typeof(Stream) => new MemoryStream([1, 2, 3]),
            _ when type == typeof(IEnumerable<(Stream Content, string FileName, string ContentType)>) =>
                new List<(Stream, string, string)> { (new MemoryStream([1]), "a.pdf", "application/pdf") },
            _ when type == typeof(object) => new object(),
            _ when type.IsValueType => Activator.CreateInstance(Nullable.GetUnderlyingType(type) is null ? type : null!),
            _ when type.GetConstructor(Type.EmptyTypes) is not null => Activator.CreateInstance(type),
            _ => null
        };
    }

    private static AsaasSdkConfig Config() => new() { BaseUrl = BaseUrl };
}
