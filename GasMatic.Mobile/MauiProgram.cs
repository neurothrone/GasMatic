using DevExpress.Maui;
using Microsoft.Extensions.Logging;

namespace GasMatic.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Register Handlers for DevExpress Components
            .UseDevExpress()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                // Custom fonts
                fonts.AddFont("Roboto-Regular.ttf");
                fonts.AddFont("Roboto-Bold.ttf");
            })
            .Services
                
            .AddTransient<Views.GasVolumeCalculatorPage>()
            .AddTransient<Client.Core.ViewModels.GasVolumeCalculatorViewModel>()
            
            .AddTransient<Views.GasVolumeHistoryPage>()
            .AddTransient<Client.Core.ViewModels.GasVolumeHistoryViewModel>()
            .AddTransient<Client.Core.ViewModels.GasVolumeViewModel>()
            
            .AddTransient<Views.SettingsPage>()
            .AddTransient<Client.Core.ViewModels.SettingsViewModel>()
            
            .AddTransient<Views.AboutPage>()
            .AddTransient<Client.Core.ViewModels.AboutViewModel>()
            ;

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}