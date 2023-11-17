namespace FeatureFlag.Domain.Models;

public sealed class FeatureFlagStatusModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
