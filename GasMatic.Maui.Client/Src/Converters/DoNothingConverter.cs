using System.Globalization;

namespace GasMatic.Maui.Client.Converters;

public class DoNothingConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Break here to inspect value during debugging
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Break here to inspect value during debugging
        return value;
    }
}