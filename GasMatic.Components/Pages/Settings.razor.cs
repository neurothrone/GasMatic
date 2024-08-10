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

    private CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

    private void ChangeCulture(CultureInfo culture)
    {
        if (Equals(CultureInfo.CurrentCulture, culture))
            return;

        CultureService.SaveCulture(culture);
        NavigationManager.Refresh(forceReload: true);
    }
}