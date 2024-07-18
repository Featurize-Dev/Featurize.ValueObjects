using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects;

/// <summary>
/// Default checks for Empty and Unknown.
/// </summary>
public static class ValueObjectExtensions
{
    /// <summary>
    /// Indicates if this object is Unknown or Empty.
    /// </summary>
    public static bool IsEmptyOrUnknown<T>(this T value)
        where T : IValueObject<T>
        => value.IsEmpty() || value.IsUnknown();

    /// <summary>
    /// Indicates if this object is unknown.
    /// </summary>
    public static bool IsUnknown<T>(this T value)
        where T : IUnknown<T>
        => T.Unknown.Equals(value);

    /// <summary>
    /// Indicates if this object is empty.
    /// </summary>
    public static bool IsEmpty<T>(this T value)
        where T : IEmpty<T>
        => T.Empty.Equals(value);
}
