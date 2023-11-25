using FeatureFlag.Application.Interfaces.Services.RuleEvaluator;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Domain.Models.RuleEvaluator;

namespace FeatureFlag.Application.Services.RuleEvaluator;

[ServiceLifetimeScoped]
public sealed class RuleEvaluatorService : IRuleEvaluatorService
{
    /// <summary>
    /// Execute the feature flag rule evaluation for the given inputs.
    /// </summary>
    /// <param name="input">the <see cref="RuleEvaluatorInputModel"/> input for the evaluator's execution.</param>
    /// <returns>the <see cref="RuleEvaluatorOutcomeModel"/> output for the evaluator 's execution.</returns>
    public RuleEvaluatorOutcomeModel Run(RuleEvaluatorInputModel input)
    {
        var outcome = new RuleEvaluatorOutcomeModel();



        return outcome;
    }
}
