using Microsoft.Extensions.Logging;
using GasMatic.Blazor.Hybrid.Services;
using GasMatic.Core.Interfaces;
using GasMatic.Core.Services;
using GasMatic.Maui.Sqlite.Interfaces;
using GasMatic.Maui.Sqlite.Repositories;
using GasMatic.Maui.Sqlite.Services;

namespace GasMatic.Blazor.Hybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IDatabaseRepository, DatabaseRepository>();
        builder.Services.AddSingleton<IGasVolumeDataSource, GasVolumeDataSource>();
        builder.Services.AddSingleton<IGasVolumeService, GasVolumeService>();
        builder.Services.AddSingleton<ICultureService, CultureService>();

        builder.Services.AddLocalization();

        return builder.Build();
    }
}