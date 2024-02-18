namespace GasMatic.Client.Core.Services;

public interface IAppInteractionsService
{
    Task OpenBrowserAsync(string url);
    Task ComposeSupportEmailAsync(string recipient, string subject);
}

public class AppInteractionsService : IAppInteractionsService
{
    public async Task OpenBrowserAsync(string url)
    {
        try
        {
            var uri = new Uri(url);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
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