namespace Featurize.ValueObjects.Interfaces;

/// <summary>
/// Marks a object that it has an Unknown value.
/// </summary>
/// <typeparam name="TSelf"></typeparam>
public interface IUnknown<TSelf>
    where TSelf : IUnknown<TSelf>
{
    /// <summary>
    /// Unknown value representation of an object.
    /// </summary>
    public static abstract TSelf Unknown { get; }
}
