namespace Featurize.ValueObjects.Measurements;

public readonly partial record struct Unit
{
    /// <summary>
    /// Adds two units of the same type.
    /// </summary>
    public static Unit operator +(Unit a, Unit b)
    {
        if (a.Type != b.Type)
            throw new InvalidOperationException("Cannot add units of different types.");
        return new Unit(a.Name, a._factor, a.Type, a._value + (b._value * b._factor / a._factor));
    }

    /// <summary>
    /// Compares if the unit's value is equal to a specified decimal.
    /// </summary>
    public static bool operator ==(Unit a, decimal b) => a._value == (double)b;

    /// <summary>
    /// Compares if the unit's value is equal to a specified double.
    /// </summary>
    public static bool operator ==(Unit a, double b) => a._value == b;

    public static bool operator !=(Unit a, double b) => !(a == b);
    public static bool operator !=(Unit a, decimal b) => !(a == (double)b);

    public static Unit operator +(Unit a, double scalar) => a.WithValue(a._value + scalar);
    public static Unit operator +(Unit a, decimal scalar) => a + (double)scalar;

    public static Unit operator -(Unit a, Unit b)
    {
        if (a.Type != b.Type)
            throw new InvalidOperationException("Cannot subtract units of different types.");
        return new Unit(a.Name, a._factor, a.Type, a._value - (b._value * b._factor / a._factor));
    }

    public static Unit operator -(Unit a, double scalar) => a.WithValue(a._value - scalar);
    public static Unit operator -(Unit a, decimal scalar) => a - (double)scalar;

    public static Unit operator *(Unit a, double scalar) => new(a.Name, a._factor, a.Type, a._value * scalar);
    public static Unit operator *(Unit a, int scalar) => new(a.Name, a._factor, a.Type, a._value * scalar);
    public static Unit operator /(Unit a, double scalar) => new(a.Name, a._factor, a.Type, a._value / scalar);
    public static Unit operator /(Unit a, int scalar) => new(a.Name, a._factor, a.Type, a._value / scalar);

    public static Unit operator *(int value, Unit unit) => unit.WithValue(unit._value * value);
    public static Unit operator *(decimal value, Unit unit) => unit.WithValue(unit._value * (double)value);
    public static Unit operator *(double value, Unit unit) => unit.WithValue(unit._value * value);

    public static Unit operator +(int value, Unit unit) => unit.WithValue(value);
    public static Unit operator +(decimal value, Unit unit) => unit.WithValue((double)value);
    public static Unit operator +(double value, Unit unit) => unit.WithValue(value);

    public static explicit operator double(Unit u) => u._value;
    public static implicit operator Unit(string s) => Parse(s);
    public static implicit operator string(Unit u) => u.ToString();
}
