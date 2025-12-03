using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

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
    public async Task<FiscalInfoGetResponseDto> GetFiscalInfoAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoGetResponseDto>(request);
    }

    /// <summary>
    /// Get municipality information
    /// </summary>
    public async Task<FiscalInfoMunicipalOptionsGetResponseDto> GetMunicipalityAsync(
        string municipalityCode,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo/municipality")
            .AddQueryParam("municipalityInscription", municipalityCode);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoMunicipalOptionsGetResponseDto>(request);
    }

    /// <summary>
    /// Update fiscal info
    /// </summary>
    public async Task<FiscalInfoGetResponseDto> UpdateFiscalInfoAsync(
        FiscalInfoSaveRequestDto fiscalInfo,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Put, "v3/fiscalInfo")
            .SetBody(fiscalInfo);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoGetResponseDto>(request);
    }
}
