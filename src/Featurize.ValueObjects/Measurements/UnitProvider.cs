using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Featurize.ValueObjects.Measurements;


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

public class UnitProvider : IFormatProvider
{
    private readonly CultureInfo provider = CultureInfo.InvariantCulture;

    static UnitProvider()
    {
        Units = typeof(Unit)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.PropertyType == typeof(Unit))
            .Select(f => (Unit)f.GetValue(null)!)
            .ToList();
    }

    private static readonly List<Unit> Units;

    public static UnitProvider Instance { get; } = new UnitProvider();

    /// <summary>
    /// Tries to get a unit by name.
    /// </summary>
    public bool TryGetUnit(string name, out Unit unit)
    {
        if(Units.Select(x => x.Name).Contains(name))
        {
            unit = Units.First(x => x.Name == name);
            return true;
        }
        
        unit = Unit.Unknown;
        return false;
    }

    public object? GetFormat(Type? formatType) => formatType == typeof(UnitProvider) ? this : null;

    internal static UnitProvider GetInstance(IFormatProvider? provider)
    {
        return provider as UnitProvider ?? Instance;
    }

    /// <summary>
    /// Tries to parse a string into a <see cref="Unit"/>.
    /// </summary>
    /// <param name="s">The string to parse (format: "value unit").</param>
    /// <param name="result">The parsed unit, or <see cref="Unit.Unknown"/> if parsing fails.</param>
    /// <returns>True if parsing succeeds, false otherwise.</returns>
    internal bool TryParse(string s, [NotNullWhen(true)] out Unit result)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            result = Unit.Unknown;
            return false;
        }

        // Find the first non-numeric character (unit name starts here)
        int index = s.TakeWhile(c => char.IsDigit(c) || c == '.' || c == '-' || c == ',').Count();

        if (index == 0 || index == s.Length)
        {
            result = Unit.Unknown;
            return false; // No valid number or unit found
        }

        string valuePart = s[..index].Trim();
        string unitPart = s[index..].Trim(); // Extract unit name

        if (!double.TryParse(valuePart, NumberStyles.Any, provider, out double value))
        {
            result = Unit.Unknown;
            return false; // Invalid number format
        }

        if (TryGetUnit(unitPart, out var unit))
        {
            result = unit.WithValue(value);
            return true;
        }

        result = Unit.Unknown;
        return false; // Unrecognized unit
    }


    /// <summary>
    /// Converts a <see cref="Unit"/> to a formatted string.
    /// </summary>
    internal string ToString(Unit unit, UnitFormats? format)
    {
        return format switch
        {
            UnitFormats.Short => $"{unit.Value:N2} {unit.Name}",
            UnitFormats.Long => $"{unit.Value:F4} {unit.Name}",
            _ => $"{unit.Value:N2} {unit.Name}"
        };
    }
}

