using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public class AlwaysDisabledRule : RuleBase
{
    public AlwaysDisabledRule(
        RuleModel ruleModel,
        Guid applicationUserId,
        List<Guid>? applicationRoles)
        : base(ruleModel, applicationUserId, applicationRoles) { }

    public override Guid RuleTypeId => RuleTypeConstants.AlwaysDisabledRuleId;
    
    public override RuleResultTypeEnum Run() => RuleResultTypeEnum.Off;
}
