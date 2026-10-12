using System.Text.Json;
using Asaas.Sdk.Models;

namespace Asaas.Sdk.Exceptions;

/// <summary>
/// Reads the body of an Asaas error response. It never throws: a body that is empty, not JSON or not shaped as expected
/// just yields no message and no items, so the HTTP error is not masked by a parser exception.
/// </summary>
internal static class ApiErrorParser
{
    private const string Separator = "; ";

    /// <summary>
    /// Parse an error body.
    /// </summary>
    /// <param name="content">Response body</param>
    /// <returns>
    /// The documented <c>errors[]</c> items (those with a code or a description, in response order), and the message to
    /// show: their descriptions joined with <c>; </c>, else a top-level <c>message</c> or <c>error</c> string, else null.
    /// </returns>
    public static (string? Message, IReadOnlyList<ErrorItemDto> Errors) Parse(string? content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return (null, []);
        }

        try
        {
            using var document = JsonDocument.Parse(content);
            var root = document.RootElement;

            if (root.ValueKind != JsonValueKind.Object)
            {
                return (null, []);
            }

            var errors = ReadErrors(root);
            var descriptions = errors
                .Select(error => error.Description)
                .Where(description => !string.IsNullOrWhiteSpace(description));
            var message = string.Join(Separator, descriptions);

            if (message.Length == 0)
            {
                message = ReadString(root, "message") ?? ReadString(root, "error") ?? "";
            }

            return (message.Length == 0 ? null : message, errors);
        }
        catch (Exception exception) when (exception is JsonException or ArgumentException or InvalidOperationException)
        {
            return (null, []);
        }
    }

    private static List<ErrorItemDto> ReadErrors(JsonElement root)
    {
        var errors = new List<ErrorItemDto>();

        if (!root.TryGetProperty("errors", out var array) || array.ValueKind != JsonValueKind.Array)
        {
            return errors;
        }

        foreach (var item in array.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Object)
            {
                continue;
            }

            var error = new ErrorItemDto
            {
                Code = ReadString(item, "code"),
                Description = ReadString(item, "description")
            };

            if (!string.IsNullOrWhiteSpace(error.Code) || !string.IsNullOrWhiteSpace(error.Description))
            {
                errors.Add(error);
            }
        }

        return errors;
    }

    private static string? ReadString(JsonElement element, string name) =>
        element.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String && !string.IsNullOrWhiteSpace(value.GetString())
            ? value.GetString()
            : null;
}
