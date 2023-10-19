using Featurize.ValueObjects.Interfaces;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using System.Globalization;

namespace Featurize.ValueObjects;

/// <summary>
/// Serializer for serializing <see cref="IValueObject{T}"/> to Bson.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValueObjectBsonSerializer<T> : SerializerBase<T>, IBsonSerializer
    where T : IValueObject<T>
{

    /// <inheritdoc />
    public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        try
        {
            var value = context.Reader.ReadString()!;
            return T.TryParse(value, CultureInfo.InvariantCulture, out var result) ? result : T.Unknown;
        }
        catch
        {
            return T.Unknown;
        }
    }

    /// <inheritdoc />
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        => context.Writer.WriteString(value.ToString());
}
