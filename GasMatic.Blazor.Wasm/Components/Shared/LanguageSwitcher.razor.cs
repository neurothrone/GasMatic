using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GasMatic.Blazor.Wasm.Components.Shared;

public partial class LanguageSwitcher
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = null!;

    [Inject]
    public ILogger<LanguageSwitcher> Logger { get; set; } = null!;

    private readonly CultureInfo[] _cultures =
    [
        new CultureInfo("en"),
        new CultureInfo("sv-se")
    ];

    private CultureInfo SelectedCulture
    {
        get => CultureInfo.CurrentCulture;
        set
        {
            if (Equals(CultureInfo.CurrentCulture, value))
                return;

            var js = (IJSInProcessRuntime)JsRuntime;
            js.InvokeVoid("clientCulture.set", value.Name);
            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);

            Logger.LogInformation($"Language switched to {value.Name}");
        }
    }
}