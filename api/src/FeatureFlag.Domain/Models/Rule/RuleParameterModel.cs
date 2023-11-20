namespace FeatureFlag.Domain.Models.Rule;

public class RuleParameterModel
{
    public List<ApplicationRoleRuleModel>? Roles { get; set; }

    public TimeRangeRuleModel? TimeRange { get; set; }
}
