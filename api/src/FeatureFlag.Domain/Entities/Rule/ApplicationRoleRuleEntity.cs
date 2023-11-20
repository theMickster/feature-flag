namespace FeatureFlag.Domain.Entities.Rule;

public sealed class ApplicationRoleRuleEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
