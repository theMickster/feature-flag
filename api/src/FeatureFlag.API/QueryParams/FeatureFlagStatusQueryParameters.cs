using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FeatureFlag.API.QueryParams;

/// <summary>
/// The object used to accept parameters used to calculate a Feature Flag's status.
/// </summary>
public sealed class FeatureFlagStatusQueryParameters
{
    /// <summary>
    /// The unique identifier for a feature flag.
    /// </summary>
    [BindRequired]
    public Guid FeatureFlagId { get; set; }

    /// <summary>
    /// The application to evaluate the feature flag's status
    /// </summary>
    [BindRequired]
    public Guid ApplicationId { get; set; }

    /// <summary>
    /// The environment to evaluate the feature flag's status
    /// </summary>
    [BindRequired]
    public Guid EnvironmentId { get; set; }

    /// <summary>
    /// The user's local timezone offset. 
    /// </summary>
    public decimal? TimeZoneOffset { get; set; }

}
