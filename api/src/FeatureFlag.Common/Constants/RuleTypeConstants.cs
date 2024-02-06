namespace FeatureFlag.Common.Constants;

public static class RuleTypeConstants
{
    public static readonly Guid AlwaysDisabledRuleId = new("26d17b10-dc3a-4e74-97b1-e19eec79f6a0");

    public static readonly Guid AlwaysEnabledRuleId = new("b1b66940-68c3-4bd2-b99e-0538a08de10f");

    public static readonly Guid ApplicationRoleRuleId = new("e51fbda9-e0c4-4bca-8c91-d060a537c743");
    
    public static readonly Guid ApplicationUserRuleId = new("f84643ae-4405-440f-8f5a-a167ad90e97a");

    public static readonly Guid DateWindowRuleId = new("63adc07a-3793-4b9c-ab49-7ad3e805d3ef");
    
    public static readonly Guid TimeWindowRuleId = new("b51e6e73-7a56-4547-9cb4-3287992cca0c");
}
