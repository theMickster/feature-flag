using FeatureFlag.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlag.Application.Interfaces.Data;

public interface IFeatureFlagMetadataDbContext
{
    DbSet<ApplicationEntity> Applications { get; set; }

    DbSet<EnvironmentEntity> Environments { get; set; }

    DbSet<RuleTypeEntity> RuleTypes { get; set; }
}
