﻿using AutoMapper;
using FeatureFlag.Application.Data;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Application.Services.Base;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Settings;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Models.RuleType;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FeatureFlag.Application.Services;

[ServiceLifetimeScoped]
public class ReadRuleTypeService : ReadMetadataBaseService<RuleTypeModel, RuleTypeEntity>, IReadRuleTypeService
{
    [SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "Cannot be done here.")]
    public ReadRuleTypeService(
        IMapper mapper,
        IDbContextFactory<FeatureFlagMetadataDbContext> dbContextFactory,
        IMemoryCache memoryCache,
        IOptionsSnapshot<CacheSettings> cacheSettings) : base
        (CacheKeyConstants.ApplicationList, mapper, dbContextFactory, memoryCache, cacheSettings)
    {
    }

    protected override async Task<IReadOnlyCollection<RuleTypeEntity>> GetEntitiesAsync()
    {
        await using var context = await DbContextFactory.CreateDbContextAsync();
        return (await context.RuleTypes.ToListAsync()).AsReadOnly();
    }
}
