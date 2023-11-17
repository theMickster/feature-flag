using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models.FeatureFlag;

namespace FeatureFlag.Application.Interfaces.Services;

public interface IReadFeatureFlagService
{
    /// <summary>
    /// Retrieve a feature flag using its unique identifier.
    /// </summary>
    /// <returns>A <see cref="FeatureFlagModel"/> </returns>
    Task<FeatureFlagModel?> GetByIdAsync(Guid featureFlagId);

    /// <summary>
    /// Retrieves a list of feature flags
    /// </summary>
    /// <param name="parameters">the input paging parameters</param>
    /// <returns>a <seealso cref="FeatureFlagSearchResultModel"/> object</returns>
    Task<FeatureFlagSearchResultModel> GetFeatureFlagsAsync(FeatureFlagParameter parameters);
}
