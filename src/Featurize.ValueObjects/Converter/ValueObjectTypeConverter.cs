using System.Globalization;
using System.ComponentModel;
using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects.Converter;

internal sealed class ValueObjectTypeConverter<T> : TypeConverter
    where T : IValueObject<T>
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
       => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        => T.TryParse(value as string, culture, out var result)
        ? result
        : base.ConvertFrom(context, culture, value);
}

/// <summary>
/// Converts an object to an ValueObject.
/// </summary>
public sealed class ValueObjectTypeConverter : TypeConverter
{
    private readonly TypeConverter _converter;

    /// <summary>
    /// Constructs a <see cref="ValueObjectTypeConverter"/> for a specific type. 
    /// </summary>
    /// <param name="type">The type of the value object.</param>
    public ValueObjectTypeConverter(Type type)
    {
        var converterType = typeof(ValueObjectTypeConverter<>).MakeGenericType(type);
        _converter = (TypeConverter)Activator.CreateInstance(converterType)!;
    }
    
    /// <inheritdoc />
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => _converter.CanConvertFrom(sourceType);
    
    /// <inheritdoc />
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        => _converter.ConvertTo(context, culture, value, destinationType);
    
    /// <inheritdoc />
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        => _converter.ConvertFrom(context, culture, value);
}
