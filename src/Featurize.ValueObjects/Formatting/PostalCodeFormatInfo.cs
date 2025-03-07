using System.Globalization;
using System.Net.Http.Headers;

namespace Featurize.ValueObjects.Formatting;
/// <summary>
/// Represents the base class for postcode format information, providing mechanisms to parse and format postcodes.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PostalCodeFormatInfo"/> class with the specified format name.
/// </remarks>
/// <param name="name">The name of the postcode format.</param>
public abstract class PostalCodeFormatInfo(string name) : IFormatProvider
{
    /// <summary>
    /// Gets the name of the postcode format.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the postcode format information associated with the current culture.
    /// </summary>
    public static PostalCodeFormatInfo CurrentInfo => GetByCultureInfo(CultureInfo.CurrentCulture);

    /// <summary>
    /// Gets the postcode format information for the Netherlands.
    /// </summary>
    public static PostalCodeFormatInfo NL { get; } = new DutchPostalCodeInfo();

    /// <summary>
    /// Gets the postcode format information for unknown formats.
    /// </summary>
    public static PostalCodeFormatInfo Unknown { get; } = new UnknownPostalCodeInfo();

    internal static PostalCodeFormatInfo FindByName(string formatName) =>
        formatName switch
        {
            "nl" => NL,
            _ => Unknown
        };

    /// <summary>
    /// Returns an object that provides formatting services for the specified type.
    /// </summary>
    /// <param name="formatType">An object that specifies the type of format object to return.</param>
    /// <returns>An instance of the format object, if formatType is supported; otherwise, null.</returns>
    public object? GetFormat(Type? formatType) => formatType == typeof(PostalCodeFormatInfo) ? this : null;

    /// <summary>
    /// Retrieves an instance of <see cref="PostalCodeFormatInfo"/> from the specified format provider.
    /// </summary>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <returns>A <see cref="PostalCodeFormatInfo"/> object.</returns>
    public static PostalCodeFormatInfo GetInstance(IFormatProvider? provider)
    {
        if (provider == null)
        {
            return NL;
        }

        if (provider is CultureInfo cultureInfo)
        {
            return GetByCultureInfo(cultureInfo);
        }

        return provider as PostalCodeFormatInfo ??
               provider.GetFormat(typeof(PostalCodeFormatInfo)) as PostalCodeFormatInfo ??
               CurrentInfo;
    }

    internal static PostalCodeFormatInfo GetByCultureInfo(CultureInfo cultureInfo) =>
        FindByName(cultureInfo.TwoLetterISOLanguageName.ToLower()) ?? Unknown;

    /// <summary>
    /// Tries to parse the specified string to a <see cref="PostalCode"/> object.
    /// </summary>
    /// <param name="s">The string representation of a postcode.</param>
    /// <param name="result">When this method returns, contains the parsed <see cref="PostalCode"/> if successful, or null if failed.</param>
    /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
    public abstract bool TryParse(string s, out PostalCode result);

    /// <summary>
    /// Converts the specified postcode value to its string representation.
    /// </summary>
    /// <param name="value">The postcode value to convert.</param>
    /// <param name="format">The string format to use.</param>
    /// <returns>A string representation of the postcode.</returns>
    public abstract string ToString(string value, PostcodeStringFormat? format);
}

/// <summary>
/// Represents possible formatting of postcode in ToString.
/// </summary>
public enum PostcodeStringFormat
{
    /// <summary>
    /// The official format of postcode
    /// </summary>
    Official = 0,

    /// <summary>
    /// Compact representation of postcode
    /// </summary>
    Compact = 10,
}
