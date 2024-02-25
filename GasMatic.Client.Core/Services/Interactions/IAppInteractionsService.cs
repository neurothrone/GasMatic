namespace GasMatic.Client.Core.Services.Interactions;

public interface IAppInteractionsService
{
    Task OpenBrowserAsync(string url);
    Task ComposeSupportEmailAsync(string recipient, string subject);
}