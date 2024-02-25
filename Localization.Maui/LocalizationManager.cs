using System.Globalization;

namespace Localization.Maui;

public class LocalizationManager(ILocalizedResourcesProvider resourceProvider) : ILocalizationManager
{
    private CultureInfo? _currentCulture;

    public void RestorePreviousCulture(CultureInfo defaultCulture = null) => SetCulture(
        GetUserCulture(defaultCulture)
    );

    public CultureInfo GetUserCulture(CultureInfo defaultCulture = null)
    {
        if (_currentCulture is not null)
            return _currentCulture;

        var culture = Preferences.Default.Get("UserCulture", string.Empty);
        if (string.IsNullOrEmpty(culture))
            _currentCulture = defaultCulture ?? CultureInfo.CurrentCulture;
        else
            _currentCulture = new CultureInfo(culture);

        return _currentCulture;
    }

    public void UpdateUserCulture(CultureInfo cultureInfo)
    {
        Preferences.Default.Set("UserCulture", cultureInfo.Name);
        SetCulture(cultureInfo);
    }

    private void SetCulture(CultureInfo cultureInfo)
    {
        _currentCulture = cultureInfo;
        Application.Current?.Dispatcher.Dispatch(() =>
        {
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        });
        resourceProvider.UpdateCulture(cultureInfo);
    }
}