using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Interfaces.Services.RuleEvaluator;

public interface IRuleFactory
{
    IReadOnlyList<IRule> BuildRules(RulesEngineInputModel input);
}
