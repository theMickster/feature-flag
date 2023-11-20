using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models.FeatureFlagConfig;

namespace FeatureFlag.Application.Interfaces.Services;

public interface IReadFeatureFlagConfigService
{
    /// <summary>
    /// Retrieve a feature flag configuration using its unique identifier.
    /// </summary>
    /// <returns>A <see cref="FeatureFlagConfigModel"/> </returns>
    Task<FeatureFlagConfigModel?> GetByIdAsync(Guid id, Guid featureFlagId);

    /// <summary>
    /// Retrieves a list of feature flag configurations
    /// </summary>
    /// <param name="parameters">the input paging parameters</param>
    /// <returns>a <seealso cref="FeatureFlagConfigSearchResultModel"/> object</returns>
    Task<FeatureFlagConfigSearchResultModel> GetFeatureFlagConfigsAsync(FeatureFlagConfigParameter parameters);
}
