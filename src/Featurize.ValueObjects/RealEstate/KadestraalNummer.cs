using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Featurize.ValueObjects.RealEstate;

/// <summary>
/// Het unieke kadastrale nummer dat het perceel identificeert.
/// </summary>
public class KadestraalNummer : IValueObject<KadestraalNummer>
{
    /// <summary>
    /// De gemeente code waar het kadastrale perceel zich bevindt.
    /// </summary>
    public string GemeenteCode { get; private set; }

    /// <summary>
    /// De sectiecode binnen de gemeente.
    /// </summary>
    public string SectieCode { get; private set; }

    /// <summary>
    /// Het perceelnummer binnen de sectie.
    /// </summary>
    public int PerceelNummer { get; private set; }

    private KadestraalNummer(string gemeenteCode, string sectieCode, int perceelNummer)
    {
        GemeenteCode = gemeenteCode;
        SectieCode = sectieCode;
        PerceelNummer = perceelNummer;
    }

    /// <summary>
    /// Create a new instance of a kadestraal nummer.
    /// </summary>
    /// <param name="gemeenteCode">De gemeente code waar het kadastrale perceel zich bevindt.</param>
    /// <param name="sectieCode">De sectiecode binnen de gemeente.</param>
    /// <param name="perceelNummer">Het perceelnummer binnen de sectie.</param>
    /// <returns></returns>
    public static KadestraalNummer Create(string gemeenteCode, string sectieCode, int perceelNummer)
        => new(gemeenteCode, sectieCode, perceelNummer);
    
    /// <inhertdoc />
    public static KadestraalNummer Unknown 
        => new(ValueObject.UnknownValue, ValueObject.UnknownValue, -1);

    /// <inhertdoc />
    public static KadestraalNummer Empty 
        => new(string.Empty, string.Empty, -1);

    /// <inhertdoc />
    public override string ToString() 
        => ToString(null, null);

    /// <inhertdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => KadestraalNummerParser.ToString(this);

    /// <inhertdoc />
    public static KadestraalNummer Parse(string s)
        => Parse(s, null);

    /// <inhertdoc />
    public static KadestraalNummer Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : Unknown;

    /// <inhertdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out KadestraalNummer result)
        => TryParse(s, null, out result);

    /// <inhertdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out KadestraalNummer result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        if (KadestraalNummerParser.TryParse(s, out var r))
        {
            result = new KadestraalNummer(r.GemeenteCode, r.SectieCode, r.PerceelNummer);
            return true;
        }

        result = Unknown;
        return false;
    }
}



internal partial class KadestraalNummerParser
{
    
    public static string ToString(KadestraalNummer kn)
    {
        if (kn == KadestraalNummer.Unknown)
        {
            return ValueObject.UnknownValue;
        }

        if (kn == KadestraalNummer.Empty)
        {
            return string.Empty;
        }

        return $"{kn.GemeenteCode} {kn.SectieCode} {kn.PerceelNummer}";
    }

    public static bool TryParse(string s, [NotNullWhen(true)] out KadestraalParseResult? result)
    {
        var regex = KadestraalNummerRegex();
        var match = regex.Match(s);

        if (!match.Success)
        {
            result = null;
            return false;
        }

        // Extract de componenten van het kadastrale nummer
        string gemeenteCode = match.Groups[1].Value;
        string sectieCode = match.Groups[2].Value;

        if (!int.TryParse(match.Groups[3].Value, out int perceelNummer))
        {
            result = null;
            return false;
        }

        // Maak een nieuwe parser met de gevonden waarden
        result = new KadestraalParseResult
        {
            GemeenteCode = gemeenteCode,
            SectieCode = sectieCode,
            PerceelNummer = perceelNummer
        };

        return true;
    }

    internal class KadestraalParseResult
    {
        public required string GemeenteCode { get; init; }
        public required string SectieCode { get; init; }
        public required int PerceelNummer { get; init; }
    }

    // Regular expression om het kadastrale nummer te valideren (bijv. "AMR01 A 1234")
    [GeneratedRegex(@"^([A-Z]{2,5}\d{2})\s([A-Z])\s(\d+)$")]
    private static partial Regex KadestraalNummerRegex();
}
