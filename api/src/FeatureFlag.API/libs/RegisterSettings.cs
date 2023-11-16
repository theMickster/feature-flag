using FeatureFlag.Application.Exceptions;
using FeatureFlag.Common.Settings;

namespace FeatureFlag.API.libs;

internal static class RegisterSettings
{
    internal static WebApplicationBuilder RegisterCommonSettings(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var cosmosDbSettings = configuration.GetSection(CosmosDbConnectionSettings.SettingsRootName) ??
                               throw new ConfigurationException("The required Configuration settings keys for the Azure Cosmos Db Settings are missing. Please verify configuration.");
        builder.Services.AddOptions<CosmosDbConnectionSettings>().Bind(cosmosDbSettings);

        var cacheSettings = configuration.GetSection(CacheSettings.SettingsRootName) ??
                            throw new ConfigurationException("The required Configuration settings keys for the Cache Settings are missing. Please verify configuration.");

        builder.Services.AddOptions<CacheSettings>().Bind(cacheSettings);

        return builder;
    }
}
