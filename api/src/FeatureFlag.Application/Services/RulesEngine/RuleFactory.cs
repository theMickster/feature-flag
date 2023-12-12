using FeatureFlag.Application.Interfaces.Services.RuleEvaluator;
using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Services.RulesEngine;

public sealed class RuleFactory : IRuleFactory
{
    public IReadOnlyList<IRule> BuildRules(RulesEngineInputModel input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.Rules! == null || input.Rules.Count == 0)
        {
            throw new ArgumentNullException(nameof(input), "The input rules list cannot be null or empty.");
        }

        return input.Rules.Select(x => CreateRule(x, input)).ToList();
    }

    private static readonly Dictionary<Guid, Func<RuleModel, RulesEngineInputModel, IRule>> RuleFactories =
        new()
        {
            { RuleTypeConstants.AlwaysDisabledRuleId, (ruleModel, input) => new AlwaysDisabledRule(ruleModel, input.ApplicationUserId, []) },
            { RuleTypeConstants.AlwaysEnabledRuleId, (ruleModel, input) => new AlwaysEnabledRule(ruleModel, input.ApplicationUserId, []) },
            { RuleTypeConstants.DateWindowRuleId, (ruleModel, input) => new DateWindowRule(ruleModel, input.ApplicationUserId, [], input.EvaluationDate) },
            { RuleTypeConstants.TimeWindowRuleId, (ruleModel, input) => new TimeWindowRule(ruleModel, input.ApplicationUserId, [], TimeOnly.FromDateTime(input.EvaluationDate)) }
        };

    private static IRule CreateRule(RuleModel ruleModel, RulesEngineInputModel input)
    {
        if (ruleModel?.RuleType == null)
        {
            throw new ArgumentNullException(nameof(ruleModel), "The input rule model cannot be null, nor can the rule type be null.");
        }
        
        if (!RuleFactories.TryGetValue(ruleModel.RuleType.Id, out var ruleFactory))
        {
            throw new NotImplementedException($"Rule type '{ruleModel.RuleType.Name}' is not supported.");
        }
        return ruleFactory(ruleModel, input);
    }

    private static IRule CreateRuleViaSwitch(RuleModel ruleModel, RulesEngineInputModel input)
    {
        if (ruleModel?.RuleType == null)
        {
            throw new ArgumentNullException(nameof(ruleModel), "The input rule model cannot be null, nor can the rule type be null.");
        }

        return ruleModel.RuleType.Id switch
        {
            var r when r == RuleTypeConstants.AlwaysDisabledRuleId => 
                new AlwaysDisabledRule(ruleModel, Guid.Empty, []),
            var r when r == RuleTypeConstants.AlwaysEnabledRuleId => 
                new AlwaysEnabledRule(ruleModel, Guid.Empty, []),
            var r when r == RuleTypeConstants.DateWindowRuleId => 
                new DateWindowRule(ruleModel, Guid.Empty, [],input.EvaluationDate),
            var r when r == RuleTypeConstants.TimeWindowRuleId => 
                new TimeWindowRule(ruleModel, Guid.Empty, [], TimeOnly.FromDateTime(input.EvaluationDate)),
            _ => throw new NotImplementedException($"Rule type '{ruleModel.RuleType.Name}' is not supported.")
        };
    }
}
