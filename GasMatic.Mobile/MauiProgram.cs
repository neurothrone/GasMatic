using CommunityToolkit.Maui;
using DevExpress.Maui;
using GasMatic.Client.Core.Features.GasVolume;
using GasMatic.Client.Core.Services.Alerts;
using GasMatic.Client.Core.Services.Database;
using GasMatic.Client.Core.Services.Environment;
using GasMatic.Client.Core.Services.Interactions;
using GasMatic.Client.Core.ViewModels;
using GasMatic.Mobile.Resources.Strings;
using GasMatic.Mobile.Services;
using GasMatic.Mobile.Views.About;
using GasMatic.Mobile.Views.GasVolume;
using GasMatic.Mobile.Views.Settings;
using Localization;
using Localization.Maui;
using Microsoft.Extensions.Logging;

namespace GasMatic.Mobile;

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