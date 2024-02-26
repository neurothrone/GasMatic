using GasMatic.Client.Core.Services.Alerts;
using GasMatic.Client.Core.Services.Interactions;
using Localization;

namespace GasMatic.Mobile.Services;

public class AppInteractionsService(
    IAlertService alertService,
    ILocalizedResourcesProvider resources) : IAppInteractionsService
{
    public async Task OpenBrowserAsync(string url)
    {
        try
        {
            var uri = new Uri(url);
            BrowserLaunchOptions options = new()
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Colors.DarkSlateBlue,
                PreferredControlColor = Colors.Gold
            };
            await Browser.Default.OpenAsync(uri, options);
        }
        catch (Exception)
        {
            await alertService.ShowAlertAsync(
                resources["BrowserFailedTitle"],
                resources["BrowserFailedMessage"],
                "OK");
        }
    }

    public async Task ComposeSupportEmailAsync(string recipient, string subject)
    {
        if (!Email.Default.IsComposeSupported)
            return;

        var message = new EmailMessage
        {
            Subject = subject,
            BodyFormat = EmailBodyFormat.PlainText,
            To = [recipient]
        };

        await Email.Default.ComposeAsync(message);
    }
}