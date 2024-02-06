namespace FeatureFlag.Domain.Models.FeatureFlagStatus;

public sealed class FeatureFlagStatusModel
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public Guid EnvironmentId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
