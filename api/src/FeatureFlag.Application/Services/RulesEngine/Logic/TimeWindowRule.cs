using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public sealed class TimeWindowRule : RuleBase
{
    public TimeWindowRule(RuleInput ruleInput) : base(ruleInput)
    {
        if (ruleInput.Rule.Parameters?.TimeRange == null)
        {
            throw new ArgumentNullException(nameof(ruleInput.Rule.Parameters.TimeRange),
                $"The input parameters ${nameof(ruleInput.Rule.Parameters)} or ${nameof(ruleInput.Rule.Parameters.TimeRange)}" +
                $"to create the Time Window Rule are unknown.");
        }
    }

    public override Guid RuleTypeId => RuleTypeConstants.TimeWindowRuleId;

    public override RuleResultTypeEnum Run()
    {
        var result = EvaluationTime.IsBetween(RuleModel.Parameters!.TimeRange!.StartTime, RuleModel.Parameters!.TimeRange!.EndTime);

        if (RuleModel.AllowRule == false)
        {
            return result ? RuleResultTypeEnum.Off : RuleResultTypeEnum.NotApplicable;
        }

        return result ? RuleResultTypeEnum.On : RuleResultTypeEnum.Off;
    }
}
