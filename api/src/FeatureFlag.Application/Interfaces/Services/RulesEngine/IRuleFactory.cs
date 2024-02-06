using FeatureFlag.Application.Services.RulesEngine;

namespace FeatureFlag.Application.Interfaces.Services.RulesEngine;

public interface IRuleFactory
{
    IReadOnlyList<IRule> BuildRules(RulesEngineInput input);
}
