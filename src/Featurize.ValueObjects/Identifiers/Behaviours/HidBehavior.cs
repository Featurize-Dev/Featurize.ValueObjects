using System.Text.RegularExpressions;

namespace Featurize.ValueObjects.Identifiers.Behaviours;

/// <inheritdoc />
public class HidBehaviour : IdBehaviour
{
    private Regex _pattern => new(@"^(?<Year>[1-9][0-9]{3})" + Name + @"(?<Number>[0-9]{3,18})$", RegexOptions.Compiled);

    /// <summary>
    /// The name part to use in the YEAR-{NAME}-000000000 pattern.
    /// </summary>
    protected virtual string Name => GetType().Name.Replace("Behaviour", "").ToUpperInvariant();
    
    /// <inheritdoc />
    public override object Next() => $"{DateTime.Now.Year}-{Name}-{NextNumber():000}";

    /// <inheritdoc />
    public override bool Supports(object id)
    {
        if (id is null) return false;
        return TryParse(id as string, out var _);
    }

    /// <inheritdoc />
    public override string ToString(object id)
    {
        return id as string ?? string.Empty;
    }

    /// <inheritdoc />
    public override bool TryParse(string? s, out object id)
    {
        id = string.Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }
        else if (_pattern.Match(Normalize(s)) is { Success: true } match)
        {
            id = $"{match.Groups["Year"].Value}-{Name}-{match.Groups["Number"].Value}";
            return true;
        }
        else
        {
            return false;
        }

    }

    private static string Normalize(string str)
        => str.ToUpperInvariant()
        .Replace("-", string.Empty, StringComparison.InvariantCulture)
        .Replace(".", string.Empty, StringComparison.InvariantCulture)
        .Trim();

    private static uint NextNumber() => BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 8);
}
