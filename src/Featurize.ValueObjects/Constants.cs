using Featurize.ValueObjects.Interfaces;
using System.Diagnostics;

namespace Featurize.ValueObjects;
internal static class Constants
{
    internal static string UnknownValue = "?";

    internal static string DebuggerDisplay<T>(this T obj, Func<T, string>? str = null)
        where T : IEmpty<T>
        =>
        obj.IsEmpty() ? "{EMPTY}" : $"{str?.Invoke(obj) ?? obj.ToString()}";

}
