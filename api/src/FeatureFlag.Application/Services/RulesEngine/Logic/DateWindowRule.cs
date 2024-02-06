using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Utilities.Extensions;
using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public sealed class DateWindowRule : RuleBase
{
    public DateWindowRule(RuleInput ruleInput)
        : base(ruleInput)
    {
        if (ruleInput.Rule.Parameters?.DateRange == null)
        {
            throw new ArgumentNullException(nameof(ruleInput.Rule.Parameters.DateRange),
                $"The input parameters ${nameof(ruleInput.Rule.Parameters)} or ${nameof(ruleInput.Rule.Parameters.DateRange)}" +
                $"to create the Date Window Rule are unknown.");
        }
    }

    public override Guid RuleTypeId => RuleTypeConstants.DateWindowRuleId;

    public override RuleResultTypeEnum Run()
    {
        var result = EvaluationDate.IsDateInDateRange(RuleModel.Parameters!.DateRange!.StartDate, RuleModel.Parameters!.DateRange!.EndDate);
        
        if (RuleModel.AllowRule == false)
        {
            return result ? RuleResultTypeEnum.Off : RuleResultTypeEnum.NotApplicable;
        }
        
        return result ? RuleResultTypeEnum.On : RuleResultTypeEnum.Off;
    }
}
