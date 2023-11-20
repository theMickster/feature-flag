using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Entities.Base;
using FeatureFlag.Domain.Entities.Rule;
using FeatureFlag.Domain.Entities.Slim;

namespace FeatureFlag.Domain.Entities;

public class FeatureFlagConfigEntity : FeatureFlagBaseEntity
{
    public string EntityType = PartitionKeyConstants.FeatureFlagConfig;

    public List<ApplicationSlimEntity> Applications { get; set; } = new();

    public List<EnvironmentSlimEntity> Environments { get; set; } = new();

    public List<RuleEntity> Rules { get; set; } = new();

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public string ModifiedBy { get; set; } = string.Empty;
}
