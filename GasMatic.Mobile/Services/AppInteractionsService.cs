using GasMatic.Client.Core.Services.Alerts;
using GasMatic.Client.Core.Services.Interactions;

namespace GasMatic.Mobile.Services;

public class AppInteractionsService(
    IAlertService alertService) : IAppInteractionsService
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
                "An unexpected error occurred.",
                "No browser may be installed on the device.",
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