using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Formatting;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json.Serialization;


namespace Featurize.ValueObjects.RealEstate;


/// <summary>
///     Represents a postal code value object with support for various formats.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay}")]
[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
public record struct PostalCode() : IValueObject<PostalCode>, IUnknown<PostalCode>
{
    private string _formatName = PostalCodeFormatInfo.Unknown.Name;
    private string _value = string.Empty;

    /// <summary>
    ///     Gets the format information for the current postal code.
    /// </summary>
    public readonly PostalCodeFormatInfo Format => PostalCodeFormatInfo.FindByName(_formatName);


    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string DebuggerDisplay =>
        this.DebuggerDisplay(x => $"{x.ToString()}: {x._formatName}");

    /// <summary>
    ///     Gets an unknown postal code.
    /// </summary>
    public static PostalCode Unknown => new()
    {
        _value = ValueObject.UnknownValue,
        _formatName = PostalCodeFormatInfo.Unknown.Name
    };

    /// <summary>
    ///     Gets an empty postal code.
    /// </summary>
    public static PostalCode Empty => new();

    /// <summary>
    ///     Parses the specified string to a <see cref="PostalCode" /> using the provided format provider.
    /// </summary>
    /// <param name="s">The string representation of a postal code.</param>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <returns>A <see cref="PostalCode" /> parsed from the input string.</returns>
    /// <exception cref="FormatException">Thrown when the string is not a valid postal code.</exception>
    public static PostalCode Parse(string s, IFormatProvider? provider) =>
        TryParse(s, provider, out var result) ? result : throw new FormatException($"'{s}' is not a valid postalcode.");

    /// <summary>
    ///     Tries to parse the specified string to a <see cref="PostalCode" /> using the provided format provider.
    /// </summary>
    /// <param name="s">The string representation of a postal code.</param>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <param name="result">
    ///     When this method returns, contains the parsed <see cref="PostalCode" /> if successful, or
    ///     <see cref="Empty" /> if failed.
    /// </param>
    /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static bool TryParse(string? s, IFormatProvider? provider, out PostalCode result)
    {
        if (string.IsNullOrEmpty(s))
        {
            result = Empty;
            return false;
        }

        if (s == ValueObject.UnknownValue)
        {
            result = Unknown;
            return false;
        }

        var formatInfo = PostalCodeFormatInfo.GetInstance(provider);
        return formatInfo.TryParse(s, out result);
    }

    /// <summary>
    ///     Gets the default culture for postal code formatting.
    /// </summary>
    public static CultureInfo DefaultCulture => CultureInfo.InvariantCulture;

    /// <summary>
    ///     Parses the specified string to a <see cref="PostalCode" /> using the default culture.
    /// </summary>
    /// <param name="s">The string representation of a postal code.</param>
    /// <returns>A <see cref="PostalCode" /> parsed from the input string.</returns>
    public static PostalCode Parse(string s) => Parse(s, null);

    /// <summary>
    ///     Tries to parse the specified string to a <see cref="PostalCode" /> using the default culture.
    /// </summary>
    /// <param name="s">The string representation of a postal code.</param>
    /// <param name="result">
    ///     When this method returns, contains the parsed <see cref="PostalCode" /> if successful, or
    ///     <see cref="Empty" /> if failed.
    /// </param>
    /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static bool TryParse(string? s, out PostalCode result) => TryParse(s, null, out result);

    internal static PostalCode Create(string value, PostalCodeFormatInfo info) => new()
    {
        _value = value,
        _formatName = info.Name
    };

    /// <summary>
    ///     Changes the format of the postal code using the specified format provider.
    /// </summary>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <returns>A new <see cref="PostalCode" /> with the updated format.</returns>
    public readonly PostalCode ChangeFormat(IFormatProvider provider) => Parse(_value, provider);

    /// <summary>
    ///     Tries to change the format of the postal code using the specified format provider.
    /// </summary>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <param name="result">
    ///     When this method returns, contains the postal code with the updated format if successful;
    ///     otherwise, the unchanged postal code.
    /// </param>
    /// <returns><c>true</c> if the format was successfully changed; otherwise, <c>false</c>.</returns>
    public readonly bool TryChangeFormat(IFormatProvider provider, out PostalCode result) =>
        TryParse(_value, provider, out result);

    /// <inheritdoc />
    public override readonly string ToString() => ToString(null, null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    public readonly string ToString(string? format = null, IFormatProvider? formatProvider = null)
    {
        var formatter = formatProvider == null ? Format : PostalCodeFormatInfo.GetInstance(formatProvider);

        var f = format switch
        {
            "C" => PostcodeStringFormat.Compact,
            _ => PostcodeStringFormat.Official,
        };

        if (formatter.Name != Format.Name)
        {
            return Parse(_value, formatter).ToString(format);
        }

        return formatter.ToString(_value, f);
    }

    /// <summary>
    ///     Determines whether a specified string is equal to the value of the postal code.
    /// </summary>
    /// <param name="left">The string to compare with the postal code.</param>
    /// <param name="right">The postal code to compare.</param>
    /// <returns><c>true</c> if the string is equal to the postal code; otherwise, <c>false</c>.</returns>
    public static bool operator ==(string left, PostalCode right) => left == right._value;

    /// <summary>
    ///     Determines whether a specified string is not equal to the value of the postal code.
    /// </summary>
    /// <param name="left">The string to compare with the postal code.</param>
    /// <param name="right">The postal code to compare.</param>
    /// <returns><c>true</c> if the string is not equal to the postal code; otherwise, <c>false</c>.</returns>
    public static bool operator !=(string left, PostalCode right) => !(left == right);

    /// <summary>
    ///     Determines whether the value of the postal code is equal to a specified string.
    /// </summary>
    /// <param name="left">The postal code to compare.</param>
    /// <param name="right">The string to compare with the postal code.</param>
    /// <returns><c>true</c> if the postal code is equal to the string; otherwise, <c>false</c>.</returns>
    public static bool operator ==(PostalCode left, string right) => left._value == right;

    /// <summary>
    ///     Determines whether the value of the postal code is not equal to a specified string.
    /// </summary>
    /// <param name="left">The postal code to compare.</param>
    /// <param name="right">The string to compare with the postal code.</param>
    /// <returns><c>true</c> if the postal code is not equal to the string; otherwise, <c>false</c>.</returns>
    public static bool operator !=(PostalCode left, string right) => !(left == right);

    /// <summary>
    ///     Converts a postalcode to a string.
    /// </summary>
    /// <param name="value">The postal code to cenvert</param>
    public static implicit operator string(PostalCode value) => value.ToString();

    /// <summary>
    ///     Converts a string to a postalcode.
    /// </summary>
    /// <param name="s"></param>
    public static explicit operator PostalCode(string s) => Parse(s);
}