using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace GasMatic.Components.Widgets;

public partial class CultureSelect
{
    [Parameter, EditorRequired]
    public CultureInfo SelectedCulture { get; set; } = null!;

    [Parameter]
    public EventCallback<CultureInfo> OnCultureSelected { get; set; }

    private readonly CultureInfo[] _supportedCultures =
    [
        new CultureInfo("en-US"),
        new CultureInfo("sv-SE")
    ];

    private void CultureSelected(CultureInfo culture)
    {
        OnCultureSelected.InvokeAsync(culture);
    }
}