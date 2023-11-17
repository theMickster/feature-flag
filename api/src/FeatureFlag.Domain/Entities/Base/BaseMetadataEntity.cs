namespace FeatureFlag.Domain.Entities.Base;

public abstract class BaseMetadataEntity : BaseEntity
{
    public abstract Guid TypeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid MetadataId { get; set; } = Guid.NewGuid();

    public string ApplicationName { get; set; } = "FeatureFlags";
}
