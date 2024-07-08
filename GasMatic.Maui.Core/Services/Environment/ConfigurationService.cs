namespace GasMatic.Maui.Core.Services.Environment;

public class ConfigurationService : IConfigurationService
{
    string IConfigurationService.GetSupportEmail() =>
        System.Environment.GetEnvironmentVariable("SUPPORT_EMAIL") ??
        throw new SystemException("Support Email missing");
}