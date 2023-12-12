using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public sealed class TimeWindowRule : RuleBase
{
    private readonly TimeOnly _evaluationTime;

    public TimeWindowRule(
        RuleModel ruleModel, 
        Guid applicationUserId, 
        List<Guid>? applicationRoles,
        TimeOnly evaluationTime)
        : base(ruleModel, applicationUserId, applicationRoles)
    {
        _evaluationTime = evaluationTime;
        if (ruleModel.Parameters?.TimeRange == null)
        {
            throw new ArgumentNullException(nameof(ruleModel),
                $"The input parameters ${nameof(ruleModel.Parameters)} or ${nameof(ruleModel.Parameters.TimeRange)}" +
                $"to create the Time Window Rule are unknown.");
        }
    }

    public override Guid RuleTypeId => RuleTypeConstants.TimeWindowRuleId;

    public override RuleResultTypeEnum Run()
    {
        var timeRange = RuleModel.Parameters?.TimeRange ?? throw new ArgumentNullException("TimeRange");
        var result = _evaluationTime.IsBetween(timeRange.StartTime, timeRange.EndTime);

        if (RuleModel.AllowRule == false)
        {
            return result ? RuleResultTypeEnum.Off : RuleResultTypeEnum.NotApplicable;
        }

        return result ? RuleResultTypeEnum.On : RuleResultTypeEnum.Off;
    }
}
