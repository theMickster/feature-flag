namespace FeatureFlag.Domain.Entities.Rule;

public sealed class TimeRangeRuleEntity
{
    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime EndDate { get; set; } = DateTime.UtcNow;
}
