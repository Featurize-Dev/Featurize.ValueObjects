using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Featurize.ValueObjects;


/// <summary>
/// Represents an encrypted value of T.
/// </summary>
/// <typeparam name="T"></typeparam>
[DebuggerDisplay("{DebuggerDisplay}")]
[JsonConverter(typeof(EncryptedConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
public record struct Encrypted<T>() : IValueObject<Encrypted<T>>
{
    private static readonly byte[] _unknown = Encoding.UTF8.GetBytes(ValueObject.UnknownValue);

    private byte[] _value = Array.Empty<byte>();
    
    /// <summary>
    /// Represents an unkown value.
    /// </summary>
    public static Encrypted<T> Unknown => new() { _value = _unknown };
    
    /// <summary>
    /// Represents an empty value.
    /// </summary>
    public static Encrypted<T> Empty => new();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string DebuggerDisplay => this.IsEmpty() ? "{empty}" : ToString();

    /// <summary>
    /// Creates an new instance of <see cref="Encrypted{T}"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="encryptor"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public static Encrypted<T> Create(T value, ICryptoTransform encryptor, TypeConverter? converter = null)
    {
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        using var writer = new StreamWriter(cryptoStream);

        converter ??= TypeDescriptor.GetConverter(typeof(T));
        writer.Write(converter.ConvertToString(value));
        writer.Flush();
        cryptoStream.FlushFinalBlock();
        return new Encrypted<T> { _value = memoryStream.ToArray() };
    }

    /// <summary>
    /// Decrypts a encrypted value to its original type.
    /// </summary>
    /// <param name="decryptor">The Decryption to use.</param>
    /// <param name="converter">The TypeConverter to use.</param>
    /// <returns>Returns the original value if Decrypted successfull otherwise Null.</returns>
    public readonly T? Decrypt(ICryptoTransform decryptor, TypeConverter? converter = null)
    {
        converter ??= TypeDescriptor.GetConverter(typeof(T));

        if (this == Unknown)
        {
            try
            {
                return converter.ConvertFromString(ValueObject.UnknownValue) is { } unknown
                   ? (T?)unknown
                   : default;
            }
            catch
            {
                return default;
            }
        }

        using var cryptoStream = new CryptoStream(new MemoryStream(_value), decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cryptoStream);

        var result = default(T?);
        try
        {
            result = converter.ConvertFromString(reader.ReadToEnd()) is { } decrytped
                ? (T?)decrytped
                : default;
        }
        catch (CryptographicException ex)
        {
            Debug.WriteLine(ex.Message);
            return default;
        }
        return result;
    }

    /// <summary>
    /// Returns a base64 string that represents the encrypted value.
    /// </summary>
    /// <returns></returns>
    public override readonly string ToString()
    {
        return Convert.ToBase64String(_value);
    }

    /// <summary>
    /// Converts the string representation to its <see cref="Encrypted{T}"/> equivalent.
    /// </summary>
    /// <param name="s">String value of an encrypted value.</param>
    /// <returns></returns>
    public static Encrypted<T> Parse(string s) =>
        Parse(s, CultureInfo.InvariantCulture);

    /// <summary>
    /// Converts the string representation to its <see cref="Encrypted{T}"/> equivalent.
    /// </summary>
    /// <param name="s">String value of an encrypted value.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about s. If provider is null, the thread current culture is used.</param>
    /// <returns>Returns EmailAddress object.</returns>
    /// <exception cref="FormatException"></exception>
    public static Encrypted<T> Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    /// <summary>
    /// Converts the string representation to its <see cref="Encrypted{T}"/> equivalent.
    /// </summary>
    /// <param name="s">String value of an encrypted value.</param>
    /// <param name="result">EmailAddress object.</param>
    /// <returns>true if s was converted successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Encrypted<T> result)
        => TryParse(s, CultureInfo.InvariantCulture, out result);

    /// <summary>
    /// Converts the string representation to its <see cref="Encrypted{T}"/> equivalent.
    /// </summary>
    /// <param name="s">String value of an encrypted value.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about s. If provider is null, the thread current culture is used.</param>
    /// <param name="result">EmailAddress object.</param>
    /// <returns>true if s was converted successfully; otherwise, false.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Encrypted<T> result)
    {
        result = Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }
        if (s == ValueObject.UnknownValue)
        {
            result = Unknown;
            return true;
        }

        result = new() { _value = Convert.FromBase64String(s) };
        return true;
    }
}
