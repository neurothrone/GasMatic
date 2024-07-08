namespace GasMatic.Maui.Core.Services.Environment;

public class DotEnvService : IDotEnvService
{
    async void IDotEnvService.LoadEnvironmentVariables(Task<Stream> dotEnvStreamTask)
    {
        var stream = await dotEnvStreamTask;

        using var reader = new StreamReader(stream, System.Text.Encoding.UTF8);
        while (await reader.ReadLineAsync() is { } line)
        {
            var parts = line.Split(
                '=',
                2,
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue; // Skip malformed lines

            var key = parts[0].Trim();
            var value = parts[1].Trim().Trim('"'); // Remove whitespace and quotes

            System.Environment.SetEnvironmentVariable(key, value);
        }
    }
}