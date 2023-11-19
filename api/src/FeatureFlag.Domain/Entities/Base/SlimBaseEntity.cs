namespace FeatureFlag.Domain.Entities.Base;

public abstract class SlimBaseEntity
{
    public Guid MetadataId { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;
}
