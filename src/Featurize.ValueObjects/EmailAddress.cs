using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Featurize.ValueObjects;


/// <summary>
/// Object that represents a EmailAddress.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay}")]
[JsonConverter(typeof(ValueObjectJsonConverter))]

public record struct EmailAddress() : IValueObject<EmailAddress>
{
    private string _value = string.Empty;
    
    /// <summary>
    /// The local part of an email address.
    /// </summary>
    public readonly string Local => _value is { Length: > 1 } ? _value[.._value.IndexOf('@')] : string.Empty;

    /// <summary>
    /// The domain part of an email address.
    /// </summary>
    public readonly string Domain => _value is { Length: > 2 } ? _value[(_value.IndexOf('@') + 1)..] : string.Empty;

    /// <summary>
    /// Indicates if an emailaddress is IP Based.
    /// </summary>
    public readonly bool IsIPBased => _value is { Length: > 1 } && _value[^1] == ']';

    /// <summary>
    /// The IPAddress of of IPBased email address.
    /// </summary>
    public readonly IPAddress IPDomain
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

    /// <inheritdoc />
    public override readonly string ToString()
    {
        return _value;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string DebuggerDisplay => ToString();

    /// <inheritdoc />
    public static EmailAddress Unknown => new() { _value = "?" };

    /// <inheritdoc />
    public static EmailAddress Empty => new();

    /// <inheritdoc />
    public static EmailAddress Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var result) ? result : throw new FormatException();
    }

    /// <inheritdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out EmailAddress result)
    {
        result = Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }
        else if (s == "?")
        {
            result = Unknown;
            return true;
        }
        else if (!s.Contains('@'))
        {
            result = Unknown;
            return false;
        }
        else if (EmailParser.TryParse(s, out string email))
        {
            result = new() { _value = email };
            return true;
        }
        else return false;
    }

    /// <inheritdoc />
    public static EmailAddress Parse(string s)
        => Parse(s, CultureInfo.InvariantCulture);

    /// <inheritdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out EmailAddress result)
        => TryParse(s, CultureInfo.InvariantCulture, out result);
}

internal static partial class EmailParser
{
    public static bool TryParse(string value, out string email)
    {
        var re = EmailRegex();
        var match = re.Match(value);
        email = match.Groups["email"].Value;

        return match.Success;
    }

    [GeneratedRegex("\"?((?<name>.*?)\"?\\s*<)?(?<email>[^>]*)")]
    private static partial Regex EmailRegex();
}