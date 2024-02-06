using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public sealed class AlwaysEnabledRule(RuleInput ruleInput) : RuleBase(ruleInput)
{
    public override Guid RuleTypeId => RuleTypeConstants.AlwaysEnabledRuleId;
    
    public override RuleResultTypeEnum Run() => RuleResultTypeEnum.On;
}
