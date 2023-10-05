namespace Featurize.ValueObjects.Identifiers;

public abstract class IdBehaviour
{
    public abstract bool Supports(object id);
    public abstract object Next();
    public abstract string ToString(object id);
    public abstract bool TryParse(string? s, out object id);
}
