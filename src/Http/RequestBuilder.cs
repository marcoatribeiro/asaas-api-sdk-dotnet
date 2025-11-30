using System.Text;
using System.Web;

namespace Asaas.Sdk.Http;

/// <summary>
/// Utility class for building HTTP requests
/// </summary>
public class RequestBuilder
{
    private readonly HttpMethod _method;
    private readonly string _path;
    private readonly Dictionary<string, string> _queryParams = new();
    private readonly Dictionary<string, string> _headers = new();
    private object? _body;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestBuilder"/> class.
    /// </summary>
    /// <param name="method">HTTP method</param>
    /// <param name="path">Request path</param>
    public RequestBuilder(HttpMethod method, string path)
    {
        _method = method;
        _path = path;
    }

    /// <summary>
    /// Add a query parameter
    /// </summary>
    /// <param name="key">Parameter key</param>
    /// <param name="value">Parameter value</param>
    /// <returns>This builder instance</returns>
    public RequestBuilder AddQueryParam(string key, object? value)
    {
        if (value != null)
        {
            _queryParams[key] = value.ToString()!;
        }
        return this;
    }

    /// <summary>
    /// Add a header
    /// </summary>
    /// <param name="key">Header key</param>
    /// <param name="value">Header value</param>
    /// <returns>This builder instance</returns>
    public RequestBuilder AddHeader(string key, string value)
    {
        _headers[key] = value;
        return this;
    }

    /// <summary>
    /// Set the request body
    /// </summary>
    /// <param name="body">Request body object</param>
    /// <returns>This builder instance</returns>
    public RequestBuilder SetBody(object body)
    {
        _body = body;
        return this;
    }

    /// <summary>
    /// Set the request body with JSON content
    /// </summary>
    /// <param name="content">Request body object to be serialized as JSON</param>
    /// <returns>This builder instance</returns>
    public RequestBuilder SetJsonContent(object content)
    {
        _body = content;
        return this;
    }

    /// <summary>
    /// Build the HTTP request message
    /// </summary>
    /// <param name="baseUrl">Base URL for the request</param>
    /// <returns>HTTP request message</returns>
    public HttpRequestMessage Build(string baseUrl)
    {
        var uriBuilder = new StringBuilder(baseUrl.TrimEnd('/'));
        uriBuilder.Append('/');
        uriBuilder.Append(_path.TrimStart('/'));

        // Add query parameters
        if (_queryParams.Any())
        {
            uriBuilder.Append('?');
            var queryString = string.Join("&", 
                _queryParams.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));
            uriBuilder.Append(queryString);
        }

        var request = new HttpRequestMessage(_method, uriBuilder.ToString());

        // Add headers
        foreach (var header in _headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        // Add body
        if (_body != null)
        {
            var json = ModelConverter.ToJson(_body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        return request;
    }
}
