using CommunityToolkit.Mvvm.Input;
using GasMatic.Client.Core.Services;

namespace GasMatic.Client.Core.ViewModels;

public partial class AboutViewModel
{
    private readonly IAppInteractionsService _appInteractionsService;

    private const string LinkedinUrl = "https://www.linkedin.com/in/neurothrone/";
    private const string SupportEmail = "support@neurothrone.tech";

    public string AppTitle => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    public string Author => "Zane Neurothrone";
    public string DotNetTextCredits => "This app is powered by .NET MAUI";
    public string DotNetDetailCredits => "Written in XAML & C#";
    public string AppIconCredit => "App icon by svgrepo.com";

    public AboutViewModel(IAppInteractionsService appInteractionsService)
    {
        _appInteractionsService = appInteractionsService;
    }

    [RelayCommand]
    private async Task OpenSupportEmailLink()
    {
        await _appInteractionsService.ComposeSupportEmailAsync(
            SupportEmail,
            "GasMatic Support"
        );
    }

    [RelayCommand]
    private async Task OpenLinkedinWebLink() => await _appInteractionsService.OpenBrowserAsync(LinkedinUrl);
}