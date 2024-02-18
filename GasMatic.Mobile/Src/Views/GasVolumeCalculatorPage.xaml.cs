namespace GasMatic.Mobile.Views;

public partial class GasVolumeCalculatorPage : ContentPage
{
    public GasVolumeCalculatorPage(Client.Core.ViewModels.GasVolumeCalculatorViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}