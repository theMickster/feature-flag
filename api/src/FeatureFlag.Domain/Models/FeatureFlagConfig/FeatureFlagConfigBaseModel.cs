namespace FeatureFlag.Domain.Models.FeatureFlagConfig;

public abstract class FeatureFlagConfigBaseModel
{
    public Guid FeatureFlagId { get; set; }

    public string Name { get; set; } = string.Empty;
}
