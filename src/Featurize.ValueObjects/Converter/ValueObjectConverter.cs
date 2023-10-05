using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects.Converter;


public class ValueObjectConverter<T> : JsonConverter<T>
    where T : IValueObject<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            var value = reader.GetString()!;
            return T.TryParse(value, CultureInfo.InvariantCulture, out var result) ? result : T.Unknown;
        }
        catch
        {
            return T.Unknown;
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ToString(), options);
    }
}
