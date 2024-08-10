using Microsoft.AspNetCore.Components;
using System.Globalization;
using GasMatic.Core.Interfaces;

namespace GasMatic.Components.Pages;

public partial class Settings
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ICultureService CultureService { get; set; } = null!;

    private CultureInfo _currentCulture = CultureInfo.CurrentCulture;

    private void ChangeCulture(CultureInfo culture)
    {
        if (Equals(CultureInfo.CurrentCulture, culture))
            return;

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        _currentCulture = culture;

        CultureService.SaveCulture(culture);
    }
}