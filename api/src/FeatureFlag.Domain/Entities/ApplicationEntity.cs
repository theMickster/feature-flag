using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Entities.Base;

namespace FeatureFlag.Domain.Entities;

public sealed class ApplicationEntity : MetadataBaseEntity
{
    public string TypeName = PartitionKeyConstants.Application;

    public override Guid TypeId { get; set; } = PartitionKeyConstants.ApplicationGuid;
}
