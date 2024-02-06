namespace FeatureFlag.Domain.Entities.Rule;

public sealed class RuleParameterEntity
{
    public List<ApplicationRoleRuleEntity>? Roles { get; set; }

    public DateRangeRuleEntity DateRange { get; set; }

    public TimeRangeRuleEntity? TimeRange { get; set; }

}
