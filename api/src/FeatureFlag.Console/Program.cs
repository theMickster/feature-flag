using FeatureFlag.Application.Data;
using FeatureFlag.Application.Exceptions;
using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Settings;
using FeatureFlag.Console;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Hello, from the Feature Flag Console App!");

var configuration = new AppSettingsHandler().BuildConfiguration();

var cosmosSettings = configuration.GetSection(CosmosDbConnectionSettings.SettingsRootName).Get<CosmosDbConnectionSettings>() ??
                     throw new ConfigurationException("The required Configuration settings keys for the Azure Cosmos Db Settings are missing. Please verify configuration.");

var cosmosClient = await InitializeCosmosAsync(cosmosSettings);

await ValidateFeatureFlagDbContext();

await ValidateFeatureFlagMetadataDbContext();

Console.ForegroundColor = ConsoleColor.White;


#region Local Functions

static async Task<CosmosClient> InitializeCosmosAsync(CosmosDbConnectionSettings cosmosSettings)
{
    var cosmosContainers = new List<(string, string)>
    {
        (cosmosSettings.DatabaseName!, CosmosContainerConstants.MainContainer),
        (cosmosSettings.DatabaseName!, CosmosContainerConstants.MetadataContainer)
    };

    return await CosmosClient.CreateAndInitializeAsync(cosmosSettings.Account, cosmosSettings.SecurityKey,
        cosmosContainers);

}

async Task ValidateFeatureFlagMetadataDbContext()
{
    var contextOptions = new DbContextOptionsBuilder<FeatureFlagMetadataDbContext>()
        .UseCosmos(cosmosSettings.Account, cosmosSettings.SecurityKey, cosmosSettings.DatabaseName)
        .Options;

    await using var context = new FeatureFlagMetadataDbContext(contextOptions);

    var applications = await context.Applications.ToListAsync();
    var environments = await context.Environments.ToListAsync();
    var ruleTypes = await context.RuleTypes.ToListAsync();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("");
    Console.WriteLine("Application Metadata Cosmos Data");
    applications.ForEach(x =>
        Console.WriteLine($"Id: {x.MetadataId}, TypeId: {x.TypeId}, Name: {x.Name}, Metadata Application Name: {x.ApplicationName}"));

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("");
    Console.WriteLine("Environments Metadata Cosmos Data");
    environments.ForEach(x =>
        Console.WriteLine($"Id: {x.MetadataId}, TypeId: {x.TypeId}, Name: {x.Name}, Metadata Application Name: {x.ApplicationName}"));

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("");
    Console.WriteLine("RuleTypes Metadata Cosmos Data");
    ruleTypes.ForEach(x =>
        Console.WriteLine($"Id: {x.MetadataId}, TypeId: {x.TypeId}, Name: {x.Name}, Metadata Application Name: {x.ApplicationName}"));
}

async Task ValidateFeatureFlagDbContext()
{
    var contextOptions = new DbContextOptionsBuilder<FeatureFlagDbContext>()
        .UseCosmos(cosmosSettings.Account, cosmosSettings.SecurityKey, cosmosSettings.DatabaseName)
        .Options;

    await using var context = new FeatureFlagDbContext(contextOptions);

    var featureFlags = await context.FeatureFlags.ToListAsync();

    Console.WriteLine("");
    Console.WriteLine("Feature Flag Cosmos Data");
    featureFlags.ForEach(x =>
        Console.WriteLine($"Id: {x.FeatureFlagId}, Feature Flag: {x.Name}, Display Name: {x.DisplayName}"));

    Console.WriteLine("");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Feature Flag Configuration Cosmos Data");

    var featureFlagConfigs = await context.FeatureFlagConfigurations.ToListAsync();
    featureFlagConfigs.ForEach(x =>
        Console.WriteLine(
            $"Id: {x.FeatureFlagId}, Feature Flag: {x.Name}, Created By: {x.CreatedBy}, Modified By: {x.ModifiedBy}"));
}



#endregion Local Functions