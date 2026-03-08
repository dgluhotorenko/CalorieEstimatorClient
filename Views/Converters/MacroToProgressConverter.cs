using System.Globalization;

namespace CalorieClient.Views.Converters;

public class MacroToProgressConverter : IValueConverter, IMarkupExtension
{
    public double Max { get; set; } = 100;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is double val ? Math.Clamp(val / Max, 0, 1) : 0;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => null;

    public object ProvideValue(IServiceProvider serviceProvider) => this;
}