using Featurize.ValueObjects.Identifiers;
using System.Globalization;

namespace Featurize.ValueObjects.Identifiers.Behaviours;

public class Int32Behaviour : IdBehaviour
{
    public override object Next()
    {
        throw new NotImplementedException();
    }

    public override bool Supports(object id)
    {
        return id is int;
    }

    public override string ToString(object id)
    {
        return ((int)id).ToString();
    }

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
