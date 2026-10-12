using System.Reflection;
using System.Text;
using System.Text.Json;
using Asaas.Sdk.Http;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Tests.Models;

/// <summary>
/// Amounts and rates are <see cref="decimal"/>, like the Payment DTOs, so statement entries add up to the balance exactly
/// (issue #11). They stay plain JSON numbers.
/// </summary>
public sealed class DecimalAmountsTests
{
    [Fact]
    public async Task StatementEntries_ShouldAddUpToTheBalance_Exactly()
    {
        // Arrange: the example of the issue, through the DTOs
        var first = await ReadAsync<FinancialTransactionGetResponseDto>("""{ "value": 0.1 }""");
        var second = await ReadAsync<FinancialTransactionGetResponseDto>("""{ "value": 0.2 }""");
        var balance = await ReadAsync<FinanceBalanceResponseDto>("""{ "balance": 0.3 }""");

        // Act
        var sum = first.Value!.Value + second.Value!.Value;

        // Assert: with double this is 0.30000000000000004 and not equal
        Assert.Equal(0.3m, sum);
        Assert.True(sum == balance.Balance!.Value);
    }

    [Fact]
    public void Amounts_ShouldBeSentAsJsonNumbers_WithTheirPropertyNames()
    {
        // Act
        var transfer = Json(new TransferSaveRequestDto { Value = 10.5m });
        var internalTransfer = Json(new TransferSaveInternalTransferRequestDto { Value = 0.1m });
        var invoice = Json(new InvoiceSaveRequestDto { Value = 1234.56m, Deductions = 0.2m });
        var update = Json(new InvoiceUpdateRequestDto { Value = 300m, Deductions = 10m });
        var account = Json(new AccountSaveRequestDto { IncomeValue = 5000.99m });

        // Assert
        Assert.Equal(JsonValueKind.Number, transfer.GetProperty("value").ValueKind);
        Assert.Equal("10.5", transfer.GetProperty("value").GetRawText());
        Assert.Equal("0.1", internalTransfer.GetProperty("value").GetRawText());
        Assert.Equal("1234.56", invoice.GetProperty("value").GetRawText());
        Assert.Equal("0.2", invoice.GetProperty("deductions").GetRawText());
        Assert.Equal("300", update.GetProperty("value").GetRawText());
        Assert.Equal("5000.99", account.GetProperty("incomeValue").GetRawText());
    }

    [Fact]
    public void TaxRates_ShouldKeepTheirDecimals_AsPercentages()
    {
        // Act
        var taxes = Json(new InvoiceTaxesDto { RetainIss = true, Iss = 2m, Pis = 0.65m, Cofins = 3m, Csll = 9m, Inss = 11m, Ir = 1.5m });

        // Assert: the reference's examples
        Assert.Equal(["retainIss", "cofins", "csll", "inss", "ir", "pis", "iss"], taxes.EnumerateObject().Select(property => property.Name));
        Assert.Equal("0.65", taxes.GetProperty("pis").GetRawText());
        Assert.Equal("1.5", taxes.GetProperty("ir").GetRawText());
        Assert.Equal("2", taxes.GetProperty("iss").GetRawText());
    }

    [Fact]
    public async Task Responses_ShouldReadNumbersAsDecimals_IncludingNullsAndNegatives()
    {
        // Act
        var transfer = await ReadAsync<TransferGetResponseDto>("""{ "value": 100.10, "netValue": 98.1, "transferFee": 2 }""");
        var internalTransfer = await ReadAsync<TransferSaveInternalTransferResponseDto>("""{ "value": 5.5, "netValue": 5.5, "transferFee": 0 }""");
        var transaction = await ReadAsync<FinancialTransactionGetResponseDto>("""{ "value": -1234.56, "balance": 8765.44 }""");
        var statistics = await ReadAsync<FinanceGetPaymentStatisticsResponseDto>("""{ "value": 10.01, "netValue": 9.99 }""");
        var split = await ReadAsync<FinanceGetSplitStatisticsResponseDto>("""{ "income": 1.1, "value": 2.2 }""");
        var invoice = await ReadAsync<InvoiceGetResponseDto>("""{ "value": 300, "deductions": null }""");
        var info = await ReadAsync<AccountInfoGetResponseDto>("""{ "incomeValue": 12000.5 }""");
        var empty = await ReadAsync<FinanceBalanceResponseDto>("""{ "balance": null }""");
        var service = await ReadAsync<MunicipalServiceDto>("""{ "issTax": 0.5 }""");

        // Assert
        Assert.Equal(100.10m, transfer.Value);
        Assert.Equal(98.1m, transfer.NetValue);
        Assert.Equal(2m, transfer.TransferFee);
        Assert.Equal(5.5m, internalTransfer.Value);
        Assert.Equal(0m, internalTransfer.TransferFee);
        Assert.Equal(-1234.56m, transaction.Value);
        Assert.Equal(8765.44m, transaction.Balance);
        Assert.Equal(10.01m - 9.99m, statistics.Value - statistics.NetValue);
        Assert.Equal(3.3m, split.Income + split.Value);
        Assert.Equal(300m, invoice.Value);
        Assert.Null(invoice.Deductions);
        Assert.Equal(12000.5m, info.IncomeValue);
        Assert.Null(empty.Balance);
        Assert.Equal(0.5m, service.IssTax);
    }

    [Theory]
    [InlineData("123456789012.34", "123456789012.34")]
    [InlineData("9999999999999.99", "9999999999999.99")]
    [InlineData("0.01", "0.01")]
    [InlineData("-0.01", "-0.01")]
    [InlineData("1E+3", "1000")]
    [InlineData("1.5e2", "150")]
    [InlineData("10.50", "10.50")]
    public async Task Amounts_ShouldBeReadAndWrittenWithoutLosingACent(string json, string expected)
    {
        // Act
        var read = await ReadAsync<FinanceBalanceResponseDto>($$"""{ "balance": {{json}} }""");
        var written = Json(new TransferSaveRequestDto { Value = read.Balance!.Value }).GetProperty("value").GetRawText();

        // Assert
        Assert.Equal(decimal.Parse(expected, System.Globalization.CultureInfo.InvariantCulture), read.Balance);
        Assert.Equal(decimal.Parse(expected, System.Globalization.CultureInfo.InvariantCulture), decimal.Parse(written, System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public async Task ReadingAnAmount_ShouldNotDependOnTheCurrentCulture()
    {
        // Arrange
        var previous = System.Globalization.CultureInfo.CurrentCulture;

        // Act
        try
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            var balance = await ReadAsync<FinanceBalanceResponseDto>("""{ "balance": 1234.5 }""");
            var json = Json(new TransferSaveRequestDto { Value = 1234.5m });

            // Assert
            Assert.Equal(1234.5m, balance.Balance);
            Assert.Equal("1234.5", json.GetProperty("value").GetRawText());
        }
        finally
        {
            System.Globalization.CultureInfo.CurrentCulture = previous;
        }
    }

    [Fact]
    public void NoModel_ShouldExposeABinaryFloatingPointProperty()
    {
        // Arrange
        var floating = typeof(FinanceBalanceResponseDto).Assembly.GetTypes()
            .Where(type => type.Namespace == "Asaas.Sdk.Models")
            .SelectMany(type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance), (type, property) => (type, property))
            .Where(entry => (Nullable.GetUnderlyingType(entry.property.PropertyType) ?? entry.property.PropertyType) == typeof(double)
                         || (Nullable.GetUnderlyingType(entry.property.PropertyType) ?? entry.property.PropertyType) == typeof(float))
            .Select(entry => $"{entry.type.Name}.{entry.property.Name}")
            .ToList();

        // Assert
        Assert.Empty(floating);
    }

    private static async Task<T> ReadAsync<T>(string json)
    {
        using var response = new HttpResponseMessage { Content = new StringContent(json, Encoding.UTF8, "application/json") };
        return await ModelConverter.ConvertAsync<T>(response, TestContext.Current.CancellationToken);
    }

    private static JsonElement Json(object value) => JsonDocument.Parse(ModelConverter.ToJson(value)).RootElement;
}
