using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects;

internal static class ValueObject
{
    public const string UnknownValue = "?";

    internal static string DebuggerDisplay<T>(this T obj, Func<T, string>? str = null)
        where T : IEmpty<T>
        =>
        obj.IsEmpty() ? "{EMPTY}" : $"{str?.Invoke(obj) ?? obj.ToString()}";
}