using Localization;

namespace GasMatic.Mobile;

public partial class App : Application
{
    public App(ILocalizationManager localizationManager)
    {
        localizationManager.RestorePreviousCulture();
        Application.Current.UserAppTheme = AppTheme.Dark;

        InitializeComponent();

        MainPage = new AppShell();
    }
}