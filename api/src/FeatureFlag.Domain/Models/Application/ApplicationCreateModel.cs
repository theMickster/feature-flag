using FeatureFlag.Common.Constants;

namespace FeatureFlag.Domain.Models.Application;

public sealed class ApplicationCreateModel
{
    public string TypeName = PartitionKeyConstants.Application;

    public Guid TypeId = PartitionKeyConstants.ApplicationGuid;

    public string Name { get; set; } = string.Empty;

    public string MetadataApplicationName = PartitionKeyConstants.MetadataApplicationName;
}
