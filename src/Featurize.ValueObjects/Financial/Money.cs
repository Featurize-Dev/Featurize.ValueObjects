using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Financial;

/// <summary>
/// Represents a monetary value with a specific currency and amount.
/// </summary>
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public record struct Money(Currency Currency, Amount Amount) : IValueObject<Money>
{
    /// <summary>
    /// Gets the unknown monetary value.
    /// </summary>
    public static Money Unknown => new(Currency.Unknown, Amount.Unknown);

    /// <summary>
    /// Gets the empty monetary value.
    /// </summary>
    public static Money Empty => new(Currency.Empty, Amount.Empty);

    /// <summary>
    /// Returns a string that represents the monetary value.
    /// </summary>
    /// <returns>A string representation of the monetary value.</returns>
    public override readonly string ToString()
        => ToString(null, null);

    /// <summary>
    /// Returns a string that represents the monetary value.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The format provider to use.</param>
    /// <returns>A string representation of the monetary value.</returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
        => CurrencyFormatter.FormatCurrency(Currency, Amount, 2);

    /// <summary>
    /// Parses a string to a <see cref="Money"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <returns>A <see cref="Money"/> value.</returns>
    public static Money Parse(string s)
        => Parse(s, null);

    /// <summary>
    /// Parses a string to a <see cref="Money"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider to use.</param>
    /// <returns>A <see cref="Money"/> value.</returns>
    public static Money Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException($"Unknown money: '{s}'");

    /// <summary>
    /// Tries to parse a string to a <see cref="Money"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="result">The parsed <see cref="Money"/> value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Money result)
        => TryParse(s, null, out result);

    /// <summary>
    /// Tries to parse a string to a <see cref="Money"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider to use.</param>
    /// <param name="result">The parsed <see cref="Money"/> value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Money result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        var parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length > 1)
        {
            var currency = Currency.Parse(parts[0]);
            var amount = Amount.Parse(parts[1]);
            result = new(currency, amount);
            return true;
        }

        result = Unknown;
        return false;
    }
}

