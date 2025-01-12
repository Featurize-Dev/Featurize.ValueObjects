using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Sustainablility;
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public sealed record Energielabel
    : IValueObject<Energielabel>
{
    private string _label;
    private Energielabel(string label)
    {
        _label = label;
    }

    public static Energielabel A4 => new("A++++");
    public static Energielabel A3 => new("A+++");
    public static Energielabel A2 => new("A++");
    public static Energielabel A1 => new("A+");
    public static Energielabel A => new("A");
    public static Energielabel B => new("B");
    public static Energielabel C => new("C");
    public static Energielabel D => new("D");
    public static Energielabel E => new("E");
    public static Energielabel F => new("F");

    public static Energielabel Unknown => new(ValueObject.UnknownValue);

    public static Energielabel Empty => new(string.Empty);

    public override string ToString()
        => EnergieLabelFormatter.ToString(_label);

    public static Energielabel Parse(string s)
        => Parse(s, CultureInfo.InvariantCulture);

    public static Energielabel Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Energielabel result)
        => TryParse(s, CultureInfo.InvariantCulture, out result);


    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Energielabel result)
    {
        if (string.IsNullOrEmpty(s))
        {
            result = Empty;
            return true;
        }

        if (s == ValueObject.UnknownValue)
        {
            result = Unknown;
            return true;
        }

        if(EnergieLabelFormatter.TryParse(s, out result))
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
        return result != Energielabel.Unknown;
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
