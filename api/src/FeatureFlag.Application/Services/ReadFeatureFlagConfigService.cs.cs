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
    public Task<FeatureFlagConfigSearchResultModel> GetFeatureFlagConfigsAsync(FeatureFlagConfigParameter parameters)
    {
        throw new NotImplementedException();
    }
}
