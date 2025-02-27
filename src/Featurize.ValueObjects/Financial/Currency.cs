using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Financial;
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public record Currency(string Symbol, string Code, string Unit) : IValueObject<Currency>
{
    public static Currency Default { get; set; } = Euro;
    public static Currency Euro => new("€", "EUR", "Euro");
    public static Currency Dollar => new("$", "USD", "United States Dollar");
    public static Currency Unknown => new("?", "Unknown", "Unknown Currency");
    public static Currency Empty => new(string.Empty, string.Empty, string.Empty);

    public static Currency Parse(string s)
        => Parse(s, null);


    public static Currency Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result :
        throw new FormatException($"Unknown currency '{s}'.");

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Currency result)
        => TryParse(s, null, out result);

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

internal static class CurrencyFormatter
{
    public static string FormatCurrency(Currency currency, Amount amount, int decPlaces)
    {
        NumberFormatInfo localFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
        localFormat.CurrencySymbol = currency.Symbol;
        localFormat.CurrencyDecimalDigits = decPlaces;
        return ((decimal)amount).ToString("c", localFormat);
    }
}