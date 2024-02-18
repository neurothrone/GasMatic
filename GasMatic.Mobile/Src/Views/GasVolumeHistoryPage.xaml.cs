namespace GasMatic.Mobile.Views;

public partial class GasVolumeHistoryPage : ContentPage
{
    public GasVolumeHistoryPage(Client.Core.ViewModels.GasVolumeHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}