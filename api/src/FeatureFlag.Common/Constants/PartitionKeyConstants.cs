namespace FeatureFlag.Common.Constants;

public static class PartitionKeyConstants
{
    public static readonly string FeatureFlag = "FeatureFlag";

    public static readonly string FeatureFlagConfig = "FeatureFlagConfig";

    public static readonly string MetadataApplicationName = "FeatureFlags";

    public static readonly string Environment = "Environment";

    public static readonly Guid EnvironmentGuid = new("d732a26e-7ced-4f2c-8566-3132d1469baa");

    public static readonly string Application = "Application";

    public static readonly Guid ApplicationGuid = new("d28be6d8-b764-47f1-9ff7-0947cba39168");

    public static readonly string RuleType = "RuleType";

    public static readonly Guid RuleTypeGuid = new("75c1577a-f353-4735-a53c-72c7b8dbe1c4");
}
