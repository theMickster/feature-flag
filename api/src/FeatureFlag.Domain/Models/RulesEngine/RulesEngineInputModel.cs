using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Domain.Models.RulesEngine;

public sealed class RulesEngineInputModel
{
    public List<RuleModel> Rules { get; set; } = new();

    public List<Guid>? ApplicationUserRoles { get; set; }

    public Guid ApplicationUserId { get; set; }

    public DateTime EvaluationDate { get; set; } = DateTime.Now;
}
