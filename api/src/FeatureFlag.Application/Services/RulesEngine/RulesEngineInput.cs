using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Application.Services.RulesEngine;

/// <summary>
/// The POCO leveraged to send complete feature flag rules and current content information
/// to the <see cref="RulesEngineService"/> and <see cref="RuleFactory"/>.
/// </summary>
/// <remarks>
/// When additional context is needed by the <see cref="RulesEngineService"/> to make a decision, then this
/// is the class to extend.</remarks>
public sealed class RulesEngineInput
{
    /// <summary>
    /// The list of feature flag rules to be evaluated by the rules engine.
    /// </summary>
    public List<RuleModel> Rules { get; set; } = [];

    /// <summary>
    /// The list of application roles that are associated to the user whose requesting a feature flag status evaluation by the <see cref="RulesEngineService"/>.
    /// </summary>
    public List<Guid> ApplicationUserRoles { get; set; } = [];

    /// <summary>
    /// The unique identifier of the application user whose requesting a feature flag status evaluation by the <see cref="RulesEngineService"/>.
    /// </summary>
    public Guid ApplicationUserId { get; set; }

    /// <summary>
    /// The date and time to be used for evaluation by the <see cref="RulesEngineService"/>.
    /// </summary>
    public DateTime EvaluationDate { get; set; } = DateTime.Now;
}
