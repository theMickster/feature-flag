using FeatureFlag.Domain.Models.FeatureFlagStatus;

namespace FeatureFlag.Application.Interfaces.Services;

public interface IReadFeatureFlagStatus
{
    /// <summary>
    /// Retrieve the feature flag's status based upon the context of the user inputs.
    /// </summary>
    /// <param name="inputParams"></param>
    /// <returns></returns>
    Task<FeatureFlagStatusModel> GetFeatureFlagStatusAsync(FeatureFlagStatusInputParams inputParams);
}
