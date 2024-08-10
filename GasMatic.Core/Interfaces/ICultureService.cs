using System.Globalization;

namespace GasMatic.Core.Interfaces;

public interface ICultureService
{
    CultureInfo GetCulture();
    void SaveCulture(CultureInfo culture);
}