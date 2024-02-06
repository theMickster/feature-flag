namespace FeatureFlag.Domain.Models.Rule;

public sealed class RuleParameterModel
{
    public List<ApplicationRoleRuleModel>? Roles { get; set; }

    public DateRangeRuleModel? DateRange { get; set; }

    public TimeRangeRuleModel? TimeRange { get; set; }
}
