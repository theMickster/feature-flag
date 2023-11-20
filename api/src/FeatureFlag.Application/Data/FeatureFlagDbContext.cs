using FeatureFlag.Application.Interfaces.Data;
using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;

namespace FeatureFlag.Application.Data;

public class FeatureFlagDbContext : DbContext, IFeatureFlagDbContext
{
    public FeatureFlagDbContext(DbContextOptions<FeatureFlagDbContext> options) : base(options)
    {
    }

    public DbSet<FeatureFlagConfigEntity> FeatureFlagConfigurations { get; set; }

    public DbSet<FeatureFlagEntity> FeatureFlags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging().EnableDetailedErrors();
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer(CosmosContainerConstants.MainContainer);

        var featureFlagBuilder = modelBuilder.Entity<FeatureFlagEntity>();
        ConfigureFeatureFlagEntity(featureFlagBuilder);
        featureFlagBuilder.HasDiscriminator(x => x.EntityType).HasValue(PartitionKeyConstants.FeatureFlag);
        
        var featureFlagConfigBuilder = modelBuilder.Entity<FeatureFlagConfigEntity>();
        ConfigureFeatureFlagEntity(featureFlagConfigBuilder);
        featureFlagConfigBuilder.HasDiscriminator(x => x.EntityType).HasValue(PartitionKeyConstants.FeatureFlagConfig);
        featureFlagConfigBuilder.OwnsMany(x => x.Applications);
        featureFlagConfigBuilder.OwnsMany(x => x.Environments);
        featureFlagConfigBuilder.OwnsMany(x => x.Rules);
    }

    private static void ConfigureFeatureFlagEntity<T>(EntityTypeBuilder<T> entityTypeBuilder) where T : FeatureFlagBaseEntity
    {
        entityTypeBuilder.Property(x => x.Id).ToJsonProperty("id");
        entityTypeBuilder.ToContainer(CosmosContainerConstants.MainContainer);
        entityTypeBuilder.HasPartitionKey(x => x.FeatureFlagId);
        entityTypeBuilder.HasKey(x => x.Id);
    }

    
}
