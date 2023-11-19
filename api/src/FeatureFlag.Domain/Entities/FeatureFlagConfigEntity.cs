using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Entities.Base;

namespace FeatureFlag.Domain.Entities;

public class FeatureFlagConfigEntity : FeatureFlagBaseEntity
{
    public string EntityType = PartitionKeyConstants.FeatureFlagConfig;

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public string ModifiedBy { get; set; } = string.Empty;
}
