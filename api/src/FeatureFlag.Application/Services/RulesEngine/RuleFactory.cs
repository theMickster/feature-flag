using FeatureFlag.Application.Interfaces.Services.RulesEngine;
using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Common.Constants;

namespace FeatureFlag.Application.Services.RulesEngine;

public sealed class RuleFactory : IRuleFactory
{
    public IReadOnlyList<IRule> BuildRules(RulesEngineInput input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.Rules! == null || input.Rules.Count == 0)
        {
            throw new ArgumentNullException(nameof(input.Rules), "The input rules list cannot be null or empty.");
        }

        return input.Rules.Select(x => CreateRule(
            new RuleInput
            {
                Rule = x,
                EvaluationDate = input.EvaluationDate,
                ApplicationUserId = input.ApplicationUserId,
                ApplicationUserRoles = input.ApplicationUserRoles
            })).ToList();
    }

    private static readonly Dictionary<Guid, Func<RuleInput, IRule>> RuleFactories =
        new()
        {
            { RuleTypeConstants.AlwaysDisabledRuleId, ruleInput => new AlwaysDisabledRule(ruleInput) },
            { RuleTypeConstants.AlwaysEnabledRuleId, ruleInput => new AlwaysEnabledRule(ruleInput) },
            { RuleTypeConstants.DateWindowRuleId, ruleInput => new DateWindowRule(ruleInput) },
            { RuleTypeConstants.TimeWindowRuleId, ruleInput => new TimeWindowRule(ruleInput) }
        };

    private static IRule CreateRule(RuleInput ruleInput)
    {
        if (ruleInput.Rule?.RuleType == null)
        {
            throw new ArgumentNullException(nameof(ruleInput), "The input rule model cannot be null, nor can the rule type be null.");
        }
        
        if (!RuleFactories.TryGetValue(ruleInput.Rule.RuleType.Id, out var ruleFactory))
        {
            throw new NotImplementedException($"Rule type '{ruleInput.Rule.RuleType.Name}' is not supported.");
        }

        return ruleFactory(ruleInput);
    }
}
