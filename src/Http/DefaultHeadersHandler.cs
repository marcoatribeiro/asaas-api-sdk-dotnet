using Asaas.Sdk.Config;

namespace Asaas.Sdk.Http;

/// <summary>
/// Delegating handler for adding default headers to HTTP requests
/// </summary>
public class DefaultHeadersHandler : DelegatingHandler
{
    private readonly AsaasSdkConfig _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultHeadersHandler"/> class.
    /// </summary>
    /// <param name="config">SDK configuration</param>
    public DefaultHeadersHandler(AsaasSdkConfig config)
    {
        _config = config;
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        // Add user agent
        if (!request.Headers.Contains("User-Agent"))
        {
            request.Headers.Add("User-Agent", _config.UserAgent);
        }

        // Add API key authentication
        if (!string.IsNullOrEmpty(_config.ApiKeyAuthConfig.ApiKey))
        {
            var headerName = _config.ApiKeyAuthConfig.ApiKeyHeader;
            if (!request.Headers.Contains(headerName))
            {
                request.Headers.Add(headerName, $"${_config.ApiKeyAuthConfig.ApiKey}");
            }
        }

        // Add content type for requests with content
        if (request.Content != null && !request.Content.Headers.Contains("Content-Type"))
        {
            request.Content.Headers.Add("Content-Type", "application/json");
        }

        // Add accept header
        if (!request.Headers.Contains("Accept"))
        {
            request.Headers.Add("Accept", "application/json");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
