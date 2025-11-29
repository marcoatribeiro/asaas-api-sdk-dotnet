using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Credit card service for managing credit card operations
/// </summary>
public class CreditCardService : BaseService
{
    public CreditCardService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// Tokenize credit card
    /// </summary>
    public async Task<dynamic> TokenizeCreditCardAsync(
        object creditCard,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/creditCard/tokenize")
            .SetBody(creditCard);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get tokenized credit card
    /// </summary>
    public async Task<dynamic> GetTokenizedCreditCardAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/creditCard/{token}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Delete tokenized credit card
    /// </summary>
    public async Task DeleteTokenizedCreditCardAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Delete, $"v3/creditCard/{token}");
        var request = requestBuilder.Build(GetBaseUrl());
        await ExecuteAsync(request);
    }
}
