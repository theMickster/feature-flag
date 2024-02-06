using AutoMapper;
using FeatureFlag.Application.Data;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Application.Services.Base;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Settings;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Models.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FeatureFlag.Application.Services;

[ServiceLifetimeScoped]
public sealed class ReadApplicationService(
    IMapper mapper,
    IDbContextFactory<FeatureFlagMetadataDbContext> dbContextFactory,
    IMemoryCache memoryCache,
    IOptionsSnapshot<CacheSettings> cacheSettings)
        : ReadMetadataBaseService<ApplicationModel, ApplicationEntity>(CacheKeyConstants.ApplicationList, mapper,
            dbContextFactory, memoryCache, cacheSettings), IReadApplicationService
{
    protected override async Task<IReadOnlyCollection<ApplicationEntity>> GetEntitiesAsync()
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();
        return (await context.Applications.ToListAsync()).AsReadOnly();
    }
}
