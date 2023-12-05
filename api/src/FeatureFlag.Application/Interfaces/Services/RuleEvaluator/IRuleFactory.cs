using FeatureFlag.Domain.Models.RuleEvaluator;

namespace FeatureFlag.Application.Interfaces.Services.RuleEvaluator;

public interface IRuleFactory
{
    IReadOnlyList<IRule> BuildRules(RuleEvaluatorInputModel input);
}
