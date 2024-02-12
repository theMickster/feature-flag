namespace FeatureFlag.Common.Constants;

public static class AuthPolicyConstants
{
    /// <summary>
    /// Administrators can manage everything within the Feature Flag API.
    /// </summary>
    public static readonly string AdministratorRole = "FeatureFlagAdmin";

    /// <summary>
    /// Contributors can read all feature flag data, including configurations but may not change any data.
    /// </summary>
    public static readonly string ContributorRole = "FeatureFlagContributor";

    /// <summary>
    /// Readers can read feature flag statuses and limited feature flag data.
    /// </summary>
    public static readonly string ReaderRole = "FeatureFlagReader";

    /// <summary>
    /// Policy name that enforces the user be in an Administrator role.
    /// </summary>
    public const string RequireAdministratorPolicy = "RequireAdministratorRole";

    /// <summary>
    /// Policy name that enforces the user be in a Contributor role.
    /// </summary>
    public const string RequireContributorPolicy = "RequireContributorRole";

    /// <summary>
    /// Policy name that enforces the user be in a Reader role.
    /// </summary>
    public const string RequireReaderPolicy = "RequireReaderRole";
}
