using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public sealed class AlwaysEnabledRule : RuleBase
{
    public AlwaysEnabledRule(
        RuleModel ruleModel,
        Guid applicationUserId,
        List<Guid>? applicationRoles)
        : base(ruleModel, applicationUserId, applicationRoles) {}

    public override Guid RuleTypeId => RuleTypeConstants.AlwaysEnabledRuleId;
    
    public override RuleResultTypeEnum Run() => RuleResultTypeEnum.On;
}
