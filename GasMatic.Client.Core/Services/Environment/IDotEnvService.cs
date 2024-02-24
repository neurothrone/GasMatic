namespace GasMatic.Client.Core.Services.Environment;

public interface IDotEnvService
{
    void LoadEnvironmentVariables(Task<Stream> dotEnvStreamTask);
}