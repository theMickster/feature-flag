using FeatureFlag.Application.Interfaces.Services.RuleEvaluator;
using FeatureFlag.Domain.Models.RuleEvaluator;

namespace FeatureFlag.Application.Services.RuleEvaluator;

public sealed class RuleFactory : IRuleFactory
{
    public IReadOnlyList<IRule> BuildRules(RuleEvaluatorInputModel input)
    {
        throw new NotImplementedException();
    }
}
