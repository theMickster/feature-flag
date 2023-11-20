using AutoMapper;
using FeatureFlag.Application.Data;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Application.Services.Base;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Settings;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Models.Environment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FeatureFlag.Application.Services;

[ServiceLifetimeScoped]
public class ReadEnvironmentService : ReadMetadataBaseService<EnvironmentModel, EnvironmentEntity>, IReadEnvironmentService
{
    [SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "Cannot be done here.")]
    public ReadEnvironmentService(
        IMapper mapper,
        IDbContextFactory<FeatureFlagMetadataDbContext> dbContextFactory,
        IMemoryCache memoryCache,
        IOptionsSnapshot<CacheSettings> cacheSettings) : base
        (CacheKeyConstants.ApplicationList, mapper, dbContextFactory, memoryCache, cacheSettings)
    {
    }

    protected override async Task<IReadOnlyCollection<EnvironmentEntity>> GetEntitiesAsync()
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();
        return (await context.Environments.ToListAsync()).AsReadOnly();
    }
}
