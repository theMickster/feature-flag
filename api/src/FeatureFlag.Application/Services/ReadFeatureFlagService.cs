using AutoMapper;
using FeatureFlag.Application.Data;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models.FeatureFlag;
using Microsoft.EntityFrameworkCore;

namespace FeatureFlag.Application.Services;

[ServiceLifetimeScoped]
public sealed class ReadFeatureFlagService : IReadFeatureFlagService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<FeatureFlagDbContext> _dbContextFactory;

    public ReadFeatureFlagService(IMapper mapper, IDbContextFactory<FeatureFlagDbContext> dbContextFactory)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    /// <summary>
    /// Retrieve a feature flag using its unique identifier.
    /// </summary>
    /// <returns>A <see cref="FeatureFlagModel"/> </returns>
    public async Task<FeatureFlagModel?> GetByIdAsync(Guid featureFlagId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var entity = await context.FeatureFlags.FirstOrDefaultAsync(x => x.Id == featureFlagId);
        return entity == null ? null : _mapper.Map<FeatureFlagModel>(entity);
    }

    /// <summary>
    /// Retrieves a list of feature flags
    /// </summary>
    /// <param name="parameters">the input paging parameters</param>
    /// <returns>a <seealso cref="FeatureFlagSearchResultModel"/> object</returns>
    public async Task<FeatureFlagSearchResultModel> GetFeatureFlagsAsync(FeatureFlagParameter parameters)
    {
        var result = new FeatureFlagSearchResultModel
        {
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalRecords = 0
        };

        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var totalCount = await context.FeatureFlags.CountAsync();

        var query = context.FeatureFlags.AsQueryable();

        if (parameters.OrderBy == SortedResultConstants.Name)
        {
            query = parameters.SortOrder == SortedResultConstants.Ascending ?
                    query.OrderBy(x => x.Name) :
                    query.OrderByDescending(x => x.Name);
        }

        var entities = await query.ToListAsync();

        if (entities == null || !entities.Any())
        {
            return result;
        }

        var models = _mapper.Map<List<FeatureFlagModel>>(entities);
        result.Results = models;
        result.TotalRecords = totalCount;
        return result;
    }
}
