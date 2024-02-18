using System.Globalization;

namespace GasMatic.Client.Converters;

public class IntToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double val)
            return val >= 100;

        return false;
    }

    object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException("IntToBoolConverter can only be used for one way conversion.");
    }
}