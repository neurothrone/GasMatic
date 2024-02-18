namespace GasMatic.Mobile.Views.Settings;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(Client.Core.ViewModels.SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}