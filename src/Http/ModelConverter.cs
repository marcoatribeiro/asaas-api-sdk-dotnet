using System.Text.Json;

namespace Asaas.Sdk.Http;

/// <summary>
/// Utility class for converting HTTP responses to model objects
/// </summary>
public static class ModelConverter
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Convert HTTP response to model object
    /// </summary>
    /// <typeparam name="T">Type of model to convert to</typeparam>
    /// <param name="response">HTTP response message</param>
    /// <returns>Deserialized model object</returns>
    public static async Task<T> ConvertAsync<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        
        if (string.IsNullOrEmpty(content))
        {
            throw new InvalidOperationException("Response content is empty");
        }

        var result = JsonSerializer.Deserialize<T>(content, JsonOptions);
        
        if (result == null)
        {
            throw new InvalidOperationException($"Failed to deserialize response to {typeof(T).Name}");
        }

        return result;
    }

    /// <summary>
    /// Convert object to JSON string
    /// </summary>
    /// <param name="obj">Object to serialize</param>
    /// <returns>JSON string</returns>
    public static string ToJson(object obj)
    {
        return JsonSerializer.Serialize(obj, JsonOptions);
    }
}
