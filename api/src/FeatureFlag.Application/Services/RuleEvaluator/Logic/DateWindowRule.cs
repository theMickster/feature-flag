using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RuleEvaluator;

namespace FeatureFlag.Application.Services.RuleEvaluator.Logic;

public sealed class DateWindowRule : RuleBase
{
    private readonly DateTime _evaluationDate;

    public DateWindowRule(
        RuleModel ruleModel, 
        Guid applicationUserId, 
        List<Guid>? applicationRoles,
        DateTime evaluationDate)
        : base(ruleModel, applicationUserId, applicationRoles)
    {
        if (ruleModel.Parameters?.DateRange == null)
        {
            throw new ArgumentNullException(nameof(ruleModel),
                $"The input parameters ${nameof(ruleModel.Parameters)} or ${nameof(ruleModel.Parameters.DateRange)}" +
                $"to create the Date Window Rule are unknown.");
        }

        _evaluationDate = evaluationDate;
    }
    
    public override RuleResultTypeEnum Run()
    {
        if (RuleModel.AllowRule == false)
        {
            return RuleModel.Parameters!.DateRange!.IsWithinDateRange(_evaluationDate) ? RuleResultTypeEnum.Off : RuleResultTypeEnum.NotApplicable;
        }

        return RuleModel.Parameters!.DateRange!.IsWithinDateRange(_evaluationDate) ? RuleResultTypeEnum.On : RuleResultTypeEnum.Off;
    }
}
