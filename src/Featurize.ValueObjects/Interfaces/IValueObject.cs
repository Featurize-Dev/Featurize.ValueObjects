using System.Diagnostics.CodeAnalysis;

namespace Featurize.ValueObjects.Interfaces;

/// <summary>
/// Marks an object as a ValueObject
/// </summary>
/// <typeparam name="TSelf"></typeparam>
public interface IValueObject<TSelf> : IUnknown<TSelf>, IEmpty<TSelf>, IParsable<TSelf>, IFormattable
    where TSelf : IUnknown<TSelf>, IEmpty<TSelf>, IParsable<TSelf>, IFormattable
{

    /// <summary>
    /// Returns a string representation of its value.
    /// </summary>
    /// <returns></returns>
    string ToString();

    /// <summary>
    /// Parse the string representation of an <typeparamref name="TSelf"/> to its <typeparamref name="TSelf"/> equivalent.
    /// </summary>
    /// <param name="s">A string representing the <typeparamref name="TSelf"/> to convert.</param>
    /// <returns>a instance of <typeparamref name="TSelf"/>.</returns>
    abstract static TSelf Parse(string s);

    /// <summary>
    /// Parse the string representation of an <typeparamref name="TSelf"/> to its <typeparamref name="TSelf"/> equivalent.
    /// </summary>
    /// <param name="s">String value of an <typeparamref name="TSelf"/>.</param>
    /// <param name="result">Returns a instance of <typeparamref name="TSelf"/>. if succesfull</param>
    /// <returns>true if s was converted successfully; otherwise, false.</returns>
    /// <exception cref="FormatException"></exception>
    abstract static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out TSelf result);
}
