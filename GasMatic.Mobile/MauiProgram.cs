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
            })
            // Register Handlers for DevExpress Components
            .UseDevExpress();

        builder.Services.AddSingleton<App>();

        builder.Services.AddTransient<GasVolumeCalculatorPage>();
        builder.Services.AddTransient<GasVolumeCalculatorViewModel>();

        builder.Services.AddTransient<GasVolumeHistoryPage>();
        builder.Services.AddTransient<GasVolumeHistoryViewModel>();
        builder.Services.AddTransient<GasVolumeItemViewModel>();

        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsViewModel>();

        builder.Services.AddTransient<AboutPage>();
        builder.Services.AddTransient<AboutViewModel>();

        builder.Services.AddSingleton<IGasVolumeService, GasVolumeService>();
        builder.Services.AddSingleton<IGasVolumeDatabase, GasVolumeDatabase>();
        builder.Services.AddSingleton<IAppInteractionsService, AppInteractionsService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}