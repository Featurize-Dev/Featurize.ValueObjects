using Featurize.ValueObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Featurize.ValueObjects;
public record struct RomanNumeral() : IValueObject<RomanNumeral>
{
    private const string _unknownvalue = "?";
    private int? _value = 0;

    private static Dictionary<char, int> _values = new()
    {
        { 'I', 1 },
        { 'V', 5 },
        { 'X', 10 },
        { 'L', 50 },
        { 'C', 100 },
        { 'D', 500 },
        { 'M', 1000 },
    };

    public static RomanNumeral Unknown => new() {  _value = null };

    public static RomanNumeral Empty => new();

    public override string ToString()
    {
        StringBuilder result = new();
        var num = _value ?? 0;

        if (num >= 1000)
        {
            result.Append(new string('M', num / 1000));
            num %= 1000;
        }
        if (num >= 100)
        {
            result.Append(num / 900 >= 1 ? "CM" :
                            num / 500 >= 1 ? $"D{new string('C', num / 100 - 5)}" :
                            num / 400 >= 1 ? "CD" : new string('C', num / 100));
            num %= 100;
        }

        if (num >= 10)
        {
            result.Append(num / 90 >= 1 ? "XC" :
                            num / 50 >= 1 ? $"L{new string('X', num / 10 - 5)}" :
                            num / 40 >= 1 ? "XL" : new string('X', num / 10));
            num %= 10;
        }

        result.Append(num / 9 >= 1 ? "IX" :
                        num / 5 >= 1 ? $"V{new string('I', num - 5)}" :
                        num / 4 >= 1 ? "IV" : new string('I', num));

        return result.ToString();
    }

    public static RomanNumeral Parse(string s)
        => Parse(s, null);

    public static RomanNumeral Parse(string s, IFormatProvider? provider)
        => TryParse(s, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out RomanNumeral result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out RomanNumeral result)
    {
        if (string.IsNullOrEmpty(s))
        {
            result = Empty;
            return true;
        }

        if(s == _unknownvalue)
        {
            result = Unknown;
            return true;
        }

        var total = 0;
        for (var i = 0; i < s.Length; i++)
        {
            if (i < s.Length - 1 && _values[s[i]] < _values[s[i + 1]])
            {
                total -= _values[s[i]];
            }
            else
            {
                total += _values[s[i]];
            }
        }

        result = new() { _value = total };
        return true;
    }

    public bool IsEmpty() 
        => this == Empty;

    public static bool operator ==(RomanNumeral a, int b)
    {
        return a._value == b;
    }

    public static bool operator !=(RomanNumeral a, int b)
    {
        return !(a == b);
    }

    public static RomanNumeral operator +(RomanNumeral a, RomanNumeral b)
        => new() { _value = a._value + b._value };
    public static RomanNumeral operator +(RomanNumeral a, int b)
        => new() { _value = a._value + b };

    public static RomanNumeral operator -(RomanNumeral a, RomanNumeral b)
        => new() { _value = a._value - b._value };
    public static RomanNumeral operator -(RomanNumeral a, int b)
        => new() { _value = a._value - b };
    public static RomanNumeral operator /(RomanNumeral a, RomanNumeral b)
            => new() { _value = a._value / b._value };
    public static RomanNumeral operator /(RomanNumeral a, int b)
        => new() { _value = a._value / b };
    public static RomanNumeral operator *(RomanNumeral a, RomanNumeral b)
            => new() { _value = a._value * b._value };
    public static RomanNumeral operator *(RomanNumeral a, int b)
        => new() { _value = a._value * b };

   
    public static RomanNumeral operator ++(RomanNumeral a)
        => new() {  _value = a._value++ };

    public static RomanNumeral operator --(RomanNumeral a)
        => new() { _value = a._value-- };

    public static implicit operator int(RomanNumeral a)
        => a._value ?? 0;

    public static explicit operator RomanNumeral(int a)
        => new () { _value = a };

    public static implicit operator string(RomanNumeral a)
        => a.ToString();

    public static implicit operator RomanNumeral(string a)
        => Parse(a);
}
