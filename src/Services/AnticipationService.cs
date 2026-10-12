using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Anticipation service for managing payment anticipations
/// </summary>
public class AnticipationService : BaseService
{
    public AnticipationService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List anticipations
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> ListAnticipationsAsync(
        long? offset = null,
        long? limit = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/anticipations")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Get anticipation by ID
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> GetAnticipationAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/anticipations/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Create an anticipation
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> CreateAnticipationAsync(
        object anticipation,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/anticipations")
            .SetBody(anticipation);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Simulate an anticipation
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> SimulateAnticipationAsync(
        object simulation,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/anticipations/simulate")
            .SetBody(simulation);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Get anticipation configuration
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> GetConfigurationAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/anticipations/configuration");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Update anticipation configuration
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> UpdateConfigurationAsync(
        object configuration,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, "v3/anticipations/configuration")
            .SetBody(configuration);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Get anticipation limits
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> GetLimitsAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/anticipations/limits");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }
}
