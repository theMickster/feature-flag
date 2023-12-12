using FeatureFlag.Application.Interfaces.Services.RuleEvaluator;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RulesEngine;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public abstract class RuleBase : IRule
{
    protected readonly RuleModel RuleModel;
    protected readonly Guid ApplicationUserId;
    protected readonly List<Guid>? ApplicationRoles;
    protected readonly DateTime LocalNow = DateTime.Now;
    protected readonly DateTime UtcNow = DateTime.UtcNow;

    protected RuleBase(RuleModel ruleModel, Guid applicationUserId, List<Guid>? applicationRoles)
    {
        RuleModel = ruleModel ?? throw new ArgumentNullException(nameof(ruleModel));
        ApplicationUserId = applicationUserId;
        ApplicationRoles = applicationRoles ?? throw new ArgumentNullException(nameof(applicationRoles));
    }

    public abstract Guid RuleTypeId { get; }
    
    public abstract RuleResultTypeEnum Run();
}
