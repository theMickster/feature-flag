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

    /// <summary>
    /// The user's local timezone offset. 
    /// </summary>
    public decimal? TimeZoneOffset { get; set; }
    
    /// <summary>
    /// The current date and time in UTC format.
    /// </summary>
    public DateTime CurrentUtcDate { get; set; }

    /// <summary>
    /// The current date and time in the user's local timezone.
    /// </summary>
    public DateTime LocalDate => this.TimeZoneOffset != null ? CurrentUtcDate.AddHours((double)TimeZoneOffset) : CurrentUtcDate;
}
