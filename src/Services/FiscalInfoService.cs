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

    /// <summary>
    /// List municipal configurations
    /// </summary>
    public async Task<FiscalInfoMunicipalOptionsGetResponseDto> ListMunicipalConfigurationsAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo/municipalOptions");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoMunicipalOptionsGetResponseDto>(request);
    }

    /// <summary>
    /// Retrieve tax information
    /// </summary>
    public async Task<FiscalInfoGetResponseDto> RetrieveTaxInformationAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo/");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoGetResponseDto>(request);
    }

    /// <summary>
    /// Create and update tax information
    /// </summary>
    public async Task<FiscalInfoGetResponseDto> CreateAndUpdateTaxInformationAsync(
        FiscalInfoSaveRequestDto fiscalInfo,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/fiscalInfo/")
            .SetBody(fiscalInfo);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoGetResponseDto>(request);
    }

    /// <summary>
    /// List municipal services
    /// </summary>
    public async Task<FiscalInfoListMunicipalServicesResponseDto> ListMunicipalServicesAsync(
        ListMunicipalServicesParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo/services");

        if (parameters != null)
        {
            requestBuilder
                .AddQueryParam("offset", parameters.Offset)
                .AddQueryParam("limit", parameters.Limit)
                .AddQueryParam("description", parameters.Description);
        }

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoListMunicipalServicesResponseDto>(request);
    }

    /// <summary>
    /// List NBS codes
    /// </summary>
    public async Task<FiscalInfoListInvoiceNbsCodesResponseDto> ListNbsCodesAsync(
        ListNbsCodesParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo/nbsCodes");

        if (parameters != null)
        {
            requestBuilder
                .AddQueryParam("offset", parameters.Offset)
                .AddQueryParam("limit", parameters.Limit)
                .AddQueryParam("codeDescription", parameters.CodeDescription);
        }

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoListInvoiceNbsCodesResponseDto>(request);
    }

    /// <summary>
    /// Configure invoice issuing portal
    /// </summary>
    public async Task<FiscalInfoUpdateUseNationalPortalResponseDto> ConfigureInvoiceIssuingPortalAsync(
        FiscalInfoUpdateUseNationalPortalRequestDto? requestDto = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/fiscalInfo/nationalPortal")
            .SetJsonContent(requestDto ?? new FiscalInfoUpdateUseNationalPortalRequestDto());

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoUpdateUseNationalPortalResponseDto>(request);
    }
}
