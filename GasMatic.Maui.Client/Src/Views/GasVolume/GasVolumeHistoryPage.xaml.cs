using GasMatic.Maui.Core.ViewModels;

namespace GasMatic.Maui.Client.Views.GasVolume;

public partial class GasVolumeHistoryPage : ContentPage
{
    public GasVolumeHistoryPage(GasVolumeHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}