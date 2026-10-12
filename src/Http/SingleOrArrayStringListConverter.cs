using System.Text.Json;
using System.Text.Json.Serialization;

namespace Asaas.Sdk.Http;

/// <summary>
/// Reads a list of strings that the Asaas sends either as a JSON array or as a single string. Its documentation
/// declares some of these fields as arrays while the API answers with one string, so both shapes are accepted.
/// A value of any other shape is skipped and read as <see langword="null"/>, so that an unexpected answer in one field
/// does not prevent the rest of the response from being read.
/// </summary>
public sealed class SingleOrArrayStringListConverter : JsonConverter<List<string>?>
{
    /// <inheritdoc />
    public override List<string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return [reader.GetString()!];

            case JsonTokenType.StartArray:
                return ReadArray(ref reader);

            case JsonTokenType.Null:
                return null;

            default:
                reader.Skip();
                return null;
        }
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, List<string>? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartArray();

        foreach (var item in value)
        {
            writer.WriteStringValue(item);
        }

        writer.WriteEndArray();
    }

    private static List<string> ReadArray(ref Utf8JsonReader reader)
    {
        var values = new List<string>();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                values.Add(reader.GetString()!);
            }
            else
            {
                reader.Skip();
            }
        }

        return values;
    }
}
