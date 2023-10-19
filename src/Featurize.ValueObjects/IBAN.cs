using Featurize.ValueObjects.Extensions;
using Featurize.ValueObjects.Interfaces;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Featurize.ValueObjects;

public record struct IBAN() : IValueObject<IBAN>
{
    private const string _unknownValue = "?";
    private string? _bankAccountNumber = null;
    public Country Country { get; private set; }
    public static IBAN Unknown => new() { _bankAccountNumber = null, Country = Country.Unknown };
    public static IBAN Empty => new() { _bankAccountNumber = null, Country = Country.Empty };

    public override string ToString()
    {
        return _bankAccountNumber ?? _unknownValue;
    }

    public static IBAN Parse(string s)
        => Parse(s, null);

    public static IBAN Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out IBAN result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out IBAN result)
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

        var normalized = Normalize(s).ToString();

        if (IbanParser.TryParse(normalized, out var country))
        {
            result = new() { _bankAccountNumber = normalized, Country = country };
            return true;
        }

        result = Unknown;
        return false;
    }

    public bool IsEmpty()
        => this == Empty;



    private static ReadOnlySpan<char> Normalize(ReadOnlySpan<char> value)
    {
        int length = value.Length;

        char[] poolBuffer = ArrayPool<char>.Shared.Rent(length);
        try
        {
            Span<char> buffer = poolBuffer;
            var pos = 0;
            var hasModified = false;
            for (int i = 0; i < length; i++)
            {
                var c = value[i];
                if (c.IsSingleLineWhitespace())
                {
                    hasModified = true;
                    continue;
                }

                if (c.IsAsciiLetter())
                {
                    var newCh = (char)(c & ~' ');
                    hasModified |= c != newCh;
                    buffer[pos++] = newCh;
                }
                else
                {
                    buffer[pos++] = c;
                }
            }

            return hasModified
                ? buffer[..pos]
                : value;

        }
        finally
        {
            ArrayPool<char>.Shared.Return(poolBuffer);
        }
    }
}

internal static partial class IbanParser
{
    public static bool TryParse(string value, out Country country)
    {
        var re = IbanRegex();
        var match = re.Match(value);
        country = Country.All.FirstOrDefault(x => x.ISO2 == match.Groups[1].Value);
        return match.Success;
    }

    [GeneratedRegex("(^[a-zA-Z]{2})([0-9]{2})([a-zA-Z0-9]{4})([0-9]{7})(([a-zA-Z0-9]?){0,16}$)")]
    private static partial Regex IbanRegex();
}