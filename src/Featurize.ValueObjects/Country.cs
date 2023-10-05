using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Featurize.ValueObjects;


/// <summary>
/// Represents a Country.
/// </summary>
public record Country : IValueObject<Country>
{
    private string _value = string.Empty;

    /// <summary>
    /// Unknown country.
    /// </summary>
    public static Country Unknown => new() { _value = "?" };

    /// <summary>
    /// Empty country object.
    /// </summary>
    public static Country Empty => new();

    /// <summary>
    /// Name of the country
    /// </summary>
    public string Name => _value;

    /// <summary>
    /// Display name of a country.
    /// </summary>
    public string DisplayName => GetDisplayName(CultureInfo.CurrentCulture);

    /// <summary>
    /// English name of a country.
    /// </summary>
    public string EnglishName => GetDisplayName(CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse a string to a country object.
    /// </summary>
    /// <param name="s">String value of a country.</param>
    /// <param name="provider">Format provider.</param>
    /// <returns></returns>
    public static Country Parse(string s, IFormatProvider? provider)
    {
        return new Country();
    }

    /// <summary>
    /// Parse a string to a country.
    /// </summary>
    /// <param name="s">String value of a country.</param>
    /// <returns>Return a Country object.</returns>
    public static Country Parse(string s)
        => Parse(s, CultureInfo.CurrentCulture);

    /// <summary>
    /// Tries to parse a string to a country.
    /// </summary>
    /// <param name="s">String value of a country.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">Country object.</param>
    /// <returns>True if parse was succesfull.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Country result)
    {
        result = new Country();
        return true;
    }

    /// <summary>
    /// Tries to parse a string to a country.
    /// </summary>
    /// <param name="s">String value of a country.</param>
    /// <param name="result">Country object.</param>
    /// <returns>True if succesfull parsed.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Country result)
        => TryParse(s, CultureInfo.CurrentCulture, out result);

    /// <summary>
    /// Indicator if this is empty
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty() => _value == string.Empty;

    private string GetDisplayName(CultureInfo currentCulture)
    {
        throw new NotImplementedException();
    }
}
