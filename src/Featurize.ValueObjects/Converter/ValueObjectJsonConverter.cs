using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects.Converter;

/// <summary>
/// A json converter that converts json into a ValueObject
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValueObjectJsonConverter<T> : JsonConverter<T>
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

/// <inheritdoc />
public sealed class ValueObjectJsonConverter : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.GetInterfaces()
            .Where(x=>x.IsGenericType && x.GetGenericTypeDefinition().Equals(typeof(IValueObject<>)))
            .Any();
    }

    /// <inheritdoc />
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(ValueObjectJsonConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}
