using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MVVM_Template.Converters.Base;

[MarkupExtensionReturnType(typeof(ConverterBase))]
public abstract class ConverterBase : MarkupExtension, IValueConverter
{
    /// <inheritdoc />
    public override object ProvideValue(IServiceProvider sp) => this;

    protected abstract object? Convert(object? value, Type? targetType, object? parameter, CultureInfo culture);

    protected virtual object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException("Обратное преобразование не поддерживается");

    /// <inheritdoc />
    object? IValueConverter.Convert(object? value, Type? targetType, object? parameter, CultureInfo culture) =>
        Convert(value, targetType, parameter, culture);

    /// <inheritdoc />
    object? IValueConverter.ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo culture) =>
        ConvertBack(value, targetType, parameter, culture);
}