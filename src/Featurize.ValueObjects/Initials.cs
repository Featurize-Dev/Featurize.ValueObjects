using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json.Serialization;
using System.ComponentModel;
using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects;

[TypeConverter(typeof(ValueObjectTypeConverter<Initials>))]
[JsonConverter(typeof(ValueObjectConverter<Initials>))]
[DebuggerDisplay("{DebuggerDisplay}")]
public record struct Initials() : IValueObject<Initials>
{
    private string _value = string.Empty;
    private const char Dot = '.';

    public int Length => _value.Count(ch => ch == Dot);

    public override string ToString()
    {
        return _value;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => IsEmpty() ? "{empty}" : ToString();

    public static Initials Unknown => new() { _value = "?" };
    public static Initials Empty => new();
    public bool IsEmpty() => string.IsNullOrEmpty(_value);

    public static Initials Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var result) ? result : throw new FormatException();
    }
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Initials result)
    {
        result = Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }
        else if ("?" == s)
        {
            result = Unknown;
            return true;
        }
        else
        {
            result = new()
            {
                _value = s.Contains(Dot)
                        ? s
                        : string.Join(Dot, s.ToUpper(CultureInfo.InvariantCulture).ToCharArray()) + Dot
            };

            return true;
        }
    }

    public static Initials Parse(string s)
        => Parse(s, CultureInfo.InvariantCulture);

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Initials result)
        => TryParse(s, CultureInfo.InvariantCulture, out result);

    public static Initials FromNames(string names)
    {
        return TryParse(string.Join("", names.Split(' ').Select(s => s.First())), out var results) ? results : Empty;
    }
}
