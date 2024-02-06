using FeatureFlag.Application.Services.RulesEngine;

namespace FeatureFlag.Application.Interfaces.Services.RulesEngine;

public interface IRulesEngineService
{
    /// <summary>
    /// Execute the feature flag rule evaluation for the given inputs.
    /// </summary>
    /// <param name="input">the <see cref="RulesEngineInput"/> input for the evaluator's execution.</param>
    /// <returns>the <see cref="RulesEngineOutcome"/> output for the evaluator 's execution.</returns>
    RulesEngineOutcome Run(RulesEngineInput input);
}