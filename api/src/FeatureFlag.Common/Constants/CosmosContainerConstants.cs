namespace FeatureFlag.Common.Constants;

public static class CosmosContainerConstants
{
    /// <summary>
    /// CosmosDb container used for storing feature flag data.
    /// </summary>
    public const string MainContainer = "FeatureFlags";

    /// <summary>
    /// CosmosDb container used for storing supporting feature flag metadata.
    /// </summary>
    public const string MetadataContainer = "Metadata";
}
