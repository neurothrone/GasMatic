using Microsoft.JSInterop;
using System.Globalization;
using GasMatic.Core.Interfaces;

namespace GasMatic.Blazor.Wasm.Services;

public class CultureService : ICultureService
{
    private readonly IJSRuntime _jsRuntime;

    public CultureService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public CultureInfo GetCulture()
    {
        var js = (IJSInProcessRuntime)_jsRuntime;
        var languageCode = js.Invoke<string?>("clientCulture.get") ?? "en-US";
        return new CultureInfo(languageCode);
    }

    public void SaveCulture(CultureInfo culture)
    {
        var js = (IJSInProcessRuntime)_jsRuntime;
        js.InvokeVoid("clientCulture.set", culture.Name);
        js.InvokeVoid("changeHtmlLang", culture.Name);
    }
}