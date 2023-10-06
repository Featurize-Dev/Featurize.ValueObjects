namespace Featurize.ValueObjects.Interfaces;

/// <summary>
/// Marks a ValueObject that it can be empty
/// </summary>
/// <typeparam name="TSelf">The type that can be empty.</typeparam>
public interface IEmpty<TSelf>
    where TSelf : IEmpty<TSelf>
{
    /// <summary>
    /// The empty representation of <typeparamref name="TSelf"/>.
    /// </summary>
    public static abstract TSelf Empty { get; }

    /// <summary>
    /// Checks if the <typeparamref name="TSelf"/> is empty.
    /// </summary>
    /// <returns>returns true if empty.</returns>
    public bool IsEmpty();
}
