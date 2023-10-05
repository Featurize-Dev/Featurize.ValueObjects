using System.Globalization;
using System.ComponentModel;
using Featurize.ValueObjects.Interfaces;

namespace Featurize.ValueObjects.Converter;

public class ValueObjectTypeConverter<T> : TypeConverter
    where T : IValueObject<T>
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
       => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        => T.TryParse(value as string, culture, out var result)
        ? result
        : base.ConvertFrom(context, culture, value);
}
public sealed class ValueObjectTypeConverter : TypeConverter
{
    private TypeConverter _converter;
    public ValueObjectTypeConverter(Type type)
    {
        var converterType = typeof(ValueObjectTypeConverter<>).MakeGenericType(type);
        _converter = (TypeConverter)Activator.CreateInstance(converterType)!;
    }
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => _converter.CanConvertFrom(sourceType);

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        => _converter.ConvertTo(context, culture, value, destinationType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        => _converter.ConvertFrom(context, culture, value);
}
