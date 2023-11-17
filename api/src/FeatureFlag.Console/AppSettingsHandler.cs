using Microsoft.Extensions.Configuration;

namespace FeatureFlag.Console;

internal class AppSettingsHandler
{
    internal IConfiguration BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables();
        return builder.Build();
    }
}
