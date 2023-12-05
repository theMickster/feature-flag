using FeatureFlag.Domain.Models.RuleEvaluator;

namespace FeatureFlag.Application.Interfaces.Services.RuleEvaluator;

public interface IRule
{
    RuleResultTypeEnum Run();
}
