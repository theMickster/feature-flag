using FeatureFlag.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlag.Application.Interfaces.Data;

public interface IFeatureFlagDbContext
{
    DbSet<FeatureFlagConfigEntity> FeatureFlagConfigurations { get; set; }

    DbSet<FeatureFlagEntity> FeatureFlags { get; set; }
}