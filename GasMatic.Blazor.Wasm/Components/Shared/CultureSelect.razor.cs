using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GasMatic.Blazor.Wasm.Components.Shared;

public partial class CultureSelect
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = null!;

    private readonly CultureInfo[] _supportedCultures =
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
            js.InvokeVoid("changeHtmlLang", value.Name);
            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        }
    }
}