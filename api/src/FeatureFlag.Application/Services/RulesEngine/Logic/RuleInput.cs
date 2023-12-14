using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public sealed class RuleInput
{
    /// <summary>
    /// The feature flag rule to be evaluated.
    /// </summary>
    public RuleModel Rule { get; set; }

    /// <summary>
    /// The list of application roles that are associated to the user to be evaluated by the rule.
    /// </summary>
    public List<Guid> ApplicationUserRoles { get; set; } = [];

    /// <summary>
    /// The unique identifier of the application user to be evaluated by the rule.
    /// </summary>
    public Guid ApplicationUserId { get; set; }

    /// <summary>
    /// The date and time to be evaluated by the rule.
    /// </summary>
    public DateTime EvaluationDate { get; set; } = DateTime.Now;
}
