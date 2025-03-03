namespace Featurize.ValueObjects.Formatting;

internal class UnknownPostcodeInfo() : PostcodeFormatInfo("Unknown")
{
    public override bool TryParse(string s, out Postcode result)
    {
        result = Postcode.Create(s, this);
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
