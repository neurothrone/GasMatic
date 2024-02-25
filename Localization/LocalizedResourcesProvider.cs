using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Localization;

public class LocalizedResourcesProvider : INotifyPropertyChanged, ILocalizedResourcesProvider
{
    private readonly ResourceManager _resourceManager;
    private CultureInfo _currentCulture;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocalizedResourcesProvider Instance { get; private set; }
#pragma warning restore CS8618

    public string this[string key] => _resourceManager.GetString(key, _currentCulture) ?? key;

    public LocalizedResourcesProvider(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
        _currentCulture = CultureInfo.CurrentUICulture;
        Instance = this;
    }

    public void UpdateCulture(CultureInfo cultureInfo)
    {
        _currentCulture = cultureInfo;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}