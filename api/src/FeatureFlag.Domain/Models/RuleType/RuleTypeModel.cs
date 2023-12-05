using FeatureFlag.Domain.Models.Base;

namespace FeatureFlag.Domain.Models.RuleType;

public class RuleTypeModel : MetadataBaseModel
{
    public string Description { get; set; } = string.Empty;
}