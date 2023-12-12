using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Interfaces.Services.RuleEvaluator;

public interface IRule
{
    RuleResultTypeEnum Run();
}
