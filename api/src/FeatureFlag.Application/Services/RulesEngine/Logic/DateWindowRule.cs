using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Utilities.Extensions;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

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

    public override Guid RuleTypeId => RuleTypeConstants.DateWindowRuleId;

    public override RuleResultTypeEnum Run()
    {
        var result = _evaluationDate.IsDateInDateRange(RuleModel.Parameters!.DateRange!.StartDate, RuleModel.Parameters!.DateRange!.EndDate);
        
        if (RuleModel.AllowRule == false)
        {
            return result ? RuleResultTypeEnum.Off : RuleResultTypeEnum.NotApplicable;
        }
        
        return result ? RuleResultTypeEnum.On : RuleResultTypeEnum.Off;
    }
}
