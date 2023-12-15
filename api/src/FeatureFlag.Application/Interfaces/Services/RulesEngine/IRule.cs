using FeatureFlag.Application.Services.RulesEngine;

namespace FeatureFlag.Application.Interfaces.Services.RulesEngine;

public interface IRule
{
    RuleResultTypeEnum Run();

    Guid RuleTypeId { get; }
}
