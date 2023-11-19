using AutoMapper;
using FeatureFlag.Application.Interfaces.Data;
using FeatureFlag.Application.Interfaces.Services.Base;
using FeatureFlag.Common.Settings;
using FeatureFlag.Domain.Entities.Base;
using FeatureFlag.Domain.Models.Base;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FeatureFlag.Application.Services.Base;

public abstract class ReadMetadataBaseService<TModel, TEntity> : IReadMetadataBaseService<TModel> where TModel : MetadataBaseModel where TEntity : MetadataBaseEntity
{
    protected readonly IFeatureFlagMetadataDbContext MetadataDbContext;
    protected readonly IMemoryCache MemoryCache;
    protected readonly IMapper Mapper;
    protected readonly IOptionsSnapshot<CacheSettings> CacheSettings;

    protected ReadMetadataBaseService(
        string cacheKey,
        IMapper mapper,
        IFeatureFlagMetadataDbContext metadataDbContext,
        IMemoryCache memoryCache,
        IOptionsSnapshot<CacheSettings> cacheSettings)
    {
        CacheKey = cacheKey ?? throw new ArgumentNullException(nameof(cacheKey));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        MetadataDbContext = metadataDbContext ?? throw new ArgumentNullException(nameof(metadataDbContext));
        MemoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        CacheSettings = cacheSettings ?? throw new ArgumentNullException(nameof(cacheSettings));
    }

    public string CacheKey { get; }

    /// <summary>
    /// Retrieve a list of metadata models.
    /// </summary>
    /// <remarks>The cache will be checked prior to retrieving metadata from Cosmos Db.</remarks>
    /// <returns>List of type <typeparam name="TModel"></typeparam></returns>
    public IReadOnlyList<TModel> GetList()
    {
        MemoryCache.TryGetValue(CacheKey, out IReadOnlyList<TModel>? cachedData);

        if (cachedData is { Count: > 0 })
        {
            return cachedData;
        }

        var entities = GetEntities();
        cachedData = Mapper.Map<List<TModel>>(entities);
        MemoryCache.Set(CacheKey, cachedData, TimeSpan.FromSeconds(CacheSettings.Value.TimeoutInSeconds));

        return cachedData;
    }

    /// <summary>
    /// Retrieve a single metadata model by it's unique indecenter.
    /// </summary>
    /// <remarks>The cache will be checked prior to retrieving metadata from Cosmos Db.</remarks>
    /// <returns>A single <typeparam name="TModel"></typeparam></returns>
    public TModel? GetModelById(Guid id)
    {
        return GetList().SingleOrDefault(x => x.Id == id);
    }

    /// <summary>
    /// Retrieve a single metadata model by it's name.
    /// </summary>
    /// <remarks>The cache will be checked prior to retrieving metadata from Cosmos Db.</remarks>
    /// <returns>A single <typeparam name="TModel"></typeparam></returns>
    public TModel? GetModelByName(string name)
    {
        return GetList().FirstOrDefault(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
    }

    protected abstract IReadOnlyCollection<TEntity> GetEntities();
}
