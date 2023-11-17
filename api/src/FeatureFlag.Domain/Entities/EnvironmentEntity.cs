using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Entities.Base;

namespace FeatureFlag.Domain.Entities;

public sealed class EnvironmentEntity : BaseMetadataEntity
{
    public string TypeName = PartitionKeyConstants.Environment;

    public override Guid TypeId { get; set; } = PartitionKeyConstants.EnvironmentGuid;
}
