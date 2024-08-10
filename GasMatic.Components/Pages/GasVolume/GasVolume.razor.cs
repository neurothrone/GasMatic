using GasMatic.Core.ViewModels;

namespace GasMatic.Components.Pages.GasVolume;

public partial class GasVolume
{
    private enum GasVolumeTab
    {
        Calculator,
        History
    }

    private readonly GasVolumeInputViewModel _inputViewModel = new();
    private GasVolumeTab _tab = GasVolumeTab.Calculator;

    private void ChangeTabTo(GasVolumeTab tab)
    {
        _tab = tab;
    }
}