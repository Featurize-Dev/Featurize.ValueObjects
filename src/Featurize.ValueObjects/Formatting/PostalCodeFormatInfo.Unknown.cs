namespace Featurize.ValueObjects.Formatting;

internal class UnknownPostalCodeInfo() : PostalCodeFormatInfo("Unknown")
{
    public override bool TryParse(string s, out PostalCode result)
    {
        result = PostalCode.Create(s, this);
        return true;
    }

    public override string ToString(string value, PostcodeStringFormat? format)
    {
        return format switch
        {
            PostcodeStringFormat.Compact => value.Replace(" ", string.Empty),
            _ => value
        };
    }
}
