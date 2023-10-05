using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Identifiers;

[DebuggerDisplay("{DebuggerDisplay}")]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[JsonConverter(typeof(IdConverter))]
public record Id<TBehavior> : IValueObject<Id<TBehavior>>
    where TBehavior : IdBehaviour, new()
{
    private object? _value = null;
    private static IdBehaviour _behaviour = new TBehavior();
    public static Id<TBehavior> Unknown => new() { _value = "?" };

    public static Id<TBehavior> Empty => new();

    public static Id<TBehavior> Next() => new() { _value = _behaviour.Next() };

    public static Id<TBehavior> Create(object id)
    {
        if (_behaviour.Supports(id))
        {
            return new() { _value = id };
        }
        throw new NotSupportedException();
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => IsEmpty() ? "{empty}" : ToString();
    public override string ToString()
    {
        if (this == Unknown) return "?";
        if (this == Empty) return string.Empty;
        return _behaviour.ToString(_value!);
    }

    public static Id<TBehavior> Parse(string s) => Parse(s, CultureInfo.InvariantCulture);

    public static Id<TBehavior> Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var id) ? id : throw new FormatException();
    }
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Id<TBehavior> result) =>
        TryParse(s, CultureInfo.InvariantCulture, out result);
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Id<TBehavior> result)
    {
        result = Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }
        else if (_behaviour.TryParse(s, out var id))
        {

            result = new() { _value = id };

            return true;
        }
        else
        {
            result = Unknown;
            return false;
        }
    }

    public bool IsEmpty()
    {
        return this == Empty;
    }
}
