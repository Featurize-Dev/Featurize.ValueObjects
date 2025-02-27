using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Featurize.ValueObjects.Financial;
/// <summary>
/// Een waarde-object dat een geldbedrag (Amount) vertegenwoordigt, inclusief verschillende methoden en operatoren voor bewerkingen.
/// </summary>
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public partial record struct Amount : IValueObject<Amount>
{
    private decimal? _value = 0;

    /// <summary>
    /// Maakt een nieuwe instantie van <see cref="Amount"/> met de gegeven waarde.
    /// </summary>
    /// <param name="value">De decimale waarde van het bedrag.</param>
    private Amount(decimal? value)
    {
        _value = value;
    }

    /// <inhertdoc />
    public static Amount Unknown => new(null);

    /// <inhertdoc />
    public static Amount Empty => new(0);

    /// <summary>
    /// Zerro
    /// </summary>
    public static Amount Zero => Empty;

    public static Amount One => new(1);
    public static Amount Max => new(decimal.MaxValue);
    public static Amount Min => new(decimal.MinValue);

    /// <summary>
    /// Creates an Amount from a decimal value.
    /// </summary>
    /// <param name="value">The decimal amount.</param>
    /// <returns>An amount</returns>
    public static Amount Create(decimal value)
        => new(value);

    /// <summary>
    /// Creates an amount from a integer value.
    /// </summary>
    /// <param name="value">The integer amount.</param>
    /// <returns>An amount</returns>
    public static Amount Create(int value)
        => Create((decimal)value);

    /// <summary>
    /// Creates an amount from a double value.
    /// </summary>
    /// <param name="value">The double amount.</param>
    /// <returns>An amount</returns>
    public static Amount Create(double value)
        => Create((decimal)value);

    /// <inhertdoc />
    public override readonly string ToString()
        => (_value ?? 0).ToString();

    /// <inhertdoc />
    public static Amount Parse(string s)
        => Parse(s, null);

    /// <inhertdoc />
    public static Amount Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out Amount result) ? result : Unknown;

    /// <inhertdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Amount result)
        => TryParse(s, null, out result);

    /// <inhertdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Amount result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        if (decimal.TryParse(s, NumberStyles.Any, provider, out decimal value))
        {
            result = new Amount(value);
            return true;
        }

        result = Unknown;

        return false;
    }

    /// <inhertdoc />
    public static implicit operator Amount(decimal val) => Create(val);
    /// <inhertdoc />
    public static explicit operator decimal(Amount val) => val._value ?? 0;
    /// <inhertdoc />
    public static explicit operator double(Amount val) => (double)(val._value ?? 0);
}

public partial record struct Amount :
    IIncrementOperators<Amount>,
    IDecrementOperators<Amount>,
    IUnaryPlusOperators<Amount, Amount>,
    IUnaryNegationOperators<Amount, Amount>,
    IAdditionOperators<Amount, Amount, Amount>,
    IAdditionOperators<Amount, Currency, Money>,
    ISubtractionOperators<Amount, Amount, Amount>,
    IMultiplyOperators<Amount, decimal, Amount>,
    IMultiplyOperators<Amount, int, Amount>,
    IMultiplyOperators<Amount, double, Amount>,
    IMultiplyOperators<Amount, Percentage, Amount>,
    IDivisionOperators<Amount, decimal, Amount>,
    IDivisionOperators<Amount, double, Amount>,
    IDivisionOperators<Amount, int, Amount>,
    IDivisionOperators<Amount, Percentage, Amount>
{
    /// <inhertdoc />
    public static Amount operator +(Amount value)
        => new(+value._value);

    /// <inhertdoc />
    public static Amount operator -(Amount value)
       => new(-value._value);
    
    /// <inhertdoc />
    public static Amount operator +(Amount left, Amount right)
        => new(left._value + right._value);

    /// <inhertdoc />
    public static Amount operator -(Amount left, Amount right)
        => new(left._value - right._value);

    /// <inhertdoc />
    public static Amount operator ++(Amount value)
        => new(value._value++);

    /// <inhertdoc />
    public static Amount operator --(Amount value)
        => new(value._value--);

    /// <inhertdoc />
    public static Amount operator *(Amount left, decimal right)
        => new(left._value * right);

    /// <inhertdoc />
    public static Amount operator /(Amount left, decimal right)
        => new(left._value / right);

    /// <inhertdoc />
    public static Amount operator *(Amount left, Percentage right)
        => new(left._value * (decimal)right);

    /// <inhertdoc />
    public static bool operator >(Amount left, Amount right)
        => left._value > right._value;

    /// <inhertdoc />
    public static bool operator >=(Amount left, Amount right)
        => right._value >= left._value;

    /// <inhertdoc />
    public static bool operator <(Amount left, Amount right)
        => left._value < right._value;

    /// <inhertdoc />
    public static bool operator <=(Amount left, Amount right)
        => left._value <= right._value;

    /// <inhertdoc />
    public static Amount operator %(Amount left, decimal right)
        => new(left._value % right);

    /// <inhertdoc />
    public static Amount operator *(Amount left, double right)
        => new(left._value * (decimal)right);

    /// <inhertdoc />
    public static Amount operator /(Amount left, double right)
        => new(left._value / (decimal)right);

    /// <inhertdoc />
    public static Amount operator *(Amount left, int right)
        => new(left._value * right);

    /// <inhertdoc />
    public static Amount operator /(Amount left, int right)
        => new(left._value / right);

    /// <inhertdoc />
    public static Money operator +(Amount left, Currency right)
        => new(right, left);

    /// <inhertdoc />
    public static Amount operator /(Amount left, Percentage right)
        => new(left._value / (decimal)right);
}

/// <summary>
/// Linq extensions for Amount values.
/// </summary>
public static class AmountLinqExtensions
{

    /// <summary>
    /// To Sumup all values
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Amount Sum(this IEnumerable<Amount> source)
    {
        return source.Aggregate((left, right) => left + right);
    }

    /// <summary>
    /// Returns a Sumerrize of Different money's
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<Money> Sum(this IEnumerable<Money> source)
    {
        var currencies = source.GroupBy(x => x.Currency);
        var results = source.GroupBy(x => x.Currency)
            .Select(x => new Money(x.Key, x.Sum(y => y.Amount)));
        return results;
    }

    /// <summary>
    /// Sums a all values of a given currency
    /// </summary>
    /// <param name="source"></param>
    /// <param name="currency"></param>
    /// <returns></returns>
    public static Money Sum(this IEnumerable<Money> source, Currency currency)
    {
        var results = source.Where(x => x.Currency == currency)
            .Select(x => x.Amount).Sum();
        return new(currency, results);
    }

    /// <summary>
    /// Sums the amount values of a given type.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <param name="source">List of types</param>
    /// <param name="selector">The selctor for the amount value.</param>
    /// <returns></returns>
    public static Amount Sum<T>(this IEnumerable<T> source, Func<T, Amount> selector)
        => Sum(source.Select(selector));

    /// <summary>
    /// Summerize
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<Money> Sum<T>(this IEnumerable<T> source, Func<T, Money> selector)
    {
        var results = source.Select(selector).GroupBy(x => x.Currency)
            .Select(x => new Money(x.Key, x.Sum(y => y.Amount)));
        return results;
    }

    public static Money Sum<T>(this IEnumerable<T> source, Func<T, Money> selector, Currency currency)
    {
        var results = source
            .Select(selector)
            .Where(x => x.Currency == currency)
            .Select(x => x.Amount).Sum();

        return new Money(currency, results);
    }
}

