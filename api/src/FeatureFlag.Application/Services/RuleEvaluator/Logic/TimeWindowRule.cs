using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RuleEvaluator;

namespace FeatureFlag.Application.Services.RuleEvaluator.Logic;

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

    public override RuleResultTypeEnum Run()
    {
        if (RuleModel.AllowRule == false)
        {
            return IsWithinTimeWindow(RuleModel.Parameters?.TimeRange!, _evaluationTime) ? RuleResultTypeEnum.Off : RuleResultTypeEnum.NotApplicable;
        }

        return IsWithinTimeWindow(RuleModel.Parameters?.TimeRange!, _evaluationTime) ? RuleResultTypeEnum.On : RuleResultTypeEnum.Off;
    }

    /// <summary>
    /// Determines whether an evaluation time falls within a given TimeOnly Time Range Model
    /// </summary>
    /// <param name="range"></param>
    /// <param name="timeToEvaluate"></param>
    /// <returns></returns>
    public static bool IsWithinTimeWindow(TimeRangeRuleModel range, TimeOnly timeToEvaluate)
    {
        return range.StartTime.CompareTo(timeToEvaluate) >= 0 && range.EndTime.CompareTo(timeToEvaluate) < 0;
    }
}
