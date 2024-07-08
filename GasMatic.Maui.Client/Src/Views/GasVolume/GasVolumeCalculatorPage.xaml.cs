using GasMatic.Maui.Core.ViewModels;

namespace GasMatic.Maui.Client.Views.GasVolume;

public partial class GasVolumeCalculatorPage : ContentPage
{
    public GasVolumeCalculatorPage(GasVolumeCalculatorViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}