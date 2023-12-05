using AutoMapper;
using FeatureFlag.Application.Data;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models.FeatureFlagConfig;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlag.Application.Services;

[ServiceLifetimeScoped]
public class ReadFeatureFlagConfigService : IReadFeatureFlagConfigService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<FeatureFlagDbContext> _dbContextFactory;

    public ReadFeatureFlagConfigService(IMapper mapper, IDbContextFactory<FeatureFlagDbContext> dbContextFactory)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

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

        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var totalCount = await context.FeatureFlagConfigurations.CountAsync(x => x.FeatureFlagId == parameters.FeatureFlagId);

        var query = context.FeatureFlagConfigurations.AsQueryable();
        query = query.Where(x => x.FeatureFlagId == parameters.FeatureFlagId);
        var entities = await query.ToListAsync();

        if (entities == null || !entities.Any())
        {
            return result;
        }

        var models = _mapper.Map<List<FeatureFlagConfigModel>>(entities);
        result.Results = models;
        result.TotalRecords = totalCount;
        return result;
    }
}
