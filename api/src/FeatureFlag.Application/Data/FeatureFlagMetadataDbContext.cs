using FeatureFlag.Application.Interfaces.Data;
using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatureFlag.Application.Data;

public class FeatureFlagMetadataDbContext(DbContextOptions<FeatureFlagMetadataDbContext> options) 
    : DbContext(options), IFeatureFlagMetadataDbContext
{
    public DbSet<ApplicationEntity> Applications { get; set; }

    public DbSet<EnvironmentEntity> Environments { get; set; }

    public DbSet<RuleTypeEntity> RuleTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer(CosmosContainerConstants.MainContainer);

        var application = modelBuilder.Entity<ApplicationEntity>();
        ConfigureMetadataEntity(application);
        application.HasDiscriminator(x => x.TypeName).HasValue(PartitionKeyConstants.Application);

        var environment = modelBuilder.Entity<EnvironmentEntity>();
        ConfigureMetadataEntity(environment);
        environment.HasDiscriminator(x => x.TypeName).HasValue(PartitionKeyConstants.Environment);

        var ruleType = modelBuilder.Entity<RuleTypeEntity>();
        ConfigureMetadataEntity(ruleType);
        ruleType.HasDiscriminator(x => x.TypeName).HasValue(PartitionKeyConstants.RuleType);
    }

    private static void ConfigureMetadataEntity<T>(EntityTypeBuilder<T> entityTypeBuilder) where T : MetadataBaseEntity
    {
        entityTypeBuilder.Property(x => x.Id).ToJsonProperty("id");
        entityTypeBuilder.ToContainer(CosmosContainerConstants.MetadataContainer);
        entityTypeBuilder.HasPartitionKey(x => x.ApplicationName);
        entityTypeBuilder.HasPartitionKey(x => x.TypeId);
        entityTypeBuilder.HasKey(x => x.Id);
    }
}
