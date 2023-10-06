namespace Featurize.ValueObjects.Identifiers;

/// <summary>
/// Bstract class for generating Ids
/// </summary>
public abstract class IdBehaviour
{
    /// <summary>
    /// Tries to convert the object representation of an identifier to the type supported by this behaviour, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public abstract bool Supports(object id);

    /// <summary>
    /// Returns the next generated id.
    /// </summary>
    /// <returns>The next Identifier</returns>
    public abstract object Next();

    /// <summary>
    /// Returns a string that represents the identifier supported by this behaviour.
    /// </summary>
    /// <param name="id">On object representation of the identifier.</param>
    /// <returns></returns>
    public abstract string ToString(object id);

    /// <summary>
    /// Tries to convert the string representation of an indentifier to its equivalent supported by this behaviour, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="s">A string representing of the indentifier to convert.</param>
    /// <param name="id">An object representing the identifier of this behaviour.</param>
    /// <returns>true if s was converted successfully; otherwise, false.</returns>
    public abstract bool TryParse(string? s, out object id);
}
