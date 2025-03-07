using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Financial;

/// <summary>
/// Represents a currency with a symbol, code, and unit.
/// </summary>
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{DebuggerDisplay()}")]
public record Currency(string Symbol, string Code, string Unit) : IValueObject<Currency>
{
    /// <summary>
    /// Gets or sets the default currency.
    /// </summary>
    public static Currency Default { get; set; } = Euro;

    /// <summary>
    /// Gets the Euro currency.
    /// </summary>
    public static Currency Euro => new("€", "EUR", "Euro");

    /// <summary>
    /// Gets the Dollar currency.
    /// </summary>
    public static Currency Dollar => new("$", "USD", "United States Dollar");

    /// <summary>
    /// Gets the unknown currency.
    /// </summary>
    public static Currency Unknown => new(ValueObject.UnknownValue, "Unknown", "Unknown Currency");

    /// <summary>
    /// Gets the empty currency.
    /// </summary>
    public static Currency Empty => new(string.Empty, string.Empty, string.Empty);

    /// <inheritdoc />
    public override string ToString() => ToString(null, null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => format switch
        {
            "C" => Symbol,
            "U" => Unit,
            "S" => Code,
            _ => Symbol
        };

    private string DebuggerDisplay => this.DebuggerDisplay();

    /// <summary>
    /// Parses a string to a <see cref="Currency"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <returns>A <see cref="Currency"/> value.</returns>
    public static Currency Parse(string s)
        => Parse(s, null);

    /// <summary>
    /// Parses a string to a <see cref="Currency"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider to use.</param>
    /// <returns>A <see cref="Currency"/> value.</returns>
    public static Currency Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException($"Unknown currency '{s}'.");

    /// <summary>
    /// Tries to parse a string to a <see cref="Currency"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="result">The parsed <see cref="Currency"/> value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Currency result)
        => TryParse(s, null, out result);

    /// <summary>
    /// Tries to parse a string to a <see cref="Currency"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider to use.</param>
    /// <param name="result">The parsed <see cref="Currency"/> value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Currency result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        var dollar = new[] { "$", "USD", "dols." };
        var euro = new[] { "€", "Euro", "EUR", "EURO" };

        result = s switch
        {
            var str when dollar.Contains(str) => Dollar,
            var str when euro.Contains(str) => Euro,
            _ => Unknown
        };

        return result != Unknown;
    }
}

/// <summary>
/// Provides formatting methods for <see cref="Currency"/> values.
/// </summary>
internal static class CurrencyFormatter
{
    /// <summary>
    /// Formats a currency and amount to a string with the specified decimal places.
    /// </summary>
    /// <param name="currency">The currency to format.</param>
    /// <param name="amount">The amount to format.</param>
    /// <param name="decPlaces">The number of decimal places.</param>
    /// <returns>A formatted currency string.</returns>
    public static string FormatCurrency(Currency currency, Amount amount, int decPlaces)
    {
        NumberFormatInfo localFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
        localFormat.CurrencySymbol = currency.Symbol;
        localFormat.CurrencyDecimalDigits = decPlaces;
        return ((decimal)amount).ToString("c", localFormat);
    }
}

