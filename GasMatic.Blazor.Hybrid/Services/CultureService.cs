using System.Globalization;
using GasMatic.Core.Interfaces;

namespace GasMatic.Blazor.Hybrid.Services;

public class CultureService : ICultureService
{
    public CultureInfo GetCulture()
    {
        var languageCode = Preferences.Default.Get(nameof(CultureInfo), defaultValue: "en-US");
        return new CultureInfo(languageCode);
    }

    public void SaveCulture(CultureInfo culture)
    {
        Preferences.Default.Set(nameof(CultureInfo), culture.Name);
        
        Application.Current?.Dispatcher.Dispatch(() =>
        {
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        });
    }
}