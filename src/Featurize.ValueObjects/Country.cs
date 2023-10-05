using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Featurize.ValueObjects;

public record Country : IValueObject<Country>
{
    private string _value = string.Empty;

    public static Country Unknown => new() { _value = "?" };
    public static Country Empty => new();

    public string Name => _value;
    public string DisplayName => GetDisplayName(CultureInfo.CurrentCulture);
    public string EnglishName => GetDisplayName(CultureInfo.InvariantCulture);

    public static Country Parse(string s, IFormatProvider? provider)
    {
        return new Country();
    }

    public static Country Parse(string s)
        => Parse(s, CultureInfo.CurrentCulture);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Country result)
    {
        result = new Country();
        return true;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Country result)
        => TryParse(s, CultureInfo.CurrentCulture, out result);

    public bool IsEmpty() => _value == string.Empty;

    private string GetDisplayName(CultureInfo currentCulture)
    {
        throw new NotImplementedException();
    }
}
