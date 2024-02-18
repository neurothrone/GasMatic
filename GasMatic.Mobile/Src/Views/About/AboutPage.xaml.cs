namespace GasMatic.Mobile.Views.About;

public partial class AboutPage : ContentPage
{
    public AboutPage(Client.Core.ViewModels.AboutViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}