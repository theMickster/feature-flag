using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RuleEvaluator;

namespace FeatureFlag.Application.Services.RuleEvaluator.Logic;

public sealed class AppUserRule : RuleBase
{
    public AppUserRule(RuleModel ruleModel, Guid applicationUserId, List<Guid>? applicationRoles)
        : base(ruleModel, applicationUserId, applicationRoles)
    {
    }

    public override RuleResultTypeEnum Run()
    {
        return RuleResultTypeEnum.NotApplicable;
    }
}
