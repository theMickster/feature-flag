using AutoMapper;
using FeatureFlag.Application.Data;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models.FeatureFlagConfig;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FeatureFlag.Application.Services;

[ServiceLifetimeScoped]
public class ReadFeatureFlagConfigService(IMapper mapper, IDbContextFactory<FeatureFlagDbContext> dbContextFactory)
    : IReadFeatureFlagConfigService
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IDbContextFactory<FeatureFlagDbContext> _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));

    /// <summary>
    /// Retrieve a feature flag configuration using its unique identifier.
    /// </summary>
    /// <returns>A <see cref="FeatureFlagConfigModel"/> </returns>
    public async Task<FeatureFlagConfigModel?> GetByIdAsync(Guid id, Guid featureFlagId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var entity = await context.FeatureFlagConfigurations.FirstOrDefaultAsync(x => x.Id == id && x.FeatureFlagId == featureFlagId);
        return entity == null ? null : _mapper.Map<FeatureFlagConfigModel>(entity);
    }

    /// <summary>
    /// Retrieves a list of feature flag configurations
    /// </summary>
    /// <param name="parameters">the input paging parameters</param>
    /// <returns>a <seealso cref="FeatureFlagConfigSearchResultModel"/> object</returns>
    public async Task<FeatureFlagConfigSearchResultModel> GetFeatureFlagConfigsAsync(FeatureFlagConfigParameter parameters)
    {
        var result = new FeatureFlagConfigSearchResultModel
        {
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalRecords = 0
        };

        var queryBuilder = new StringBuilder();
        queryBuilder.Append($"SELECT * FROM c WHERE c.EntityType = 'FeatureFlagConfig' AND c.FeatureFlagId = '{parameters.FeatureFlagId}'");
        
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var entities =  await context.FeatureFlagConfigurations.Where(x => x.FeatureFlagId == parameters.FeatureFlagId).ToListAsync();

        if (parameters.ApplicationId != Guid.Empty)
        {
            queryBuilder.Append($" AND EXISTS(SELECT VALUE x FROM x IN c.Applications WHERE x.MetadataId = '{parameters.ApplicationId}')");
            entities = entities.Where(x => x.Applications.Any(y => y.MetadataId == parameters.ApplicationId)).ToList();
        }

        if (parameters.EnvironmentId != Guid.Empty)
        {
            queryBuilder.Append($" AND EXISTS(SELECT VALUE y FROM y IN c.Environments WHERE y.MetadataId = '{parameters.EnvironmentId}')");
            entities = entities.Where(x => x.Environments.Any(y => y.MetadataId == parameters.EnvironmentId)).ToList();
        }
        
        /* TODO :: Keeping this around for a short while because we might need to swap to raw CosmosDb formatted SQL. */
        //var entitiesRawSql = await context.FeatureFlagConfigurations.FromSqlRaw(queryBuilder.ToString()).ToListAsync();
        
        if (entities is { Count: 0 })
        {
            return result;
        }

        var models = _mapper.Map<List<FeatureFlagConfigModel>>(entities);
        result.Results = models;
        result.TotalRecords = entities.Count;
        return result;
    }
}
