using FeatureFlag.Application.Interfaces.Services.Base;
using FeatureFlag.Domain.Models.RuleType;

namespace FeatureFlag.Application.Interfaces.Services;

public interface IReadRuleTypeService : IReadMetadataBaseService<RuleTypeModel>
{
}
