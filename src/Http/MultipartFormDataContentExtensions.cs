using System.Globalization;

namespace Asaas.Sdk.Http;

/// <summary>
/// Helpers to add scalar form fields to a <see cref="MultipartFormDataContent"/> the way the Asaas expects them
/// </summary>
internal static class MultipartFormDataContentExtensions
{
    /// <summary>
    /// Add a text field, skipping it when the value is null
    /// </summary>
    public static void AddField(this MultipartFormDataContent content, string name, string? value)
    {
        if (value != null)
        {
            content.Add(new StringContent(value), name);
        }
    }

    /// <summary>
    /// Add a boolean field as lowercase <c>true</c>/<c>false</c>, skipping it when the value is null
    /// </summary>
    public static void AddField(this MultipartFormDataContent content, string name, bool? value)
    {
        if (value.HasValue)
        {
            content.Add(new StringContent(value.Value ? "true" : "false"), name);
        }
    }

    /// <summary>
    /// Add a number field using the invariant culture, skipping it when the value is null
    /// </summary>
    public static void AddField(this MultipartFormDataContent content, string name, long? value)
    {
        if (value.HasValue)
        {
            content.Add(new StringContent(value.Value.ToString(CultureInfo.InvariantCulture)), name);
        }
    }
}
