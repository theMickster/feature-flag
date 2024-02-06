using AutoMapper;
using FeatureFlag.Application.Data;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Profiles;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FeatureFlag.UnitTests.Setup;

[ExcludeFromCodeCoverage]
public abstract class PersistenceUnitTestBase : UnitTestBase
{
    private static readonly string _featureFlags = "FeatureFlags";
    private static readonly string _application = "Application";
    private static readonly Guid _applicationTypeId = new ("d28be6d8-b764-47f1-9ff7-0947cba39168");
    private static readonly string _environment = "Environment";
    private static readonly Guid _environmentTypeId = new("d732a26e-7ced-4f2c-8566-3132d1469baa");
    private static readonly string _ruleType = "RuleType";
    private static readonly Guid _ruleTypeTypeId = new("75c1577a-f353-4735-a53c-72c7b8dbe1c4");
    
    private readonly DbConnection _metadataConnection;
    private readonly DbConnection _primaryConnection;
    
    protected FeatureFlagMetadataDbContext MetadataDbContext;
    //protected FeatureFlagDbContext PrimaryDbContext;
    protected readonly IMapper Mapper;

    protected Mock<IDbContextFactory<FeatureFlagMetadataDbContext>> MockMetadataDbContextFactory = new();
    //protected Mock<IDbContextFactory<FeatureFlagDbContext>> MockPrimaryDbContextFactory = new();

    protected PersistenceUnitTestBase()
    {
        _metadataConnection = new SqliteConnection("Filename=:memory:");
        _primaryConnection = new SqliteConnection("Filename=:memory:");
        _metadataConnection.Open();
        _primaryConnection.Open();

        var metadataOptions = new DbContextOptionsBuilder<FeatureFlagMetadataDbContext>()
            .UseSqlite(_metadataConnection)
            .Options;

        //var primaryOptions = new DbContextOptionsBuilder<FeatureFlagDbContext>()
        //    .UseSqlite(_primaryConnection)
        //    .Options;

        MetadataDbContext = new FeatureFlagMetadataDbContext(metadataOptions);
        MetadataDbContext.Database.EnsureCreated();

        //PrimaryDbContext = new FeatureFlagDbContext(primaryOptions);
        //PrimaryDbContext.Database.EnsureCreated();

        MockMetadataDbContextFactory.Setup(x => x.CreateDbContext()).Returns(MetadataDbContext);
        MockMetadataDbContextFactory.Setup(x => x.CreateDbContextAsync(default)).ReturnsAsync(MetadataDbContext);
        //MockPrimaryDbContextFactory.Setup(x => x.CreateDbContext()).Returns(PrimaryDbContext);
        //MockPrimaryDbContextFactory.Setup(x => x.CreateDbContextAsync(default)).ReturnsAsync(PrimaryDbContext);

        var mapConfig = new MapperConfiguration(config =>
            config.AddMaps(typeof(FeatureFlagConfigEntityToModelProfile).Assembly)
        );
        mapConfig.AssertConfigurationIsValid();
        
        Mapper = mapConfig.CreateMapper();
        
        Setup();
    }

    protected sealed override void Setup()
    {
        MetadataDbContext.Applications.AddRange(new List<ApplicationEntity>
        {
            new()
            {
                Id = new Guid("37eb8f87-2b6f-4129-a574-335ab6602138"),
                MetadataId = new Guid("37eb8f87-2b6f-4129-a574-335ab6602138"),
                ApplicationName = _featureFlags,
                Name = "Beers",
                TypeId = _applicationTypeId,
                TypeName = _application
            },
            new()
            {
                Id = new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444"),
                MetadataId = new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444"),
                ApplicationName = _featureFlags,
                Name = "Adventure Works",
                TypeId = new Guid("d28be6d8-b764-47f1-9ff7-0947cba39168"),
                TypeName = _application
            }
        });

        MetadataDbContext.Environments.AddRange(new List<EnvironmentEntity>
        {
            new()
            {
                Id = new Guid("b121e69e-6f82-4504-a1d9-4952e1d0f9f5"),
                MetadataId = new Guid("b121e69e-6f82-4504-a1d9-4952e1d0f9f5"),
                Name = "Production",
                ApplicationName = _featureFlags,
                TypeId = _environmentTypeId,
                TypeName = _environment
            },
            new()
            {
                Id = new Guid("ea5d9785-0d42-4373-b0ab-b34762b96037"),
                MetadataId = new Guid("ea5d9785-0d42-4373-b0ab-b34762b96037"),
                Name = "Staging",
                ApplicationName = _featureFlags,
                TypeId = _environmentTypeId,
                TypeName = _environment
            },
            new()
            {
                Id = new Guid("153a4ccc-29ef-4f5f-a8f2-c6ab39f889d0"),
                MetadataId = new Guid("153a4ccc-29ef-4f5f-a8f2-c6ab39f889d0"),
                Name = "QA",
                ApplicationName = _featureFlags,
                TypeId = _environmentTypeId,
                TypeName = _environment
            },
            new()
            {
                Id = new Guid("587fd994-048f-486e-b9c0-288b0edd838e"),
                MetadataId = new Guid("587fd994-048f-486e-b9c0-288b0edd838e"),
                Name = "UAT",
                ApplicationName = _featureFlags,
                TypeId = _environmentTypeId,
                TypeName = _environment
            },
            new()
            {
                Id = new Guid("9694016d-ade5-4e8c-9c0e-b5101e272ab9"),
                MetadataId = new Guid("9694016d-ade5-4e8c-9c0e-b5101e272ab9"),
                Name = "Development",
                ApplicationName = _featureFlags,
                TypeId = _environmentTypeId,
                TypeName = _environment
            }
        });

        MetadataDbContext.RuleTypes.AddRange(new List<RuleTypeEntity>
        {
            new()
            {
                Id = new Guid("b1b66940-68c3-4bd2-b99e-0538a08de10f"),
                MetadataId = new Guid("b1b66940-68c3-4bd2-b99e-0538a08de10f"),
                Name = "AlwaysEnabled",
                Description = "A rule type that always returns a state of On when used by a feature flag configuration. The rule is second highest in precedence behind the Always Disabled rule.",
                ApplicationName = _featureFlags,
                TypeId = _ruleTypeTypeId,
                TypeName = _ruleType
            },
            new()
            {
                Id = new Guid("26d17b10-dc3a-4e74-97b1-e19eec79f6a0"),
                MetadataId = new Guid("26d17b10-dc3a-4e74-97b1-e19eec79f6a0"),
                Name = "AlwaysDisabled",
                Description = "A rule type that always returns a state of Off when used by a feature flag configuration. The rule is the highest in precedence.",
                ApplicationName = _featureFlags,
                TypeId = _ruleTypeTypeId,
                TypeName = _ruleType
            },
            new()
            {
                Id = new Guid("63adc07a-3793-4b9c-ab49-7ad3e805d3ef"),
                MetadataId = new Guid("63adc07a-3793-4b9c-ab49-7ad3e805d3ef"),
                Name = "DateWindow",
                Description = "A rule type that evaluates based upon a date range parameter in the feature flag configuration.",
                ApplicationName = _featureFlags,
                TypeId = _ruleTypeTypeId,
                TypeName = _ruleType
            },
            new()
            {
                Id = new Guid("e51fbda9-e0c4-4bca-8c91-d060a537c743"),
                MetadataId = new Guid("e51fbda9-e0c4-4bca-8c91-d060a537c743"),
                Name = "ApplicationRole",
                Description = "A rule type that evaluates based upon the application role list parameter in the feature flag configuration.",
                ApplicationName = _featureFlags,
                TypeId = _ruleTypeTypeId,
                TypeName = _ruleType
            },
            new()
            {
                Id = new Guid("f84643ae-4405-440f-8f5a-a167ad90e97a"),
                MetadataId = new Guid("f84643ae-4405-440f-8f5a-a167ad90e97a"),
                Name = "ApplicationUser",
                Description = "A rule type that evaluates based upon the application user list parameter in the feature flag configuration.",
                ApplicationName = _featureFlags,
                TypeId = _ruleTypeTypeId,
                TypeName = _ruleType
            },
            new()
            {
                Id = new Guid("b51e6e73-7a56-4547-9cb4-3287992cca0c"),
                MetadataId = new Guid("b51e6e73-7a56-4547-9cb4-3287992cca0c"),
                Name = "TimeWindow",
                Description = "A rule type that evaluates based upon a time range parameter in the feature flag configuration. The time is evaluated each day within the provided date range parameters.",
                ApplicationName = _featureFlags,
                TypeId = _ruleTypeTypeId,
                TypeName = _ruleType
            },
            new()
            {
                Id = new Guid("a5990a95-13fe-4333-8ad6-afa10b94f98a"),
                MetadataId = new Guid("a5990a95-13fe-4333-8ad6-afa10b94f98a"),
                Name = "DayOfTheWeek",
                Description = "A rule type that evaluates based upon a day of the week bitwise parameter in the feature flag configuration. The configuration allows for one to all days of the week to be configured for the feature flag.",
                ApplicationName = _featureFlags,
                TypeId = _ruleTypeTypeId,
                TypeName = _ruleType
            }
        });

        MetadataDbContext.SaveChanges();
    }

    public override void ConcreteDispose()
    {
        _metadataConnection.Dispose();
        _primaryConnection.Dispose();
    } 
}
