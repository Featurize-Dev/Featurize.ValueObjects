using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Featurize.ValueObjects.Interfaces;

public interface IValueObject<TSelf> : IUnknown<TSelf>, IEmpty<TSelf>, IParsable<TSelf>
    where TSelf : IUnknown<TSelf>, IEmpty<TSelf>, IParsable<TSelf>
{
    abstract static TSelf Parse(string s);
    abstract static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out TSelf result);
}
