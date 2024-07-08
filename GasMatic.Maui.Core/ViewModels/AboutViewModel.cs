using CommunityToolkit.Mvvm.Input;
using GasMatic.Maui.Core.Services.Environment;
using GasMatic.Maui.Core.Services.Interactions;

namespace GasMatic.Maui.Core.ViewModels;

public partial class AboutViewModel(
    IAppInteractionsService appInteractionsService,
    IConfigurationService configurationService)
{
    private const string LinkedinUrl = "https://www.linkedin.com/in/neurothrone/";

    public string AppTitle => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    public string Author => "Zane Neurothrone";

    [RelayCommand]
    private async Task OpenSupportEmailLink()
    {
        await appInteractionsService.ComposeSupportEmailAsync(
            configurationService.GetSupportEmail(),
            "GasMatic Support"
        );
    }

    [RelayCommand]
    private async Task OpenLinkedinWebLink() => await appInteractionsService.OpenBrowserAsync(LinkedinUrl);
}