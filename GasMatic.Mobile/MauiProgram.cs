using DevExpress.Maui;
using GasMatic.Client.Core.Features.GasVolume;
using GasMatic.Client.Core.Features.GasVolume.Database;
using GasMatic.Client.Core.Services;
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

        builder.Services.AddTransient<Views.GasVolumeCalculatorPage>();
        builder.Services.AddTransient<Client.Core.ViewModels.GasVolumeCalculatorViewModel>();

        builder.Services.AddTransient<Views.GasVolumeHistoryPage>();
        builder.Services.AddTransient<Client.Core.ViewModels.GasVolumeHistoryViewModel>();
        builder.Services.AddTransient<Client.Core.ViewModels.GasVolumeViewModel>();

        builder.Services.AddTransient<Views.SettingsPage>();
        builder.Services.AddTransient<Client.Core.ViewModels.SettingsViewModel>();

        builder.Services.AddTransient<Views.AboutPage>();
        builder.Services.AddTransient<Client.Core.ViewModels.AboutViewModel>();

        builder.Services.AddSingleton<IGasVolumeService, GasVolumeService>();
        builder.Services.AddSingleton<IGasVolumeDatabase, GasVolumeDatabase>();
        builder.Services.AddSingleton<IAppInteractionsService, AppInteractionsService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}