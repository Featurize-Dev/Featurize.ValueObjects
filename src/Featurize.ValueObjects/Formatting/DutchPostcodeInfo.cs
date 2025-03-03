﻿using System.Text.RegularExpressions;

namespace Featurize.ValueObjects.Formatting;

internal partial class DutchPostcodeInfo() : PostcodeFormatInfo("NL")
{
    [GeneratedRegex(@"^(\d{4})((?![Ss][AaDdSs])[A-Za-z]{2})$")]
    private static partial Regex PostcodeRegex();

    public override bool TryParse(string s, out Postcode result)
    {
        var matchResult = GetMatchedResult(s);
        if (!matchResult.Success)
        {
            result = Postcode.Unknown;
            return false;
        }

        result = Postcode.Create(s, this);
        return true;
    }

    public override string ToString(string value, PostcodeStringFormat? format)
    {
        var matchResult = GetMatchedResult(value);
        var numbers = matchResult.Groups[1];
        var letters = matchResult.Groups[2];
        return format switch
        {
            PostcodeStringFormat.Compact => $"{numbers}{letters}",
            _ => $"{numbers} {letters}"
        };
    }

    private static Match GetMatchedResult(string s)
    {
        var sanitised = s.Replace(" ", string.Empty).ToUpper();
        return PostcodeRegex().Match(sanitised);
    }
}