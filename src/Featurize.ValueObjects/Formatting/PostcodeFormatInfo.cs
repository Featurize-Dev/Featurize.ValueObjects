using System.Globalization;

namespace Featurize.ValueObjects.Formatting;
/// <summary>
/// Represents the base class for postcode format information, providing mechanisms to parse and format postcodes.
/// </summary>
public abstract class PostcodeFormatInfo : IFormatProvider
{
    private static readonly List<PostcodeFormatInfo> _allFormats = new()
    {
        new DutchPostcodeInfo(),
        new UnknownPostcodeInfo()
    };

    /// <summary>
    /// Gets the name of the postcode format.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PostcodeFormatInfo"/> class with the specified format name.
    /// </summary>
    /// <param name="name">The name of the postcode format.</param>
    protected PostcodeFormatInfo(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets the postcode format information associated with the current culture.
    /// </summary>
    public static PostcodeFormatInfo CurrentInfo => GetByCultureInfo(CultureInfo.CurrentCulture);

    /// <summary>
    /// Gets the postcode format information for the Netherlands.
    /// </summary>
    public static PostcodeFormatInfo NL => FindByName(nameof(NL));

    /// <summary>
    /// Gets the postcode format information for unknown formats.
    /// </summary>
    public static PostcodeFormatInfo Unknown => FindByName(nameof(Unknown));

    internal static PostcodeFormatInfo FindByName(string formatName) =>
        _allFormats.Single(x => x.Name.Equals(formatName));

    /// <summary>
    /// Returns an object that provides formatting services for the specified type.
    /// </summary>
    /// <param name="formatType">An object that specifies the type of format object to return.</param>
    /// <returns>An instance of the format object, if formatType is supported; otherwise, null.</returns>
    public object? GetFormat(Type? formatType) => formatType == typeof(PostcodeFormatInfo) ? this : null;

    /// <summary>
    /// Retrieves an instance of <see cref="PostcodeFormatInfo"/> from the specified format provider.
    /// </summary>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <returns>A <see cref="PostcodeFormatInfo"/> object.</returns>
    public static PostcodeFormatInfo GetInstance(IFormatProvider? provider)
    {
        if (provider == null)
        {
            return NL;
        }

        if (provider is CultureInfo cultureInfo)
        {
            return GetByCultureInfo(cultureInfo);
        }

        return provider as PostcodeFormatInfo ??
               provider.GetFormat(typeof(PostcodeFormatInfo)) as PostcodeFormatInfo ??
               CurrentInfo;
    }

    internal static PostcodeFormatInfo GetByCultureInfo(CultureInfo cultureInfo) =>
        _allFormats.FirstOrDefault(x => x.Name.Equals(cultureInfo.TwoLetterISOLanguageName.ToUpper())) ?? Unknown;

    /// <summary>
    /// Tries to parse the specified string to a <see cref="Postcode"/> object.
    /// </summary>
    /// <param name="s">The string representation of a postcode.</param>
    /// <param name="result">When this method returns, contains the parsed <see cref="Postcode"/> if successful, or null if failed.</param>
    /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
    public abstract bool TryParse(string s, out Postcode result);

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
