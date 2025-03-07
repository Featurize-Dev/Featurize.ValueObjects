using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects;

/// <summary>
/// Represents a percentage value.
/// </summary>
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{DebuggerDisplay}")]
public partial record struct Percentage : IValueObject<Percentage>
{
    private decimal? _value;
    private Percentage(decimal? value)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the unknown percentage value.
    /// </summary>
    public static Percentage Unknown => new(null);

    /// <summary>
    /// Gets the empty percentage value.
    /// </summary>
    public static Percentage Empty => new(0);

    /// <summary>
    /// Creates a new percentage value from a decimal.
    /// </summary>
    /// <param name="value">The decimal value.</param>
    /// <returns>A new <see cref="Percentage"/> instance.</returns>
    public static Percentage Create(decimal value)
        => Parse(value.ToString());

    /// <summary>
    /// Returns a string that represents the percentage value.
    /// </summary>
    /// <returns>A string representation of the percentage value.</returns>
    public override readonly string ToString()
        => ToString(null, null);

    /// <summary>
    /// Returns a string that represents the percentage value.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The format provider to use.</param>
    /// <returns>A string representation of the percentage value.</returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (this == Empty)
            return string.Empty;

        if (this == Unknown)
            return ValueObject.UnknownValue;

        return PercentageParser.ToString(_value);
    }

    /// <summary>
    /// Parses a string to a <see cref="Percentage"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <returns>A <see cref="Percentage"/> value.</returns>
    public static Percentage Parse(string s)
         => Parse(s, null);

    /// <summary>
    /// Parses a string to a <see cref="Percentage"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider to use.</param>
    /// <returns>A <see cref="Percentage"/> value.</returns>
    public static Percentage Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : Unknown;

    /// <summary>
    /// Tries to parse a string to a <see cref="Percentage"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="result">The parsed <see cref="Percentage"/> value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Percentage result)
        => TryParse(s, null, out result);

    /// <summary>
    /// Tries to parse a string to a <see cref="Percentage"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider to use.</param>
    /// <param name="result">The parsed <see cref="Percentage"/> value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Percentage result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        if (PercentageParser.TryParse(s, out decimal r))
        {
            result = new Percentage(r);
            return true;
        }

        return false;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string DebuggerDisplay => this.DebuggerDisplay();

    /// <summary>
    /// Implicitly converts a decimal to a <see cref="Percentage"/> value.
    /// </summary>
    /// <param name="val">The decimal value.</param>
    public static implicit operator Percentage(decimal val) => Create(val);

    /// <summary>
    /// Explicitly converts a <see cref="Percentage"/> value to a decimal.
    /// </summary>
    /// <param name="val">The <see cref="Percentage"/> value.</param>
    public static explicit operator decimal(Percentage val) => val._value ?? 0;

    /// <summary>
    /// Explicitly converts a <see cref="Percentage"/> value to a double.
    /// </summary>
    /// <param name="val">The <see cref="Percentage"/> value.</param>
    public static explicit operator double(Percentage val) => (double)(val._value ?? 0);
}

/// <summary>
/// Provides operator overloads for the <see cref="Percentage"/> struct.
/// </summary>
public partial record struct Percentage :
    IIncrementOperators<Percentage>,
    IDecrementOperators<Percentage>,
    IUnaryPlusOperators<Percentage, Percentage>,
    IUnaryNegationOperators<Percentage, Percentage>,
    IAdditionOperators<Percentage, Percentage, Percentage>,
    ISubtractionOperators<Percentage, Percentage, Percentage>,
    IMultiplyOperators<Percentage, Percentage, Percentage>,
    IDivisionOperators<Percentage, Percentage, Percentage>,
    IDivisionOperators<Percentage, decimal, Percentage>,
    IDivisionOperators<Percentage, int, Percentage>
{
    /// <summary>
    /// Increments the <see cref="Percentage"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Percentage"/> value.</param>
    /// <returns>The incremented <see cref="Percentage"/> value.</returns>
    public static Percentage operator ++(Percentage value)
        => new(value._value++);

    /// <summary>
    /// Decrements the <see cref="Percentage"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Percentage"/> value.</param>
    /// <returns>The decremented <see cref="Percentage"/> value.</returns>
    public static Percentage operator --(Percentage value)
        => new(value._value--);

    /// <summary>
    /// Returns the unary plus of the <see cref="Percentage"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Percentage"/> value.</param>
    /// <returns>The unary plus of the <see cref="Percentage"/> value.</returns>
    public static Percentage operator +(Percentage value)
        => new(+value._value);

    /// <summary>
    /// Returns the unary negation of the <see cref="Percentage"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Percentage"/> value.</param>
    /// <returns>The unary negation of the <see cref="Percentage"/> value.</returns>
    public static Percentage operator -(Percentage value)
        => new(-value._value);

    /// <summary>
    /// Adds two <see cref="Percentage"/> values.
    /// </summary>
    /// <param name="left">The left <see cref="Percentage"/> value.</param>
    /// <param name="right">The right <see cref="Percentage"/> value.</param>
    /// <returns>The sum of the two <see cref="Percentage"/> values.</returns>
    public static Percentage operator +(Percentage left, Percentage right)
        => new(left._value + right._value);

    /// <summary>
    /// Subtracts one <see cref="Percentage"/> value from another.
    /// </summary>
    /// <param name="left">The left <see cref="Percentage"/> value.</param>
    /// <param name="right">The right <see cref="Percentage"/> value.</param>
    /// <returns>The difference between the two <see cref="Percentage"/> values.</returns>
    public static Percentage operator -(Percentage left, Percentage right)
        => new(left._value - right._value);

    /// <summary>
    /// Multiplies two <see cref="Percentage"/> values.
    /// </summary>
    /// <param name="left">The left <see cref="Percentage"/> value.</param>
    /// <param name="right">The right <see cref="Percentage"/> value.</param>
    /// <returns>The product of the two <see cref="Percentage"/> values.</returns>
    public static Percentage operator *(Percentage left, Percentage right)
        => new(left._value * right._value);

    /// <summary>
    /// Divides one <see cref="Percentage"/> value by another.
    /// </summary>
    /// <param name="left">The left <see cref="Percentage"/> value.</param>
    /// <param name="right">The right <see cref="Percentage"/> value.</param>
    /// <returns>The quotient of the two <see cref="Percentage"/> values.</returns>
    public static Percentage operator /(Percentage left, Percentage right)
        => new(left._value / right._value);

    /// <summary>
    /// Divides a <see cref="Percentage"/> value by a decimal.
    /// </summary>
    /// <param name="left">The <see cref="Percentage"/> value.</param>
    /// <param name="right">The decimal value.</param>
    /// <returns>The quotient of the <see cref="Percentage"/> value and the decimal value.</returns>
    public static Percentage operator /(Percentage left, decimal right)
        => new((decimal)left / right);

    /// <summary>
    /// Divides a <see cref="Percentage"/> value by an integer.
    /// </summary>
    /// <param name="left">The <see cref="Percentage"/> value.</param>
    /// <param name="right">The integer value.</param>
    /// <returns>The quotient of the <see cref="Percentage"/> value and the integer value.</returns>
    public static Percentage operator /(Percentage left, int right)
        => new((decimal)left / right);
}

/// <summary>
/// Provides parsing and formatting methods for <see cref="Percentage"/> values.
/// </summary>
internal static class PercentageParser
{
    private const int _procent_factor = 100;
    private const int _promile_factor = 1000;

    /// <summary>
    /// Converts a decimal value to a percentage string.
    /// </summary>
    /// <param name="value">The decimal value.</param>
    /// <returns>A percentage string.</returns>
    public static string ToString(decimal? value)
    {
        var culture = new CultureInfo("nl-NL");
        return ((value ?? 0) * _procent_factor).ToString(culture) + "%";
    }

    /// <summary>
    /// Tries to parse a percentage string to a decimal value.
    /// </summary>
    /// <param name="s">The percentage string.</param>
    /// <param name="result">The parsed decimal value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(string s, out decimal result)
    {
        result = 0;
        var value = Normalize(s);

        if (decimal.TryParse(value, out var tmp))
        {
            result = tmp / _procent_factor;

            if (s.Contains('‰'))
            {
                result = tmp / _promile_factor;
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Normalizes a percentage string by removing percentage and permille symbols.
    /// </summary>
    /// <param name="s">The percentage string.</param>
    /// <returns>The normalized string.</returns>
    private static string Normalize(string s)
        => s.Replace("%", string.Empty)
            .Replace("‰", string.Empty);
}
