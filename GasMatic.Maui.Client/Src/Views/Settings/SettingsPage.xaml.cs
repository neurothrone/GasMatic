using GasMatic.Maui.Core.ViewModels;

namespace GasMatic.Maui.Client.Views.Settings;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}