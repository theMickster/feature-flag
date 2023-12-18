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
                ApplicationName = "FeatureFlags",
                Name = "Beers",
                TypeId = new Guid("d28be6d8-b764-47f1-9ff7-0947cba39168"),
                TypeName = "Application"
            },
            new()
            {
                Id = new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444"),
                MetadataId = new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444"),
                ApplicationName = "FeatureFlags",
                Name = "Adventure Works",
                TypeId = new Guid("d28be6d8-b764-47f1-9ff7-0947cba39168"),
                TypeName = "Application"
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
