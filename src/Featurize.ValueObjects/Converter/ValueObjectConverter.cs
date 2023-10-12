using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects.Converter;

/// <summary>
/// A json converter that converts json into a ValueObject
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValueObjectConverter<T> : JsonConverter<T>
    where T : IValueObject<T>
{
    /// <inheritdoc />
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

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ToString(), options);
    }
}
