using System.Globalization;
using System.Net.Http.Headers;
using Asaas.Sdk.Config;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Services;

/// <summary>
/// Chargeback service for managing chargebacks
/// </summary>
public class ChargebackService : BaseService
{
    public ChargebackService(HttpClient httpClient, AsaasSdkConfig config) 
        : base(httpClient, config)
    {
    }

    /// <summary>
    /// List chargebacks (<c>GET /v3/chargebacks</c>). It only filters by <paramref name="status"/>; the overload that takes
    /// <see cref="ListChargebacksParameters"/> also filters by card brand and dates, and returns a typed response.
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> ListChargebacksAsync(
        long? offset = null,
        long? limit = null,
        string? status = null,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/chargebacks")
            .AddQueryParam("offset", offset)
            .AddQueryParam("limit", limit)
            .AddQueryParam("status", status);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// List chargebacks (<c>GET /v3/chargebacks</c>), with the card brand and date filters the Asaas documents.
    /// </summary>
    /// <param name="parameters">Pagination and filters. Those left null are not sent.</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<ChargebackListResponseDto> ListChargebacksAsync(
        ListChargebacksParameters parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        var requestBuilder = new RequestBuilder(HttpMethod.Get, "v3/chargebacks")
            .AddQueryParam("offset", parameters.Offset)
            .AddQueryParam("limit", parameters.Limit)
            .AddQueryParam("status", parameters.Status)
            .AddQueryParam("creditCardBrand", parameters.CreditCardBrand)
            .AddQueryParam("originDisputeDate[ge]", FormatDate(parameters.OriginDisputeDateGreaterOrEqual))
            .AddQueryParam("originDisputeDate[le]", FormatDate(parameters.OriginDisputeDateLessOrEqual))
            .AddQueryParam("originTransactionDate[ge]", FormatDate(parameters.OriginTransactionDateGreaterOrEqual))
            .AddQueryParam("originTransactionDate[le]", FormatDate(parameters.OriginTransactionDateLessOrEqual));

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<ChargebackListResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get the chargeback of a payment or installment (<c>GET /v3/payments/{id}/chargeback</c>).
    /// </summary>
    /// <param name="paymentOrInstallmentId">
    /// Identifier of the <b>payment</b> or of the <b>installment</b>, not of the chargeback
    /// </param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    public async Task<PaymentChargebackResponseDto> GetPaymentChargebackAsync(
        string paymentOrInstallmentId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(paymentOrInstallmentId);

        var requestBuilder = new RequestBuilder(
            HttpMethod.Get, $"v3/payments/{Uri.EscapeDataString(paymentOrInstallmentId)}/chargeback");

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<PaymentChargebackResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Get chargeback by ID
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    [Obsolete("Calls GET v3/chargebacks/{id}, which the Asaas does not document. Use GetPaymentChargebackAsync, which takes a payment or installment id.")]
    public async Task<dynamic> GetChargebackAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/chargebacks/{id}");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Create a chargeback dispute (<c>POST /v3/chargebacks/{id}/dispute</c>, multipart/form-data).
    /// </summary>
    /// <remarks>
    /// The streams are read once and disposed together with the request, also when it fails or is cancelled. When an
    /// argument is rejected before anything is sent, they are left untouched. The Asaas answers 400 for a file extension
    /// it does not accept. Creating a dispute does not guarantee the chargeback is reversed: follow the payment webhook
    /// events, or <see cref="GetPaymentChargebackAsync"/>.
    /// </remarks>
    /// <param name="chargebackId">Identifier of the chargeback to dispute</param>
    /// <param name="files">Evidence files, from 1 to 11</param>
    /// <param name="cancellationToken">Token that cancels the request</param>
    /// <exception cref="ArgumentException">There is no file, or more than 11</exception>
    public async Task<ChargebackDisputeResponseDto> CreateChargebackDisputeAsync(
        string chargebackId,
        IEnumerable<(Stream Content, string FileName, string ContentType)> files,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chargebackId);
        ArgumentNullException.ThrowIfNull(files);

        var fileList = files.ToList();
        if (fileList.Count is 0 or > MaxDisputeFiles)
        {
            throw new ArgumentException($"A dispute takes from 1 to {MaxDisputeFiles} files.", nameof(files));
        }

        foreach (var (fileContent, fileName, contentType) in fileList)
        {
            ArgumentNullException.ThrowIfNull(fileContent, nameof(files));
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(files));
            ArgumentException.ThrowIfNullOrWhiteSpace(contentType, nameof(files));
        }

        using var content = new MultipartFormDataContent();

        foreach (var (fileContent, fileName, contentType) in fileList)
        {
            var part = new StreamContent(fileContent);
            part.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            content.Add(part, "files", fileName);
        }

        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/chargebacks/{Uri.EscapeDataString(chargebackId)}/dispute")
            .SetMultipartContent(content);

        using var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<ChargebackDisputeResponseDto>(request, cancellationToken);
    }

    /// <summary>
    /// Request chargeback contest
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    [Obsolete("Sends JSON to POST v3/chargebacks/{id}/contest, which the Asaas does not document. Use CreateChargebackDisputeAsync, which sends the evidence files as multipart/form-data to /dispute.")]
    public async Task<dynamic> RequestChargebackContestAsync(
        string id,
        object contest,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/chargebacks/{id}/contest")
            .SetBody(contest);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// List chargeback documents. The route is not in the Asaas reference and has not been confirmed with the Asaas; the
    /// evidence of a dispute goes in <see cref="CreateChargebackDisputeAsync"/>.
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> ListChargebackDocumentsAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Get, $"v3/chargebacks/{id}/documents");
        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    /// <summary>
    /// Upload chargeback document. The route is not in the Asaas reference and has not been confirmed with the Asaas; the
    /// evidence of a dispute goes in <see cref="CreateChargebackDisputeAsync"/>.
    /// </summary>
    /// <returns>
    /// A boxed <see cref="System.Text.Json.JsonElement"/> holding the response body, not an object with one property per
    /// JSON field: <c>result.id</c> throws a <c>RuntimeBinderException</c>. Read it with
    /// <c>((JsonElement)result).GetProperty("id").GetString()</c>.
    /// </returns>
    public async Task<dynamic> UploadChargebackDocumentAsync(
        string id,
        object document,
        CancellationToken cancellationToken = default)
    {
        var requestBuilder = new RequestBuilder(HttpMethod.Post, $"v3/chargebacks/{id}/documents")
            .SetBody(document);

        var request = requestBuilder.Build(GetBaseUrl());
        return await ExecuteAsync<dynamic>(request, cancellationToken);
    }

    private const int MaxDisputeFiles = 11;

    private static string? FormatDate(DateOnly? date) =>
        date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
}
