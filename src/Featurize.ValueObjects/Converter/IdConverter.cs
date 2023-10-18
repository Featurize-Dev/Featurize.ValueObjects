using Featurize.ValueObjects.Identifiers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Converter;


/// <inheritdoc />
public class IdConverter : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
        => Behavior(typeToConvert) is { };

    /// <inheritdoc />
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        => Behavior(typeToConvert) is { }
        ? (JsonConverter?)Activator.CreateInstance(typeof(ValueObjectJsonConverter<>).MakeGenericType(typeToConvert))
        : null;
    
    private static Type? Behavior(Type type)
        => type is { IsGenericType: true } && type.GetGenericTypeDefinition() == typeof(Id<>)
        ? type.GetGenericArguments().Single()
        : null;
}
