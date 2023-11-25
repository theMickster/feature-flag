namespace FeatureFlag.Domain.Models.FeatureFlagStatus;

public sealed class FeatureFlagStatusInputParams
{
    /// <summary>
    /// The unique identifier for a feature flag.
    /// </summary>
    public Guid FeatureFlagId { get; set; }

    /// <summary>
    /// The application to evaluate the feature flag's status
    /// </summary>
    public Guid ApplicationId { get; set; }

    /// <summary>
    /// The environment to evaluate the feature flag's status
    /// </summary>
    public Guid EnvironmentId { get; set; }
}
