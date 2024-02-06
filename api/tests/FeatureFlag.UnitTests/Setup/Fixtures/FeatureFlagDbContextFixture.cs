using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Entities.Rule;
using FeatureFlag.Domain.Entities.Slim;

namespace FeatureFlag.UnitTests.Setup.Fixtures;

[ExcludeFromCodeCoverage]
public sealed class FeatureFlagDbContextFixture : IDisposable
{
    private static readonly string _createdByPeteTheCat = "PeteTheCat";
    private static readonly DateTime _createByDateTime001 = new(2023, 07, 20, 18, 30, 00);
    private static readonly string _featureFlagEntityType = "FeatureFlag";
    private static readonly string _featureFlagConfigEntityType = "FeatureFlagConfig";
    private static readonly string _featureFlagAuditEntityType = "FeatureFlagAudit";
    
    public void Dispose() { }

    /// <summary>
    /// Create a fake, queryable list of <see cref="FeatureFlagConfigEntity"/>s to be used in a test that mocks a DbContext.
    /// </summary>
    /// <returns>IQueryable<see cref="FeatureFlagConfigEntity"/></returns>
    public IQueryable<FeatureFlagConfigEntity> GetFeatureFlagConfigEntities() => new List<FeatureFlagConfigEntity>
    {
        new()
        {
            Id = new Guid("fd9c5f07-5ede-489a-a74f-c5b0c3ed390e"),
            FeatureFlagId = new Guid("e42d7535-00b2-4d0d-a280-cdbd4ed3ac32"),
            Name = "Test FeatureFlagConfigEntity 001",
            IsDeleted = false,
            Applications = new List<ApplicationSlimEntity>
                {
                new ()
                {
                    MetadataId = new Guid("37eb8f87-2b6f-4129-a574-335ab6602138"),
                    Name = "Beers"
                },
                new ()
                {
                    MetadataId = new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444"),
                    Name = "Adventure Works"
                },
                new ()
                {
                    MetadataId = new Guid("02648b40-a4ed-40fa-b2eb-d5a1edb2014f"),
                    Name = "Shawsky Records"
                },
                new ()
                {
                    MetadataId = new Guid("82b22b46-e2a7-4022-9004-411525cacac2"),
                    Name = "Just for Fun"
                },
                new()
                {
                    MetadataId = new Guid("1f1c97fe-b098-4028-9c67-ae1bc0d867a4"),
                    Name = "Pubs"
                },
                new ()
                {
                    MetadataId = new Guid("7730ee3d-d5d1-4424-bae7-0fdc133bb605"),
                    Name = "Wide World Importers"
                }
            },
            Environments = new List<EnvironmentSlimEntity>
            {
                new()
                {
                    MetadataId = new Guid("b121e69e-6f82-4504-a1d9-4952e1d0f9f5"),
                    Name = "Production"
                },
                new()
                {
                    MetadataId = new Guid("ea5d9785-0d42-4373-b0ab-b34762b96037"),
                    Name = "Staging"
                },
                new()
                {
                    MetadataId = new Guid("153a4ccc-29ef-4f5f-a8f2-c6ab39f889d0"),
                    Name = "QA"
                },
                new()
                {
                    MetadataId = new Guid("587fd994-048f-486e-b9c0-288b0edd838e"),
                    Name = "UAT"
                },
                new()
                {
                    MetadataId = new Guid("9694016d-ade5-4e8c-9c0e-b5101e272ab9"),
                    Name = "Development"
                }
            },
            Rules = new List<RuleEntity>()
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    AllowRule = true,
                    Name = "Always Allow The First Feature",
                    DisplayName = "Hello World DisplayName Test FeatureFlagConfigEntity 001",
                    Priority = 1,
                    Parameters = null,
                    RuleType = new RuleTypeSlimEntity()
                    {
                        MetadataId = new Guid("b1b66940-68c3-4bd2-b99e-0538a08de10f"),
                        Name = "AlwaysEnabled"
                    }
                }
            },
            EntityType = _featureFlagConfigEntityType,
            CreatedBy = _createdByPeteTheCat,
            CreatedDate = _createByDateTime001,
            ModifiedBy = _createdByPeteTheCat,
            ModifiedDate = _createByDateTime001
        },

        new()
        {
            Id = new Guid("afac239a-df5a-41f6-82c1-94dec91e4d4d"),
            FeatureFlagId = new Guid("122561db-0fac-4375-a569-72c877010fd1"),
            Name = "Test FeatureFlagConfigEntity 002",
            IsDeleted = false,
            Applications = new List<ApplicationSlimEntity>
                {
                new ()
                {
                    MetadataId = new Guid("37eb8f87-2b6f-4129-a574-335ab6602138"),
                    Name = "Beers"
                },
                new ()
                {
                    MetadataId = new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444"),
                    Name = "Adventure Works"
                },
                new ()
                {
                    MetadataId = new Guid("02648b40-a4ed-40fa-b2eb-d5a1edb2014f"),
                    Name = "Shawsky Records"
                },
                new ()
                {
                    MetadataId = new Guid("82b22b46-e2a7-4022-9004-411525cacac2"),
                    Name = "Just for Fun"
                },
                new()
                {
                    MetadataId = new Guid("1f1c97fe-b098-4028-9c67-ae1bc0d867a4"),
                    Name = "Pubs"
                },
                new ()
                {
                    MetadataId = new Guid("7730ee3d-d5d1-4424-bae7-0fdc133bb605"),
                    Name = "Wide World Importers"
                }
            },
            Environments = new List<EnvironmentSlimEntity>
            {
                new()
                {
                    MetadataId = new Guid("b121e69e-6f82-4504-a1d9-4952e1d0f9f5"),
                    Name = "Production"
                }
            },
            Rules = new List<RuleEntity>()
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    AllowRule = false,
                    Name = "Always Block The Second Feature",
                    DisplayName = "Hello World DisplayName Test FeatureFlagConfigEntity 002",
                    Priority = 1,
                    Parameters = null,
                    RuleType = new RuleTypeSlimEntity()
                    {
                        MetadataId = new Guid("26d17b10-dc3a-4e74-97b1-e19eec79f6a0"),
                        Name = "AlwaysDisabled"
                    }
                }
            },
            EntityType = _featureFlagConfigEntityType,
            CreatedBy = _createdByPeteTheCat,
            CreatedDate = _createByDateTime001,
            ModifiedBy = _createdByPeteTheCat,
            ModifiedDate = _createByDateTime001
        },

        new()
        {
            Id = new Guid("525fc686-673b-4787-b34d-101548ca6af3"),
            FeatureFlagId = new Guid("5e1f9ed6-ddde-4d9e-8771-a613e2066430"),
            Name = "Test FeatureFlagConfigEntity 003",
            IsDeleted = false,
            Applications = new List<ApplicationSlimEntity>
            {
                new ()
                {
                    MetadataId = new Guid("02648b40-a4ed-40fa-b2eb-d5a1edb2014f"),
                    Name = "Shawsky Records"
                },
                new ()
                {
                    MetadataId = new Guid("82b22b46-e2a7-4022-9004-411525cacac2"),
                    Name = "Just for Fun"
                }
            },
            Environments = new List<EnvironmentSlimEntity>
            {
                new()
                {
                    MetadataId = new Guid("153a4ccc-29ef-4f5f-a8f2-c6ab39f889d0"),
                    Name = "QA"
                },
                new()
                {
                    MetadataId = new Guid("587fd994-048f-486e-b9c0-288b0edd838e"),
                    Name = "UAT"
                },
                new()
                {
                    MetadataId = new Guid("9694016d-ade5-4e8c-9c0e-b5101e272ab9"),
                    Name = "Development"
                }
            },
            Rules = new List<RuleEntity>()
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    AllowRule = false,
                    Name = "Allow the 3rd feature sometimes...",
                    DisplayName = "Hello World DisplayName Test FeatureFlagConfigEntity 003",
                    Priority = 1,
                    RuleType = new RuleTypeSlimEntity()
                    {
                        MetadataId = new Guid("63adc07a-3793-4b9c-ab49-7ad3e805d3ef"),
                        Name = "DateWindow"
                    },
                    Parameters = new RuleParameterEntity
                    {
                        DateRange = new DateRangeRuleEntity()
                        {
                            StartDate = new DateTime(2023, 10, 01),
                            EndDate = new DateTime(2023, 12, 31)
                        }
                    }
                }
            },
            EntityType = _featureFlagConfigEntityType,
            CreatedBy = _createdByPeteTheCat,
            CreatedDate = _createByDateTime001,
            ModifiedBy = _createdByPeteTheCat,
            ModifiedDate = _createByDateTime001
        }

    }.AsQueryable();

    /// <summary>
    /// Create a fake, queryable list of <see cref="FeatureFlagEntity"/>s to be used in a test that mocks a DbContext.
    /// </summary>
    /// <returns>IQueryable<see cref="FeatureFlagEntity"/></returns>
    public IQueryable<FeatureFlagEntity> GetFeatureFlagEntities() => new List<FeatureFlagEntity>
    {
        new()
        {
            Id = new Guid("e42d7535-00b2-4d0d-a280-cdbd4ed3ac32"),
            FeatureFlagId = new Guid("e42d7535-00b2-4d0d-a280-cdbd4ed3ac32"),
            Name = "Test FeatureFlag 001",
            DisplayName = "Display something neat for Test FeatureFlag 001",
            EntityType = _featureFlagEntityType
        },
        
        new()
        {
            Id = new Guid("122561db-0fac-4375-a569-72c877010fd1"),
            FeatureFlagId = new Guid("122561db-0fac-4375-a569-72c877010fd1"),
            Name = "Test FeatureFlag 002",
            DisplayName = "Display something neat for Test FeatureFlag 002",
            EntityType = _featureFlagEntityType
        },

        new()
        {
            Id = new Guid("5e1f9ed6-ddde-4d9e-8771-a613e2066430"),
            FeatureFlagId = new Guid("5e1f9ed6-ddde-4d9e-8771-a613e2066430"),
            Name = "Test FeatureFlag 003",
            DisplayName = "Display something neat for Test FeatureFlag 003",
            EntityType = _featureFlagEntityType
        }
    }.AsQueryable();
}

[ExcludeFromCodeCoverage]
[CollectionDefinition("FeatureFlag DbContext Collection")]
public class FeatureFlagDbContextCollection : ICollectionFixture<FeatureFlagDbContextFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}