using System.Globalization;
using GasMatic.Core.Interfaces;

namespace GasMatic.Blazor.Hybrid;

public partial class App : Application
{
    public App(ICultureService cultureService)
    {
        InitializeComponent();

        var culture = cultureService.GetCulture();
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        MainPage = new MainPage();
    }
}