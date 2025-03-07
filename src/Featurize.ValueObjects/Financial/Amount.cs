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
/// A value object that represents a monetary amount, including various methods and operators for operations.
/// </summary>
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public partial record struct Amount : IValueObject<Amount>
{
    private decimal? _value = 0;

    /// <summary>
    /// Creates a new instance of <see cref="Amount"/> with the given value.
    /// </summary>
    /// <param name="value">The decimal value of the amount.</param>
    private Amount(decimal? value)
    {
        _value = value;
    }

    /// <inheritdoc />
    public static Amount Unknown => new(null);

    /// <inheritdoc />
    public static Amount Empty => new(0);

    /// <summary>
    /// Gets a zero amount.
    /// </summary>
    public static Amount Zero => Empty;

    /// <summary>
    /// Gets an amount of one.
    /// </summary>
    public static Amount One => new(1);

    /// <summary>
    /// Gets the maximum possible amount.
    /// </summary>
    public static Amount Max => new(decimal.MaxValue);

    /// <summary>
    /// Gets the minimum possible amount.
    /// </summary>
    public static Amount Min => new(decimal.MinValue);

    /// <summary>
    /// Creates an <see cref="Amount"/> from a decimal value.
    /// </summary>
    /// <param name="value">The decimal amount.</param>
    /// <returns>An <see cref="Amount"/> instance.</returns>
    public static Amount Create(decimal value)
        => new(value);

    /// <summary>
    /// Creates an <see cref="Amount"/> from an integer value.
    /// </summary>
    /// <param name="value">The integer amount.</param>
    /// <returns>An <see cref="Amount"/> instance.</returns>
    public static Amount Create(int value)
        => Create((decimal)value);

    /// <summary>
    /// Creates an <see cref="Amount"/> from a double value.
    /// </summary>
    /// <param name="value">The double amount.</param>
    /// <returns>An <see cref="Amount"/> instance.</returns>
    public static Amount Create(double value)
        => Create((decimal)value);

    /// <inheritdoc />
    public override readonly string ToString()
        => ToString(null, null);

    /// <inheritdoc />
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
        => (_value ?? 0).ToString();

    /// <inheritdoc />
    public static Amount Parse(string s)
        => Parse(s, null);

    /// <inheritdoc />
    public static Amount Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out Amount result) ? result : Unknown;

    /// <inheritdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Amount result)
        => TryParse(s, null, out result);

    /// <inheritdoc />
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

    /// <inheritdoc />
    public static implicit operator Amount(decimal val) => Create(val);

    /// <inheritdoc />
    public static explicit operator decimal(Amount val) => val._value ?? 0;

    /// <inheritdoc />
    public static explicit operator double(Amount val) => (double)(val._value ?? 0);
}

/// <summary>
/// Provides operator overloads for the <see cref="Amount"/> struct.
/// </summary>
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
    /// <inheritdoc />
    public static Amount operator +(Amount value)
        => new(+value._value);

    /// <inheritdoc />
    public static Amount operator -(Amount value)
       => new(-value._value);

    /// <inheritdoc />
    public static Amount operator +(Amount left, Amount right)
        => new(left._value + right._value);

    /// <inheritdoc />
    public static Amount operator -(Amount left, Amount right)
        => new(left._value - right._value);

    /// <inheritdoc />
    public static Amount operator ++(Amount value)
        => new(value._value++);

    /// <inheritdoc />
    public static Amount operator --(Amount value)
        => new(value._value--);

    /// <inheritdoc />
    public static Amount operator *(Amount left, decimal right)
        => new(left._value * right);

    /// <inheritdoc />
    public static Amount operator /(Amount left, decimal right)
        => new(left._value / right);

    /// <inheritdoc />
    public static Amount operator *(Amount left, Percentage right)
        => new(left._value * (decimal)right);

    /// <inheritdoc />
    public static bool operator >(Amount left, Amount right)
        => left._value > right._value;

    /// <inheritdoc />
    public static bool operator >=(Amount left, Amount right)
        => right._value >= left._value;

    /// <inheritdoc />
    public static bool operator <(Amount left, Amount right)
        => left._value < right._value;

    /// <inheritdoc />
    public static bool operator <=(Amount left, Amount right)
        => left._value <= right._value;

    /// <inheritdoc />
    public static Amount operator %(Amount left, decimal right)
        => new(left._value % right);

    /// <inheritdoc />
    public static Amount operator *(Amount left, double right)
        => new(left._value * (decimal)right);

    /// <inheritdoc />
    public static Amount operator /(Amount left, double right)
        => new(left._value / (decimal)right);

    /// <inheritdoc />
    public static Amount operator *(Amount left, int right)
        => new(left._value * right);

    /// <inheritdoc />
    public static Amount operator /(Amount left, int right)
        => new(left._value / right);

    /// <inheritdoc />
    public static Money operator +(Amount left, Currency right)
        => new(right, left);

    /// <inheritdoc />
    public static Amount operator /(Amount left, Percentage right)
        => new(left._value / (decimal)right);
}

/// <summary>
/// LINQ extensions for <see cref="Amount"/> values.
/// </summary>
public static class AmountLinqExtensions
{
    /// <summary>
    /// Sums all values in the source.
    /// </summary>
    /// <param name="source">The source collection.</param>
    /// <returns>The sum of all values.</returns>
    public static Amount Sum(this IEnumerable<Amount> source)
    {
        return source.Aggregate((left, right) => left + right);
    }

    /// <summary>
    /// Returns a summary of different monetary values.
    /// </summary>
    /// <param name="source">The source collection.</param>
    /// <returns>A collection of summarized monetary values.</returns>
    public static IEnumerable<Money> Sum(this IEnumerable<Money> source)
    {
        var currencies = source.GroupBy(x => x.Currency);
        var results = source.GroupBy(x => x.Currency)
            .Select(x => new Money(x.Key, x.Sum(y => y.Amount)));
        return results;
    }

    /// <summary>
    /// Sums all values of a given currency.
    /// </summary>
    /// <param name="source">The source collection.</param>
    /// <param name="currency">The currency to sum.</param>
    /// <returns>The sum of all values in the given currency.</returns>
    public static Money Sum(this IEnumerable<Money> source, Currency currency)
    {
        var results = source.Where(x => x.Currency == currency)
            .Select(x => x.Amount).Sum();
        return new(currency, results);
    }

    /// <summary>
    /// Sums the amount values of a given type.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="selector">The selector function to extract the amount value.</param>
    /// <returns>The sum of the selected amount values.</returns>
    public static Amount Sum<T>(this IEnumerable<T> source, Func<T, Amount> selector)
        => Sum(source.Select(selector));

    /// <summary>
    /// Summarizes the monetary values of a given type.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="selector">The selector function to extract the monetary value.</param>
    /// <returns>A collection of summarized monetary values.</returns>
    public static IEnumerable<Money> Sum<T>(this IEnumerable<T> source, Func<T, Money> selector)
    {
        var results = source.Select(selector).GroupBy(x => x.Currency)
            .Select(x => new Money(x.Key, x.Sum(y => y.Amount)));
        return results;
    }

    /// <summary>
    /// Sums the monetary values of a given type for a specific currency.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="selector">The selector function to extract the monetary value.</param>
    /// <param name="currency">The currency to sum.</param>
    /// <returns>The sum of the selected monetary values for the given currency.</returns>
    public static Money Sum<T>(this IEnumerable<T> source, Func<T, Money> selector, Currency currency)
    {
        var results = source
            .Select(selector)
            .Where(x => x.Currency == currency)
            .Select(x => x.Amount).Sum();

        return new Money(currency, results);
    }
}

