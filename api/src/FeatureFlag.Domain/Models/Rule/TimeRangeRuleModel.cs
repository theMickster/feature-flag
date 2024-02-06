namespace FeatureFlag.Domain.Models.Rule;

public sealed class TimeRangeRuleModel
{
    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
