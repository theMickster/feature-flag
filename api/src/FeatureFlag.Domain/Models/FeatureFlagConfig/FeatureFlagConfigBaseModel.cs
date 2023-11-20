using FeatureFlag.Domain.Models.Application;
using FeatureFlag.Domain.Models.Environment;
using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Domain.Models.FeatureFlagConfig;

public abstract class FeatureFlagConfigBaseModel
{
    public Guid FeatureFlagId { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<ApplicationModel> Applications { get; set; } = new();

    public List<EnvironmentModel> Environments { get; set; } = new();

    public List<RuleModel> Rules { get; set; } = new();
}
