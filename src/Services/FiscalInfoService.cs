using System.Net.Http.Headers;
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
        return await ExecuteAsync<FiscalInfoGetResponseDto>(request, cancellationToken);
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
        return await ExecuteAsync<FiscalInfoMunicipalOptionsGetResponseDto>(request, cancellationToken);
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
        return await ExecuteAsync<FiscalInfoGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// List municipal configurations
    /// </summary>
    public async Task<FiscalInfoMunicipalOptionsGetResponseDto> ListMunicipalConfigurationsAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo/municipalOptions");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoMunicipalOptionsGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve tax information
    /// </summary>
    public async Task<FiscalInfoGetResponseDto> RetrieveTaxInformationAsync(
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/fiscalInfo/");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoGetResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Create and update tax information (<c>POST /v3/fiscalInfo/</c>, multipart/form-data).
    /// </summary>
    /// <remarks>
    /// Every property of <paramref name="fiscalInfo"/> that is not null is sent as a form field; <c>email</c> and
    /// <c>simplesNacional</c> are always sent. <see cref="FiscalInfoSaveRequestDto.CertificateFile"/>, when set, goes as the
    /// binary <c>certificateFile</c> part named <c>certificate.pfx</c>. Booleans are sent as <c>true</c>/<c>false</c> and
    /// numbers use the invariant culture. To send the certificate from a stream, use the overload that takes one.
    /// </remarks>
    public async Task<FiscalInfoGetResponseDto> CreateAndUpdateTaxInformationAsync(
        FiscalInfoSaveRequestDto fiscalInfo,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(fiscalInfo);

        using var content = BuildFiscalInfoContent(fiscalInfo);

        if (fiscalInfo.CertificateFile is { Length: > 0 } certificate)
        {
            var certificateContent = new ByteArrayContent(certificate);
            certificateContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-pkcs12");
            content.Add(certificateContent, "certificateFile", "certificate.pfx");
        }

        return await PostFiscalInfoAsync(content, cancellationToken);
    }

    /// <summary>
    /// Create and update tax information, sending the digital certificate from a stream
    /// (<c>POST /v3/fiscalInfo/</c>, multipart/form-data).
    /// </summary>
    /// <remarks>
    /// <see cref="FiscalInfoSaveRequestDto.CertificateFile"/> is ignored: the certificate comes from
    /// <paramref name="certificateFile"/>. The other properties are sent as described on the overload without a stream.
    /// </remarks>
    /// <param name="fiscalInfo">Fiscal information to save</param>
    /// <param name="certificateFile">
    /// Content of the digital certificate. It is read once, and disposed together with the request, also when the
    /// request fails or is cancelled.
    /// </param>
    /// <param name="fileName">File name sent with the content (e.g. <c>certificate.pfx</c>)</param>
    /// <param name="contentType">Media type of the file (e.g. <c>application/x-pkcs12</c>)</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<FiscalInfoGetResponseDto> CreateAndUpdateTaxInformationAsync(
        FiscalInfoSaveRequestDto fiscalInfo,
        Stream certificateFile,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(fiscalInfo);
        ArgumentNullException.ThrowIfNull(certificateFile);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);

        using var content = BuildFiscalInfoContent(fiscalInfo);

        var certificateContent = new StreamContent(certificateFile);
        certificateContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(certificateContent, "certificateFile", fileName);

        return await PostFiscalInfoAsync(content, cancellationToken);
    }

    private async Task<FiscalInfoGetResponseDto> PostFiscalInfoAsync(
        MultipartFormDataContent content,
        CancellationToken cancellationToken)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, "v3/fiscalInfo/")
            .SetMultipartContent(content);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<FiscalInfoGetResponseDto>(request, cancellationToken);
    }

    private static MultipartFormDataContent BuildFiscalInfoContent(FiscalInfoSaveRequestDto fiscalInfo)
    {
        var content = new MultipartFormDataContent();

        try
        {
            content.AddField("email", fiscalInfo.Email);
            content.AddField("simplesNacional", fiscalInfo.SimplesNacional);
            content.AddField("municipalInscription", fiscalInfo.MunicipalInscription);
            content.AddField("culturalProjectsPromoter", fiscalInfo.CulturalProjectsPromoter);
            content.AddField("cnae", fiscalInfo.Cnae);
            content.AddField("specialTaxRegime", fiscalInfo.SpecialTaxRegime);
            content.AddField("serviceListItem", fiscalInfo.ServiceListItem);
            content.AddField("nbsCode", fiscalInfo.NbsCode);
            content.AddField("rpsSerie", fiscalInfo.RpsSerie);
            content.AddField("rpsNumber", fiscalInfo.RpsNumber);
            content.AddField("loteNumber", fiscalInfo.LoteNumber);
            content.AddField("username", fiscalInfo.Username);
            content.AddField("password", fiscalInfo.Password);
            content.AddField("accessToken", fiscalInfo.AccessToken);
            content.AddField("certificatePassword", fiscalInfo.CertificatePassword);
            content.AddField("nationalPortalTaxCalculationRegime", fiscalInfo.NationalPortalTaxCalculationRegime);
            content.AddField("serviceProvisionCityDefaultType", fiscalInfo.ServiceProvisionCityDefaultType);
        }
        catch
        {
            content.Dispose();
            throw;
        }

        return content;
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
        return await ExecuteAsync<FiscalInfoListMunicipalServicesResponseDto>(request, cancellationToken);
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
        return await ExecuteAsync<FiscalInfoListInvoiceNbsCodesResponseDto>(request, cancellationToken);
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
        return await ExecuteAsync<FiscalInfoUpdateUseNationalPortalResponseDto>(request, cancellationToken);
    }
}
