using System.Globalization;

namespace CalorieClient.Views.Converters;

public class ConfidenceToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is int score ? score switch
        {
            >= 8 => Color.FromArgb("#34C759"),  // green
            >= 5 => Color.FromArgb("#FF9500"),  // orange
            _    => Color.FromArgb("#FF3B30"),  // red
        } : Colors.Gray;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => null;
}
