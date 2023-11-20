namespace FeatureFlag.Domain.Models.Rule;

public sealed class ApplicationRoleRuleModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
