using Featurize.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Converter;

internal sealed class EncryptedConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
            => InnerValue(typeToConvert) is { };


    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    => InnerValue(typeToConvert) is { }
        ? (JsonConverter?)Activator.CreateInstance(typeof(ValueObjectJsonConverter<>).MakeGenericType(typeToConvert))
        : null;

    private static Type? InnerValue(Type type)
    => type is { IsGenericType: true } && type.GetGenericTypeDefinition() == typeof(Encrypted<>)
    ? type.GetGenericArguments().Single()
    : null;
}
