using GasMatic.Core.ViewModels;

namespace GasMatic.Blazor.Wasm.Components.Pages.GasVolume;

public partial class GasVolume
{
    private enum GasVolumeTab
    {
        Calculator,
        History
    }

    private GasVolumeInputViewModel _inputViewModel = new();
    private GasVolumeTab _tab = GasVolumeTab.Calculator;

    private void ChangeTabTo(GasVolumeTab tab)
    {
        _tab = tab;
    }
}