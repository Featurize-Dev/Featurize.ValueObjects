namespace Featurize.ValueObjects.Identifiers.Behaviours;

/// <inheritdoc />
public class GuidBehaviour : IdBehaviour
{
    /// <inheritdoc />
    public override object Next()
    {
        return Guid.NewGuid();
    }

    /// <inheritdoc />
    public override bool Supports(object id)
    {
        return id is Guid;
    }

    /// <inheritdoc />
    public override string ToString(object id)
    {
        return ((Guid)id).ToString();
    }

    /// <inheritdoc />
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
