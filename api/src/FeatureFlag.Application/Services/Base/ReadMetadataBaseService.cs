using AutoMapper;
using FeatureFlag.Application.Data;
using FeatureFlag.Application.Interfaces.Services.Base;
using FeatureFlag.Common.Settings;
using FeatureFlag.Domain.Entities.Base;
using FeatureFlag.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FeatureFlag.Application.Services.Base;

public abstract class ReadMetadataBaseService<TModel, TEntity>(string cacheKey,
        IMapper mapper,
        IDbContextFactory<FeatureFlagMetadataDbContext> dbContextFactory,
        IMemoryCache memoryCache,
        IOptionsSnapshot<CacheSettings> cacheSettings)
    : IReadMetadataBaseService<TModel>
    where TModel : MetadataBaseModel
    where TEntity : MetadataBaseEntity
{
    protected readonly IDbContextFactory<FeatureFlagMetadataDbContext> DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    protected readonly IMemoryCache MemoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    protected readonly IMapper Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    protected readonly IOptionsSnapshot<CacheSettings> CacheSettings = cacheSettings ?? throw new ArgumentNullException(nameof(cacheSettings));

    public string CacheKey { get; } = cacheKey ?? throw new ArgumentNullException(nameof(cacheKey));

    /// <summary>
    /// Retrieve a list of metadata models.
    /// </summary>
    /// <remarks>The cache will be checked prior to retrieving metadata from Cosmos Db.</remarks>
    /// <returns>List of type <typeparam name="TModel"></typeparam></returns>
    public async Task<IReadOnlyList<TModel>> GetListAsync()
    {
        MemoryCache.TryGetValue(CacheKey, out IReadOnlyList<TModel>? cachedData);

        if (cachedData is { Count: > 0 })
        {
            return cachedData;
        }

        var entities = await GetEntitiesAsync();
        cachedData = Mapper.Map<List<TModel>>(entities);
        MemoryCache.Set(CacheKey, cachedData, TimeSpan.FromSeconds(CacheSettings.Value.TimeoutInSeconds));

        return cachedData;
    }

    /// <summary>
    /// Retrieve a single metadata model by it's unique indecenter.
    /// </summary>
    /// <remarks>The cache will be checked prior to retrieving metadata from Cosmos Db.</remarks>
    /// <returns>A single <typeparam name="TModel"></typeparam></returns>
    public async Task<TModel?> GetModelById(Guid id)
    {
        return (await GetListAsync()).SingleOrDefault(x => x.Id == id); 
    }

    /// <summary>
    /// Retrieve a single metadata model by it's name.
    /// </summary>
    /// <remarks>The cache will be checked prior to retrieving metadata from Cosmos Db.</remarks>
    /// <returns>A single <typeparam name="TModel"></typeparam></returns>
    public async Task<TModel?> GetModelByName(string name)
    {
        return (await GetListAsync()).FirstOrDefault(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    protected abstract Task<IReadOnlyCollection<TEntity>> GetEntitiesAsync();
}
