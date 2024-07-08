using GasMatic.Maui.Core.ViewModels;

namespace GasMatic.Maui.Client.Views.About;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}