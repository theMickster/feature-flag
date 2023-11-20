using FeatureFlag.Domain.Entities.Slim;

namespace FeatureFlag.Domain.Entities.Rule;

public sealed class RuleEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public bool AllowRule { get; set; }

    public int Priority { get; set; }

    public RuleTypeSlimEntity RuleType { get; set; } = new();

    public RuleParameterEntity? Parameters { get; set; }
}
