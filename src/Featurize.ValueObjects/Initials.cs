using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json.Serialization;
using System.ComponentModel;
using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects;

/// <summary>
/// Object that represents an initial.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay}")]
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
public record struct Initials() : IValueObject<Initials>
{
    private string _value = string.Empty;
    private const char _dot = '.';

    /// <summary>
    /// The length of the initails.
    /// </summary>
    public readonly int Length => _value.Count(ch => ch == _dot);

    /// <summary>
    /// Returns a string that represents the <see cref="Initials"/>.
    /// </summary>
    /// <returns>string value of the <see cref="Initials"/></returns>
    public override readonly string ToString() => ToString(null, null);

    /// <summary>
    /// Returns a string that represents the <see cref="Initials"/>.
    /// </summary>
    /// <summary>
    /// Returns a string that represents the <see cref="Initials"/>.
    /// </summary>
    /// <returns>string value of the <see cref="Initials"/>.</returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        return _value;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string DebuggerDisplay =>  this.DebuggerDisplay();

    /// <summary>
    /// An unknown <see cref="Initials"/>.
    /// </summary>
    public static Initials Unknown => new() { _value = ValueObject.UnknownValue };
    /// <summary>
    /// An empty <see cref="Initials"/>.
    /// </summary>
    public static Initials Empty => new();

    /// <summary>
    /// Parse the string representation of initials to its <see cref="Initials"/> equivalent.
    /// </summary>
    /// <param name="s">String value of initials.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about s. If provider is null, the thread current culture is used.</param>
    /// <returns>Returns Initials object.</returns>
    /// <exception cref="FormatException"></exception>
    public static Initials Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var result) ? result : throw new FormatException();
    }

    /// <summary>
    /// Tries to convert the string representation of initials to its <see cref="Initials"/> equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="s">A string representing the initials to convert.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about s. If provider is null, the thread current culture is used.</param>
    /// <param name="result">Returns Initials object.</param>
    /// <returns>true if s was converted successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Initials result)
    {
        result = Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }
        if (s == ValueObject.UnknownValue)
        {
            result = Unknown;
            return true;
        }
        else
        {
            result = new()
            {
                _value = s.Contains(_dot)
                        ? s
                        : string.Join(_dot, s.ToUpper(CultureInfo.InvariantCulture).ToCharArray()) + _dot
            };

            return true;
        }
    }

    /// <summary>
    /// Parse the string representation of initials to its <see cref="Initials"/> equivalent.
    /// </summary>
    /// <param name="s">String value of initials.</param>
    /// <returns>Returns Initials object.</returns>
    /// <exception cref="FormatException"></exception>
    public static Initials Parse(string s)
        => Parse(s, null);

    /// <summary>
    /// Tries to convert the string representation of initials to its <see cref="Initials"/> equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="s">A string representing the initials to convert.</param>
    /// <param name="result">Returns Initials object.</param>
    /// <returns>true if s was converted successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Initials result)
        => TryParse(s, null, out result);

    /// <summary>
    /// Extracts initials from joined names
    /// </summary>
    /// <param name="names">A string representation of names.</param>
    /// <returns>Initials object.</returns>
    public static Initials FromNames(string names)
    {
        return TryParse(string.Join("", names.Split(' ').Select(s => s.First())), out var results) ? results : Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="initials"></param>
    public static implicit operator string(Initials initials) => initials.ToString(null, null);
}
