namespace FeatureFlag.Domain.Entities.Base;

public abstract class FeatureFlagBaseEntity : EntityBase
{
    public Guid FeatureFlagId { get; set; }

    public string Name { get; set; } = string.Empty;

}
