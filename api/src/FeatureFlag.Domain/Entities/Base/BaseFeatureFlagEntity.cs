namespace FeatureFlag.Domain.Entities.Base;

public abstract class BaseFeatureFlagEntity : BaseEntity
{
    public Guid FeatureFlagId { get; set; }

    public string Name { get; set; }

}
