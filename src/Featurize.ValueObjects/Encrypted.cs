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


[TypeConverter(typeof(ValueObjectTypeConverter))]
[JsonConverter(typeof(EncryptedConverter))]
[DebuggerDisplay("{DebuggerDisplay}")]
public record Encrypted<T> : IValueObject<Encrypted<T>>
{
    private static byte[] _unknown = Encoding.UTF8.GetBytes("?");
    private byte[] _value = Array.Empty<byte>();
    public static Encrypted<T> Unknown => new() { _value = _unknown };
    public static Encrypted<T> Empty => new();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => IsEmpty() ? "{empty}" : ToString();

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

    public T? Decrypt(ICryptoTransform decryptor, TypeConverter? converter = null)
    {
        converter ??= TypeDescriptor.GetConverter(typeof(T));

        if (this == Unknown)
        {
            try
            {
                return converter.ConvertFromString("?") is { } unknown
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

        }
        return result;
    }

    public override string ToString()
    {
        return Convert.ToBase64String(_value);
    }

    public static Encrypted<T> Parse(string s) =>
        Parse(s, CultureInfo.InvariantCulture);

    public static Encrypted<T> Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Encrypted<T> result)
        => TryParse(s, CultureInfo.InvariantCulture, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Encrypted<T> result)
    {
        result = Empty;
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }
        if (s == "?")
        {
            result = Unknown;
            return true;
        }

        result = new() { _value = Convert.FromBase64String(s) };
        return true;
    }

    public bool IsEmpty() => this == Empty;
}
