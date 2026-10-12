using System.Net;
using System.Text;

namespace Asaas.Sdk.Tests.Services;

// Test doubles shared by the service tests: a handler that records the requests (multipart parts included) and a
// stream that records whether it was disposed.

internal sealed class TrackedStream(byte[] bytes) : MemoryStream(bytes)
{
    public bool Disposed { get; private set; }

    protected override void Dispose(bool disposing)
    {
        Disposed = true;
        base.Dispose(disposing);
    }
}

internal sealed record CapturedFile(string FileName, string ContentType, byte[] Bytes);

internal sealed class CapturedRequest
{
    private readonly List<(string Name, string? FileName, string? ContentType, byte[] Bytes)> _parts = [];

    public required HttpMethod Method { get; init; }
    public required string Path { get; init; }
    public string? MediaType { get; init; }
    public string Query { get; init; } = "";
    public string Body { get; init; } = "";
    public IReadOnlyList<string> PartNames => _parts.Select(part => part.Name).ToList();

    public void AddPart(string name, string? fileName, string? contentType, byte[] bytes) =>
        _parts.Add((name, fileName, contentType, bytes));

    public string Text(string name) => Encoding.UTF8.GetString(_parts.Single(part => part.Name == name).Bytes);

    public IReadOnlyList<CapturedFile> Files(string name) => _parts
        .Where(part => part.Name == name)
        .Select(part => new CapturedFile(part.FileName!, part.ContentType!, part.Bytes))
        .ToList();

    public CapturedFile File(string name)
    {
        var part = _parts.Single(part => part.Name == name);
        return new CapturedFile(part.FileName!, part.ContentType!, part.Bytes);
    }
}

internal sealed class CaptureHandler(HttpStatusCode status = HttpStatusCode.OK, string responseBody = "{}") : HttpMessageHandler
{
    public List<CapturedRequest> Requests { get; } = [];

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var body = request.Content is null || request.Content is MultipartFormDataContent
            ? ""
            : await request.Content.ReadAsStringAsync(cancellationToken);

        var captured = new CapturedRequest
        {
            Method = request.Method,
            Path = request.RequestUri!.GetComponents(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.UriEscaped),
            MediaType = request.Content?.Headers.ContentType?.MediaType,
            Query = request.RequestUri.Query,
            Body = body
        };

        if (request.Content is MultipartFormDataContent multipart)
        {
            foreach (var part in multipart)
            {
                captured.AddPart(
                    part.Headers.ContentDisposition!.Name!.Trim('"'),
                    part.Headers.ContentDisposition.FileName?.Trim('"'),
                    part.Headers.ContentType?.MediaType,
                    await part.ReadAsByteArrayAsync(cancellationToken));
            }
        }

        Requests.Add(captured);
        return new HttpResponseMessage(status)
        {
            Content = new StringContent(responseBody, Encoding.UTF8, "application/json")
        };
    }
}
