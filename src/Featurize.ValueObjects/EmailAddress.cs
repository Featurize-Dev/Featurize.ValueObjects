using Featurize.ValueObjects.Interfaces;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace Featurize.ValueObjects;

[DebuggerDisplay("{DebuggerDisplay}")]
public record struct EmailAddress() : IValueObject<EmailAddress>
{
    private string _value = string.Empty;

    public const int MaxLength = 254;
    public string Local => _value is { Length: > 1 } ? _value[.._value.IndexOf('@')] : string.Empty;
    public string Domain => _value is { Length: > 2 } ? _value[(_value.IndexOf('@') + 1)..] : string.Empty;
    public bool IsIPBased => _value is { Length: > 1 } && _value[^1] == ']';
    public IPAddress IPDomain
    {
        get
        {
            if (IsIPBased)
            {
                var ip = Domain.StartsWith("[IPv6:", StringComparison.InvariantCulture)
                    ? Domain[6..^1]
                    : Domain[1..^1];
                return IPAddress.Parse(ip);
            }
            return IPAddress.None;
        }
    }

    public override string ToString()
    {
        return _value;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => ToString();

    public static EmailAddress Unknown => new() { _value = "?" };

    public static EmailAddress Empty => new();

    public static EmailAddress Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var result) ? result : throw new FormatException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out EmailAddress result)
    {
        result = Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }
        else if (!s.Contains('@'))
        {
            result = Unknown;
            return false;
        }
        else if (s == "?")
        {
            result = Unknown;
            return true;
        }
        else if (EmailParser.TryParse(s, out string email))
        {
            result = new() { _value = email };
            return true;
        }
        else return false;
    }

    public bool IsEmpty() => string.IsNullOrEmpty(_value);

    public static EmailAddress Parse(string s)
        => Parse(s, CultureInfo.InvariantCulture);

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out EmailAddress result)
        => TryParse(s, CultureInfo.InvariantCulture, out result);
}


internal static class EmailParser
{
    public static bool TryParse(string value, out string email)
    {
        var re = new Regex(@"""?((?<name>.*?)""?\s*<)?(?<email>[^>]*)");
        var match = re.Match(value);
        email = match.Groups["email"].Value;

        return match.Success;
    }
}
