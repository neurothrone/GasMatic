namespace GasMatic.Client.Core.Services;

public interface IAppInteractionsService
{
    Task OpenBrowserAsync(string url);
    Task ComposeSupportEmailAsync(string recipient, string subject);
}