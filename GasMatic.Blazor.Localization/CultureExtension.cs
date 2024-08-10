using System.Globalization;
using Microsoft.JSInterop;

namespace GasMatic.Blazor.Localization;

public static class CultureExtension
{
    public static async Task SetDefaultCultureAsync(this IJSRuntime jsRuntime)
    {
        var result = await jsRuntime.InvokeAsync<string?>("clientCulture.get") ?? "en-US";
        await jsRuntime.InvokeVoidAsync("changeHtmlLang", result.Equals("en-US") ? "en-US" : "sv-SE");

        var culture = new CultureInfo(result);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}