using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.RealEstate;

/// <summary>
/// Represents an EnergieLabel
/// </summary>
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToDebuggerString()}")]
public sealed record Energielabel
    : IValueObject<Energielabel>
{
    public static Energielabel A4 => new() { _label = "A++++" };
    public static Energielabel A3 => new() { _label = "A+++" };
    public static Energielabel A2 => new() { _label = "A++" };
    public static Energielabel A1 => new() { _label = "A+" };
    public static Energielabel A => new() { _label = "A" };
    public static Energielabel B => new() { _label = "B" };
    public static Energielabel C => new() { _label = "C" };
    public static Energielabel D => new() { _label = "D" };
    public static Energielabel E => new() { _label = "E" };
    public static Energielabel F => new() { _label = "F" };

    private Energielabel() { }

    private string _label = string.Empty;
    private const string _unknownValue = "?";
    
    public static Energielabel Unknown => new() {  _label = _unknownValue };

    public static Energielabel Empty => new() { _label = string.Empty };

    public override string ToString()
        => EnergieLabelFormatter.ToString(_label);

    private string ToDebuggerString()
    {
        return this == Empty ? "{Empty}": ToString();
    }

    public static Energielabel Parse(string s)
        => Parse(s, null);

    public static Energielabel Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Energielabel result)
        => TryParse(s, null, out result);


    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Energielabel result)
    {
        if (string.IsNullOrEmpty(s))
        {
            result = Empty;
            return true;
        }

        if (s == _unknownValue)
        {
            result = Unknown;
            return true;
        }

        if (EnergieLabelFormatter.TryParse(s, out result))
        {
            return true;
        }

        result = Unknown;
        return false;
    }
}


public static class EnergieLabelFormatter
{
    public static string ToString(string value)
    {
        return value;
    }

    public static bool TryParse(string s, out Energielabel result)
    {
        result = Parse(s);
        return true;

    }

    public static Energielabel Parse(string s)
        => s switch
        {
            "A++++" => Energielabel.A4,
            "A+++" => Energielabel.A3,
            "A++" => Energielabel.A2,
            "A+" => Energielabel.A1,
            "A" => Energielabel.A,
            "B" => Energielabel.B,
            "C" => Energielabel.C,
            "D" => Energielabel.D,
            "E" => Energielabel.E,
            "F" => Energielabel.F,
            _ => Energielabel.Unknown,
        };
}

