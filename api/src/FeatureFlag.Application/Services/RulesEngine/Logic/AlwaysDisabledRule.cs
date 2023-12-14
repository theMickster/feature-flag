using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public sealed class AlwaysDisabledRule(RuleInput ruleInput) : RuleBase(ruleInput)
{
    public override Guid RuleTypeId => RuleTypeConstants.AlwaysDisabledRuleId;
    
    public override RuleResultTypeEnum Run() => RuleResultTypeEnum.Off;
}
