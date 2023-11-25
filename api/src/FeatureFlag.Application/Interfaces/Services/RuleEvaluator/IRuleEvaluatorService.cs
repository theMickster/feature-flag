using FeatureFlag.Domain.Models.RuleEvaluator;

namespace FeatureFlag.Application.Interfaces.Services.RuleEvaluator;

public interface IRuleEvaluatorService
{
    /// <summary>
    /// Execute the feature flag rule evaluation for the given inputs.
    /// </summary>
    /// <param name="input">the <see cref="RuleEvaluatorInputModel"/> input for the evaluator's execution.</param>
    /// <returns>the <see cref="RuleEvaluatorOutcomeModel"/> output for the evaluator 's execution.</returns>
    RuleEvaluatorOutcomeModel Run(RuleEvaluatorInputModel input);
}