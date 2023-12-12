using FeatureFlag.Application.Interfaces.Services.RuleEvaluator;
using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Services.RulesEngine;

[ServiceLifetimeScoped]
public sealed class RulesEngineService : IRulesEngineService
{
    private readonly IRuleFactory _ruleFactory;

    public RulesEngineService(IRuleFactory ruleFactory)
    {
        _ruleFactory = ruleFactory ?? throw new ArgumentNullException(nameof(ruleFactory));
    }
    
    /// <summary>
    /// Execute the feature flag rule evaluation for the given inputs.
    /// </summary>
    /// <param name="input">the <see cref="RulesEngineInputModel"/> input for the evaluator's execution.</param>
    /// <returns>the <see cref="RulesEngineOutcomeModel"/> output for the evaluator 's execution.</returns>
    public RulesEngineOutcomeModel Run(RulesEngineInputModel input)
    {
        var rules = _ruleFactory.BuildRules(input);
        var finalOutcome = new RulesEngineOutcomeModel{Outcome = RuleResultTypeEnum.Off};

        if (RuleSetHasAlwaysDisabledRule(rules))
        {
            finalOutcome.Outcome = RuleResultTypeEnum.Off;
            return finalOutcome;
        }

        if (RuleSetHasAlwaysEnabledRule(rules))
        {
            finalOutcome.Outcome = RuleResultTypeEnum.On;
            return finalOutcome;
        }

        foreach (var rule in rules)
        {
            finalOutcome.Outcome = rule.Run();
        }

        return finalOutcome;
    }

    public static bool RuleSetHasAlwaysEnabledRule(IReadOnlyList<IRule> ruleSet)
    {
        return ruleSet.OfType<AlwaysEnabledRule>().ToList().Count != 0;
    }

    public static bool RuleSetHasAlwaysDisabledRule(IReadOnlyList<IRule> ruleSet)
    {
        return ruleSet.OfType<AlwaysDisabledRule>().ToList().Count != 0;
    }
}
