using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;
using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects.Chemistry;

/// <summary>
/// Represents an element from the periodic table.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay}")]
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
public record struct Element : IValueObject<Element>
{
    /// <summary>
    /// Symbol of the element.
    /// </summary>
    public string Symbol { get; private set; }
    /// <summary>
    /// Name of the element.
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Atomic weight of the element.
    /// </summary>
    public decimal AtomicWeight { get; private set; }

    internal Element(string symbol, string name, decimal atomicWeight)
    {
        Symbol = symbol;
        Name = name;
        AtomicWeight = atomicWeight;
    }

    public static Element Unknown => new(ValueObject.UnknownValue, nameof(Unknown), 0);
    public static Element Empty => new(string.Empty, nameof(Empty), 0);

    public override string ToString()
        => ToString(null, null);

    public string ToString(string? format, IFormatProvider? provider)
        => format switch
        {
            "S" => Symbol,
            "N" => Name,
            "W" => AtomicWeight.ToString(provider),
            _ => $"{Symbol} ({Name}) - {AtomicWeight}"
        };

    private string DebuggerDisplay
        => this.DebuggerDisplay(x => $"{x.Symbol}: {x.Name} ({x.AtomicWeight})");

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
        => Parse(s, null);

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Element result)
        => TryParse(s, null, out result);
}