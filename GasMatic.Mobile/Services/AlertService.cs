using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GasMatic.Client.Core.Services.Alerts;
using Font = Microsoft.Maui.Font;

namespace GasMatic.Mobile.Services;

public class AlertService : IAlertService
{
    async Task IAlertService.ShowAlertAsync(string title, string message, string cancel)
    {
        await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.DisplayAlert(title, message, cancel));
    }

    async Task<bool> IAlertService.ShowConfirmationPromptAsync(string title, string message)
    {
        return await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.DisplayAlert(
            title,
            message: message,
            accept: "Yes",
            cancel: "Cancel"
        ));
    }

    async Task IAlertService.ShowSnackbarAsync(string text)
    {
        CancellationTokenSource cancellationTokenSource = new();
        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.DarkSlateBlue,
            TextColor = Colors.White,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(14),
        };
        var duration = TimeSpan.FromSeconds(3);

        var snackbar = Snackbar.Make(message: text,
            actionButtonText: string.Empty,
            duration: duration,
            visualOptions: snackbarOptions);
        await snackbar.Show(cancellationTokenSource.Token);
    }
}