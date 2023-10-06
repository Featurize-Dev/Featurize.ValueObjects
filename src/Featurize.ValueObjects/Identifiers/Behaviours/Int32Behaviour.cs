using System.Globalization;

namespace Featurize.ValueObjects.Identifiers.Behaviours;

/// <inheritdoc/>
public class Int32Behaviour : IdBehaviour
{
    /// <inheritdoc/>
    public override object Next()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override bool Supports(object id)
    {
        return id is int;
    }

    /// <inheritdoc/>
    public override string ToString(object id)
    {
        return ((int)id).ToString();
    }

    /// <inheritdoc/>
    public override bool TryParse(string? s, out object id)
    {
        if (int.TryParse(s, CultureInfo.InvariantCulture, out var idenity))
        {
            id = idenity;
            return true;
        }

        id = default(int);
        return false;
    }
}
