using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public sealed class AppUserRule : RuleBase
{
    public AppUserRule(RuleModel ruleModel, Guid applicationUserId, List<Guid>? applicationRoles)
        : base(ruleModel, applicationUserId, applicationRoles)
    {
    }

    public override Guid RuleTypeId => RuleTypeConstants.ApplicationUserRuleId;

    public override RuleResultTypeEnum Run()
    {
        return RuleResultTypeEnum.NotApplicable;
    }
}
