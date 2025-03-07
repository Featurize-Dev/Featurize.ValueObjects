using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Metric;

[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public partial record Unit(double Value, string Name, string Symbol, double Factor, Unit? BaseUnit = null) 
    : IValueObject<Unit>
{
    public static Unit Unknown => new(1, nameof(Unknown), ValueObject.UnknownValue, 1);

    public static Unit Empty => new(0, nameof(Empty), string.Empty, 1);

    public static Unit Create(double value, Unit unit)
        => new(value, unit.Name, unit.Symbol, unit.Factor, unit.BaseUnit);

    public Unit ConvertTo(Unit unit)
    {
        var b = ToBase();
        if (b.Name != unit.ToBase().Name)
            throw new InvalidOperationException("Cannot divide units with different base units");
        return new(b.Value / unit.Factor, unit.Name, unit.Symbol, unit.Factor, b with { Value = 1 });
    }

    private Unit ToBase()
        => BaseUnit != null 
        ? new(Value * Factor, BaseUnit.Name, BaseUnit.Symbol, BaseUnit.Factor) 
        : this;

    public override string ToString()
        => ToString(null, null);

    public string ToString(string? format, IFormatProvider? provider)
        => 
        this == Empty? string.Empty : 
        this == Unknown? ValueObject.UnknownValue
        : $"{Value} {Symbol}";

    public static Unit Parse(string s)
        => Parse(s, CultureInfo.InvariantCulture);

    public static Unit Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Unit result)
        => TryParse(s, CultureInfo.InvariantCulture, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Unit result)
    {
        result = Empty;
        if(string.IsNullOrEmpty(s))
            return true;

        if(MetricSystem.TryParse(s, provider, out var unit))
        {
            result = unit;
        }

        return result != Unknown;
    }
}

public partial record Unit :
    IIncrementOperators<Unit>,
    IDecrementOperators<Unit>,
    IUnaryPlusOperators<Unit, Unit>,
    IUnaryNegationOperators<Unit, Unit>,
    IAdditionOperators<Unit, Unit, Unit>,
    ISubtractionOperators<Unit, Unit, Unit>,
    IDivisionOperators<Unit, Unit, Unit>,
    IMultiplyOperators<Unit, Unit, Unit>,
    IMultiplyOperators<Unit, decimal, Unit>,
    IMultiplyOperators<Unit, int, Unit>,
    IMultiplyOperators<Unit, double, Unit>,
    IMultiplyOperators<Unit, Percentage, Unit>,
    IDivisionOperators<Unit, decimal, Unit>,
    IDivisionOperators<Unit, double, Unit>,
    IDivisionOperators<Unit, int, Unit>,
    IDivisionOperators<Unit, Percentage, Unit>
{
    public Unit Increment()
        => new(Value + 1, Name, Symbol, Factor, BaseUnit);
    public Unit Decrement()
        => new(Value - 1, Name, Symbol, Factor, BaseUnit);

    public Unit Plus() 
        => new(+Value, Name, Symbol, Factor, BaseUnit);
    public Unit Negate()
        => new(-Value, Name, Symbol, Factor, BaseUnit);

    public Unit Add(Unit value)
    {
        var b = ToBase();
        var v = value.ToBase();
        if(b.BaseUnit != v.BaseUnit)
            throw new InvalidOperationException("Cannot add units with different base units");
        return new(b.Value + v.Value, b.Name, b.Symbol, b.Factor, b.BaseUnit);
    }

    public Unit Substract(Unit value)
    {
        var b = ToBase();
        var v = value.ToBase();
        if (b.BaseUnit != v.BaseUnit)
            throw new InvalidOperationException("Cannot add units with different base units");
        return new(b.Value - v.Value, b.Name, b.Symbol, b.Factor, b.BaseUnit);
    }

    public Unit Multiply(double value)
        => new(Value * value, Name, Symbol, Factor, BaseUnit);
    public Unit Divide(double value)
        => new(Value / value, Name, Symbol, Factor, BaseUnit);

    public static Unit operator ++(Unit value)
        => value.Increment();
    public static Unit operator --(Unit value)
        => value.Decrement();
    public static Unit operator +(Unit value)
        => value.Plus();
    public static Unit operator -(Unit value)
        => value.Negate();
    public static Unit operator +(Unit left, Unit right)
        => left.Add(right);
    public static Unit operator -(Unit left, Unit right)
        => left.Substract(right);
    public static Unit operator *(Unit left, decimal right)
        => left.Multiply((double)right);
    public static Unit operator *(Unit left, int right)
        => left.Multiply(right);

    public static Unit operator *(Unit left, double right)
        => left.Multiply(right);

    public static Unit operator *(Unit left, Percentage right)
        => left.Multiply((double)right);

    public static Unit operator /(Unit left, decimal right)
        => left.Divide((double)right);
    public static Unit operator /(Unit left, double right)
        => left.Divide(right);

    public static Unit operator /(Unit left, int right)
        => left.Divide(right);
    public static Unit operator /(Unit left, Percentage right)
        => left.Divide((double)right);

    public static Unit operator /(Unit left, Unit right)
    {
        var l = left.ToBase();
        var r = right.ToBase();
        if(l.Name != r.Name)
            throw new InvalidOperationException("Cannot divide units with different base units");
        return new(l.Value / r.Value, l.Name, l.Symbol, l.Factor, l.BaseUnit);
    }

    public static Unit operator *(Unit left, Unit right)
    {
        var l = left.ToBase();
        var r = right.ToBase();
        if (l.Name != r.Name)
            throw new InvalidOperationException("Cannot divide units with different base units");
        return new(l.Value * r.Value, l.Name, l.Symbol, l.Factor, l.BaseUnit);
    }
}