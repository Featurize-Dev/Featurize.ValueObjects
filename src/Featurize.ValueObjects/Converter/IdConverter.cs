using Featurize.ValueObjects.Identifiers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Converter;



internal class IdConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => Behavior(typeToConvert) is { };


    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    => Behavior(typeToConvert) is { } behavior
        ? (JsonConverter?)Activator.CreateInstance(typeof(ValueObjectConverter<>).MakeGenericType(typeToConvert))
        : null;

    private static Type? Behavior(Type type)
    => type is { IsGenericType: true } && type.GetGenericTypeDefinition() == typeof(Id<>)
    ? type.GetGenericArguments().Single()
    : null;
}
