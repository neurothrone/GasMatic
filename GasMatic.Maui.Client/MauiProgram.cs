using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using DevExpress.Maui;
using GasMatic.Maui.Client.Services;
using GasMatic.Maui.Client.Views.About;
using GasMatic.Maui.Client.Views.GasVolume;
using GasMatic.Maui.Client.Views.Settings;
using GasMatic.Maui.Core.Services.Alerts;
using GasMatic.Maui.Core.Services.Database;
using GasMatic.Maui.Core.Services.Environment;
using GasMatic.Maui.Core.Services.Interactions;
using GasMatic.Maui.Core.ViewModels;
using GasMatic.Mobile.Resources.Strings;
using GasMatic.Shared.Services;
using Localization;
using Localization.Maui;

namespace GasMatic.Maui.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                // Custom fonts
                fonts.AddFont("Roboto-Regular.ttf");
                fonts.AddFont("Roboto-Bold.ttf");
            });

        builder.RegisterThirdPartyServices();
        builder.RegisterAppServices();
        builder.RegisterViewModels();
        builder.RegisterViews();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void RegisterThirdPartyServices(this MauiAppBuilder builder)
    {
        builder.UseMauiCommunityToolkit();
        builder.UseDevExpress(); // Register Handlers for DevExpress Components
        builder.UseDevExpressControls();
    }

    private static void RegisterAppServices(this MauiAppBuilder builder)
    {
        // Load environment variables
        IDotEnvService dotEnvService = new DotEnvService();
        dotEnvService.LoadEnvironmentVariables(
            FileSystem.OpenAppPackageFileAsync("env.txt"));
        builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();

        // Load localization resources
        builder.Services.AddSingleton<ILocalizationManager, LocalizationManager>();

        var resources = new LocalizedResourcesProvider(AppResources.ResourceManager);
        builder.Services.AddSingleton<ILocalizedResourcesProvider>(resources);

        builder.Services.AddSingleton<IAlertService, AlertService>();
        builder.Services.AddSingleton<IAppInteractionsService, AppInteractionsService>();

        builder.Services.AddSingleton<IGasVolumeService, GasVolumeService>();
        builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
    }

    private static void RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<GasVolumeCalculatorViewModel>();

        builder.Services.AddTransient<GasVolumeHistoryViewModel>();
        builder.Services.AddTransient<GasVolumeItemViewModel>();

        builder.Services.AddTransient<SettingsViewModel>();

        builder.Services.AddTransient<AboutViewModel>();
    }

    private static void RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<GasVolumeCalculatorPage>();
        builder.Services.AddTransient<GasVolumeHistoryPage>();
        builder.Services.AddTransient<AboutPage>();
        builder.Services.AddTransient<SettingsPage>();
    }
}