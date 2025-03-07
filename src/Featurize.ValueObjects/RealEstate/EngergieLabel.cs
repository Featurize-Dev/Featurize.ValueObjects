using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.RealEstate;

/// <summary>
/// Represents an EnergieLabel.
/// </summary>
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{DebuggerDisplay}")]
public sealed record Energielabel
    : IValueObject<Energielabel>
{
    /// <summary>
    /// Gets the A++++ energy label.
    /// </summary>
    public static Energielabel A4 => new() { _label = "A++++" };

    /// <summary>
    /// Gets the A+++ energy label.
    /// </summary>
    public static Energielabel A3 => new() { _label = "A+++" };

    /// <summary>
    /// Gets the A++ energy label.
    /// </summary>
    public static Energielabel A2 => new() { _label = "A++" };

    /// <summary>
    /// Gets the A+ energy label.
    /// </summary>
    public static Energielabel A1 => new() { _label = "A+" };

    /// <summary>
    /// Gets the A energy label.
    /// </summary>
    public static Energielabel A => new() { _label = "A" };

    /// <summary>
    /// Gets the B energy label.
    /// </summary>
    public static Energielabel B => new() { _label = "B" };

    /// <summary>
    /// Gets the C energy label.
    /// </summary>
    public static Energielabel C => new() { _label = "C" };

    /// <summary>
    /// Gets the D energy label.
    /// </summary>
    public static Energielabel D => new() { _label = "D" };

    /// <summary>
    /// Gets the E energy label.
    /// </summary>
    public static Energielabel E => new() { _label = "E" };

    /// <summary>
    /// Gets the F energy label.
    /// </summary>
    public static Energielabel F => new() { _label = "F" };

    private Energielabel() { }

    private string _label = string.Empty;

    /// <summary>
    /// Gets the unknown energy label.
    /// </summary>
    public static Energielabel Unknown => new() { _label = ValueObject.UnknownValue };

    /// <summary>
    /// Gets the empty energy label.
    /// </summary>
    public static Energielabel Empty => new() { _label = string.Empty };

    /// <summary>
    /// Returns a string that represents the energy label.
    /// </summary>
    /// <returns>A string representation of the energy label.</returns>
    public override string ToString() => ToString(null, null);

    /// <summary>
    /// Returns a string that represents the energy label.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The format provider to use.</param>
    /// <returns>A string representation of the energy label.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
        => EnergieLabelFormatter.ToString(_label);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => this.DebuggerDisplay();

    /// <summary>
    /// Parses a string to an <see cref="Energielabel"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <returns>An <see cref="Energielabel"/> value.</returns>
    public static Energielabel Parse(string s)
        => Parse(s, null);

    /// <summary>
    /// Parses a string to an <see cref="Energielabel"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider to use.</param>
    /// <returns>An <see cref="Energielabel"/> value.</returns>
    public static Energielabel Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    /// <summary>
    /// Tries to parse a string to an <see cref="Energielabel"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="result">The parsed <see cref="Energielabel"/> value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Energielabel result)
        => TryParse(s, null, out result);

    /// <summary>
    /// Tries to parse a string to an <see cref="Energielabel"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider to use.</param>
    /// <param name="result">The parsed <see cref="Energielabel"/> value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
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

        if (EnergieLabelFormatter.TryParse(s, out result))
        {
            return true;
        }

        result = Unknown;
        return false;
    }
}

/// <summary>
/// Provides parsing and formatting methods for <see cref="Energielabel"/> values.
/// </summary>
public static class EnergieLabelFormatter
{
    /// <summary>
    /// Converts a string value to an energy label string.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>An energy label string.</returns>
    public static string ToString(string value)
    {
        return value;
    }

    /// <summary>
    /// Tries to parse a string to an <see cref="Energielabel"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="result">The parsed <see cref="Energielabel"/> value.</param>
    /// <returns>true if the string was parsed successfully; otherwise, false.</returns>
    public static bool TryParse(string s, out Energielabel result)
    {
        result = Parse(s);
        return true;
    }

    /// <summary>
    /// Parses a string to an <see cref="Energielabel"/> value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <returns>An <see cref="Energielabel"/> value.</returns>
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
