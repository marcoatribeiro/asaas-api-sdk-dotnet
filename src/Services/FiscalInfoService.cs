using Asaas.Sdk.Config;
using Asaas.Sdk.Http;

namespace Asaas.Sdk.Services;

/// <summary>
/// Fiscal info service for managing fiscal information
/// </summary>
public class FiscalInfoService : BaseService
{
    public FiscalInfoService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// Get fiscal info
    /// </summary>
    public async Task<dynamic> GetFiscalInfoAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Get municipality information
    /// </summary>
    public async Task<dynamic> GetMunicipalityAsync(
        string municipalityCode,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo/municipality")
            .AddQueryParam("municipalityInscription", municipalityCode);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }

    /// <summary>
    /// Update fiscal info
    /// </summary>
    public async Task<dynamic> UpdateFiscalInfoAsync(
        object fiscalInfo,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, "v3/fiscalInfo")
            .SetBody(fiscalInfo);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request);
    }
}
