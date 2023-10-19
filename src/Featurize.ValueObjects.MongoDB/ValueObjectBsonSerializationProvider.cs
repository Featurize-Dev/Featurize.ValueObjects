using Featurize.ValueObjects.Interfaces;
using MongoDB.Bson.Serialization;

namespace Featurize.ValueObjects;

/// <summary>
/// Serialization Provider for serializing <see cref="IValueObject{T}"/> to Bson.
/// </summary>
public sealed class ValueObjectBsonSerializationProvider : IBsonSerializationProvider
{
    /// <inheritdoc />
    public IBsonSerializer GetSerializer(Type type)
    {
        if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValueObject<>)))
        {
            var serializerType = typeof(ValueObjectBsonSerializer<>).MakeGenericType(type);
            return (IBsonSerializer)Activator.CreateInstance(serializerType)!;
        }

        return null!;
    }
}
