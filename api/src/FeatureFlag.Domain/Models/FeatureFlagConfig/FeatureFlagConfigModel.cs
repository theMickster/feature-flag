namespace FeatureFlag.Domain.Models.FeatureFlagConfig;

public class FeatureFlagConfigModel : FeatureFlagConfigBaseModel
{
    public Guid Id { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }
}
