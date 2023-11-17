namespace FeatureFlag.Domain.Models.FeatureFlag;

public abstract class FeatureFlagBaseModel
{
    public string Name { get; set; } = string.Empty;
    
    public string DisplayName { get; set; } = string.Empty;

}