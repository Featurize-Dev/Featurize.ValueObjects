using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects.Chemistry;

public record struct Element : IValueObject<Element>
{
    public string Symbol { get; private set; }
    public string Name { get; private set; }
    public decimal AtomicWeight { get; private set; }

    internal Element(string symbol, string name, decimal atomicWeight)
    {
        Symbol = symbol;
        Name = name;
        AtomicWeight = atomicWeight;
    }

    public static Element Unknown => new(ValueObject.Unknown, nameof(Unknown), 0);
    public static Element Empty => new(string.Empty, nameof(Empty), 0);

    public static Element Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result)
            ? result
            : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
        [MaybeNullWhen(false)] out Element result)
    {
        result = Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        if (PeriodicTable.TryParse(s, out var element))
        {
            result = element;
        }

        return result != Unknown;
    }

    public static Element Parse(string s)
        => Parse(s, CultureInfo.InvariantCulture);

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Element result)
        => TryParse(s, CultureInfo.InvariantCulture, out result);
}