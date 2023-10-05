namespace Featurize.ValueObjects.Identifiers.Behaviours;

public class GuidBehaviour : IdBehaviour
{
    public override object Next()
    {
        return Guid.NewGuid();
    }

    public override bool Supports(object id)
    {
        return id is Guid;
    }

    public override string ToString(object id)
    {
        return ((Guid)id).ToString();
    }

    public override bool TryParse(string? s, out object id)
    {
        if (Guid.TryParse(s, out Guid guid))
        {
            id = guid;
            return true;
        }

        id = default(Guid);
        return false;
    }
}
