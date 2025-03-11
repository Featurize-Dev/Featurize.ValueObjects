using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using Featurize.ValueObjects.Measurements.Converters;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Measurements;

/// <summary>
/// Represents a unit of measurement with conversion capabilities.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay}")]
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
public readonly partial record struct Unit : IValueObject<Unit>
{

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => this.DebuggerDisplay(x => $"{x.Value} ({x.Name})");

    private readonly double _value = double.NaN;
    private readonly double _factor = double.NaN;

    /// <summary>
    /// Gets the numerical value of the unit.
    /// </summary>
    internal double Value => _value;

    /// <summary>
    /// Gets the conversion factor of the unit relative to the base unit.
    /// </summary>
    internal double Factor => _factor;

    /// <summary>
    /// Gets the name of the unit.
    /// </summary>
    public string Name { get; } = nameof(UnitType.Unknown);

    /// <summary>
    /// Gets the type of measurement (Length, Weight, Volume, etc.).
    /// </summary>
    public UnitType Type { get; } = UnitType.Unknown;

    /// <summary>
    /// Represents an unknown unit.
    /// </summary>
    public static Unit Unknown => new(nameof(UnitType.Unknown), double.NaN, UnitType.Unknown, double.NaN);

    /// <summary>
    /// Represents an empty unit.
    /// </summary>
    public static Unit Empty => new(nameof(UnitType.Unknown), 0, UnitType.Unknown, 0);

    /// <summary>
    /// Ensures that `default(Unit)` returns `Unit.Unknown`.
    /// </summary>
    public Unit() : this("Unknown", double.NaN, UnitType.Unknown, double.NaN) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Unit"/> struct.
    /// </summary>
    /// <param name="name">The name of the unit.</param>
    /// <param name="factor">The conversion factor relative to the base unit.</param>
    /// <param name="type">The type of the unit.</param>
    /// <param name="value">The numerical value of the unit.</param>
    internal Unit(string name, double factor, UnitType type, double value = 0)
    {
        _factor = factor;
        _value = value;
        Name = name;
        Type = type;
    }

    /// <summary>
    /// Creates a new unit instance with a specified value.
    /// </summary>
    /// <param name="value">The new value.</param>
    /// <returns>A new <see cref="Unit"/> instance with the specified value.</returns>
    public Unit WithValue(double value) => new(Name, _factor, Type, value);

    /// <summary>
    /// Parses a string representation of a unit.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <returns>The parsed unit.</returns>
    public static Unit Parse(string s) => Parse(s, null);

    /// <summary>
    /// Parses a string representation of a unit with an optional format provider.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The parsed unit.</returns>
    public static Unit Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    /// <summary>
    /// Attempts to parse a string representation of a unit.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="result">The parsed unit if successful.</param>
    /// <returns><c>true</c> if parsing was successful; otherwise, <c>false</c>.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Unit result)
        => TryParse(s, null, out result);

    /// <summary>
    /// Attempts to parse a string representation of a unit with an optional format provider.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">The parsed unit if successful.</param>
    /// <returns><c>true</c> if parsing was successful; otherwise, <c>false</c>.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Unit result)
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

        var formatter = UnitProvider.GetInstance(provider);
        return formatter.TryParse(s, out result);
    }

    /// <summary>
    /// Returns a string representation of the unit.
    /// </summary>
    public override string ToString() => ToString(null, null);

    /// <summary>
    /// Converts the unit to a string using a specific format.
    /// </summary>
    /// <param name="format">The format type.</param>
    /// <param name="formatProvider">An optional format provider.</param>
    /// <returns>A formatted string representation of the unit.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
        => format switch
        {
            "S" => ToString(UnitFormats.Short, formatProvider),
            "L" => ToString(UnitFormats.Long, formatProvider),
            _ => ToString(UnitFormats.Short, formatProvider)
        };

    /// <summary>
    /// Converts the unit to a string based on the specified unit format.
    /// </summary>
    public string ToString(UnitFormats format, IFormatProvider? formatProvider)
    {
        var formatter = UnitProvider.GetInstance(formatProvider);
        return formatter.ToString(this, format);
    }


    /// <summary>
    /// Converts the current unit to another unit of the same type.
    /// </summary>
    /// <param name="targetUnit">The target unit to convert to.</param>
    /// <returns>A new <see cref="Unit"/> instance converted to the target unit.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the units are of different types.</exception>
    public Unit ConvertTo(Unit targetUnit)
    {
        if (Type != targetUnit.Type)
            throw new InvalidOperationException($"Cannot convert {Type} to {targetUnit.Type}.");

        var converter = UnitConverter.GetConverter(Type);
        return converter.Convert(this, targetUnit);
    }

    /// <summary>
    /// Checks if the unit is of the same type and name as another unit.
    /// </summary>
    /// <param name="other">The unit to compare againt.</param>
    /// <returns>Returns <c>true</c> if the name and type are the same; otherwise, <c>false</c>.</returns>
    public bool IsOfUnit(Unit other) => 
        Type == other.Type && Name == other.Name;

    /// <summary>
    /// Checks if two units are equal in value after conversion.
    /// </summary>
    /// <param name="other">The unit to compare against.</param>
    /// <returns><c>true</c> if the values are equal after conversion; otherwise, <c>false</c>.</returns>
    public bool EqualsWithConversion(Unit other)
    {
        if (Type != other.Type)
            return false; // Different unit types cannot be compared

        // Convert 'other' to the same unit type as 'this'
        var convertedOther = other.ConvertTo(this);

        // Compare values
        return Math.Abs(Value - convertedOther.Value) < 1e-10; // Small tolerance for floating-point precision
    }


    
}
