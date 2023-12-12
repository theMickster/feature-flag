using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Interfaces.Services.RuleEvaluator;

public interface IRulesEngineService
{
    /// <summary>
    /// Execute the feature flag rule evaluation for the given inputs.
    /// </summary>
    /// <param name="input">the <see cref="RulesEngineInputModel"/> input for the evaluator's execution.</param>
    /// <returns>the <see cref="RulesEngineOutcomeModel"/> output for the evaluator 's execution.</returns>
    RulesEngineOutcomeModel Run(RulesEngineInputModel input);
}