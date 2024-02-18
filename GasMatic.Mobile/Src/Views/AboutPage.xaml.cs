namespace GasMatic.Mobile.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage(Client.Core.ViewModels.AboutViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}