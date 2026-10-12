using System.Text;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Tests.Models;

/// <summary>
/// Reading <c>responsible.type</c> from <c>GET /v3/myAccount/documents</c>. The Asaas reference declares it as an
/// array, but the API answers with a single string, and every read failed while the SDK accepted only the array
/// (issue #4). The answer goes through <see cref="ModelConverter"/>, the same path the services use.
/// </summary>
public sealed class AccountDocumentResponsibleDeserializationTests
{
    [Fact]
    public async Task ConvertAsync_ShouldReadTheTypeAsASingleItemList_WhenTheAsaasSendsAString()
    {
        // Act
        var document = await ReadSingleDocumentAsync("\"ASSOCIATION\"");

        // Assert
        Assert.Equal(["ASSOCIATION"], document.Responsible!.Type);
        AssertTheRestOfTheDocumentWasRead(document);
    }

    [Fact]
    public async Task ConvertAsync_ShouldReadEveryItem_WhenTheAsaasSendsAnArray()
    {
        // Act
        var document = await ReadSingleDocumentAsync("[\"ASSOCIATION\", \"DIRECTOR\"]");

        // Assert
        Assert.Equal(["ASSOCIATION", "DIRECTOR"], document.Responsible!.Type);
        AssertTheRestOfTheDocumentWasRead(document);
    }

    [Fact]
    public async Task ConvertAsync_ShouldReadNull_WhenTheAsaasSendsNull()
    {
        // Act
        var document = await ReadSingleDocumentAsync("null");

        // Assert
        Assert.Null(document.Responsible!.Type);
        AssertTheRestOfTheDocumentWasRead(document);
    }

    [Fact]
    public async Task ConvertAsync_ShouldReadNull_WhenTheAsaasOmitsTheType()
    {
        // Act
        var document = await ReadSingleDocumentAsync(responsibleType: null);

        // Assert
        Assert.Null(document.Responsible!.Type);
        AssertTheRestOfTheDocumentWasRead(document);
    }

    [Theory]
    [InlineData("42")]
    [InlineData("true")]
    [InlineData("{ \"value\": \"ASSOCIATION\" }")]
    public async Task ConvertAsync_ShouldReadNullAndKeepReading_WhenTheAsaasSendsAnUnexpectedShape(string responsibleType)
    {
        // Act
        var document = await ReadSingleDocumentAsync(responsibleType);

        // Assert
        Assert.Null(document.Responsible!.Type);
        AssertTheRestOfTheDocumentWasRead(document);
    }

    private static async Task<AccountDocumentGroupResponseDto> ReadSingleDocumentAsync(string? responsibleType)
    {
        var type = responsibleType is null ? string.Empty : $", \"type\": {responsibleType}";
        var json = $$"""
            {
              "rejectReasons": null,
              "data": [
                {
                  "id": "doc-group-1",
                  "status": "NOT_SENT",
                  "type": "SOCIAL_CONTRACT",
                  "title": "Contrato social",
                  "description": "Envie o contrato social do condomínio.",
                  "responsible": { "name": "Síndico"{{type}} },
                  "onboardingUrl": null,
                  "onboardingUrlExpirationDate": null,
                  "documents": []
                }
              ]
            }
            """;
        using var response = new HttpResponseMessage { Content = new StringContent(json, Encoding.UTF8, "application/json") };

        var result = await ModelConverter.ConvertAsync<AccountDocumentShowResponseDto>(response);

        return Assert.Single(result.Data!);
    }

    private static void AssertTheRestOfTheDocumentWasRead(AccountDocumentGroupResponseDto document)
    {
        Assert.Equal("doc-group-1", document.Id);
        Assert.Equal("NOT_SENT", document.Status);
        Assert.Equal("SOCIAL_CONTRACT", document.Type);
        Assert.Equal("Síndico", document.Responsible!.Name);
        Assert.NotNull(document.Documents);
    }
}
