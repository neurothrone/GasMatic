namespace GasMatic.Mobile.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(Client.Core.ViewModels.SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}