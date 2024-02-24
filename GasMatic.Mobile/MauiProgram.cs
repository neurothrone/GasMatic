using DevExpress.Maui;
using GasMatic.Client.Core.Features.GasVolume;
using GasMatic.Client.Core.Features.GasVolume.Database;
using GasMatic.Client.Core.Services;
using GasMatic.Client.Core.ViewModels;
using GasMatic.Mobile.Views.About;
using GasMatic.Mobile.Views.GasVolume;
using GasMatic.Mobile.Views.Settings;
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
        // Register Handlers for DevExpress Components
        builder.UseDevExpress();
    }

    private static void RegisterAppServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IGasVolumeService, GasVolumeService>();
        builder.Services.AddSingleton<IGasVolumeDatabase, GasVolumeDatabase>();
        builder.Services.AddSingleton<IAppInteractionsService, AppInteractionsService>();
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