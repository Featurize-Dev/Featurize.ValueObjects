using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects.Identifiers;

/// <summary>
/// Represents a Id with a specific behaviour.
/// </summary>
/// <typeparam name="TBehavior">The type of the behaviour.</typeparam>
[DebuggerDisplay("{DebuggerDisplay}")]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[JsonConverter(typeof(IdConverter))]
public record struct Id<TBehavior>() : IValueObject<Id<TBehavior>>
    where TBehavior : IdBehaviour, new()
{
    private object? _value = null;
    private static readonly IdBehaviour _behaviour = new TBehavior();
    
    /// <inheritdoc />
    public static Id<TBehavior> Unknown => new() { _value = "?" };

    /// <inheritdoc />
    public static Id<TBehavior> Empty => new();

    /// <summary>
    /// Generates a new identifier with <typeparamref name="TBehavior"/>.
    /// </summary>
    /// <returns>Instance of <see cref="Id{TBehavior}"/>.</returns>
    public static Id<TBehavior> Next() => new() { _value = _behaviour.Next() };

    /// <summary>
    /// Creates a new instance of <see cref="Id{TBehavior}"/>.
    /// </summary>
    /// <param name="id">Object representation of an identifier.</param>
    /// <returns>Instance of <see cref="Id{TBehavior}"/>.</returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Id<TBehavior> Create(object id)
    {
        if (_behaviour.Supports(id))
        {
            return new() { _value = id };
        }
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (this == Unknown) return "?";
        if (this == Empty) return string.Empty;
        return _behaviour.ToString(_value!);
    }

    /// <inheritdoc />
    public static Id<TBehavior> Parse(string s) => Parse(s, CultureInfo.InvariantCulture);

    /// <inheritdoc />
    public static Id<TBehavior> Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var id) ? id : throw new FormatException();

    /// <inheritdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Id<TBehavior> result) 
        => TryParse(s, CultureInfo.InvariantCulture, out result);

    /// <inheritdoc />
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Id<TBehavior> result)
    {
        result = Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }
        else if (_behaviour.TryParse(s, out var id))
        {

            result = new() { _value = id };

            return true;
        }
        else
        {
            result = Unknown;
            return false;
        }
    }

    /// <inheritdoc />
    public bool IsEmpty()
        => this == Empty;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => IsEmpty() ? "{empty}" : ToString();
}
