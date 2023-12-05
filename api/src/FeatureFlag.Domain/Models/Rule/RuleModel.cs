using FeatureFlag.Domain.Models.RuleType;

namespace FeatureFlag.Domain.Models.Rule;

public sealed class RuleModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public bool AllowRule { get; set; }

    public int Priority { get; set; }

    public RuleTypeModel RuleType { get; set; } = new();

    public RuleParameterModel? Parameters { get; set; }
}
