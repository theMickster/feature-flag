using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Entities.Base;

namespace FeatureFlag.Domain.Entities;

public class FeatureFlagEntity : FeatureFlagBaseEntity
{
    public string EntityType = PartitionKeyConstants.FeatureFlag;

    public string DisplayName { get; set; } = string.Empty;
}
