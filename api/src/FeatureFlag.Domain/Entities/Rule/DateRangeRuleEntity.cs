namespace FeatureFlag.Domain.Entities.Rule;

public sealed class DateRangeRuleEntity
{
    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime EndDate { get; set; } = DateTime.UtcNow;
}
