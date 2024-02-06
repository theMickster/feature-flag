using FeatureFlag.Application.Interfaces.Services.RulesEngine;
using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Application.Services.RulesEngine.Logic;

public abstract class RuleBase : IRule
{
    protected readonly RuleModel RuleModel;
    protected readonly Guid ApplicationUserId;
    protected readonly List<Guid> ApplicationRoles;
    protected readonly DateTime EvaluationDate;
    protected readonly DateTime LocalNow = DateTime.Now;
    protected readonly DateTime UtcNow = DateTime.UtcNow;
    protected readonly TimeOnly EvaluationTime;

    protected RuleBase(RuleInput input)
    {
        ArgumentNullException.ThrowIfNull(input);

        RuleModel = input.Rule ?? throw new ArgumentNullException(nameof(input.Rule));
        ApplicationUserId = input.ApplicationUserId;
        ApplicationRoles = input.ApplicationUserRoles ?? throw new ArgumentNullException(nameof(input.ApplicationUserRoles));
        EvaluationDate = input.EvaluationDate;
        EvaluationTime = TimeOnly.FromDateTime(input.EvaluationDate);
    }

    protected RuleBase(RuleModel ruleModel, Guid applicationUserId, List<Guid>? applicationRoles)
    {
        RuleModel = ruleModel ?? throw new ArgumentNullException(nameof(ruleModel));
        ApplicationUserId = applicationUserId;
        ApplicationRoles = applicationRoles ?? throw new ArgumentNullException(nameof(applicationRoles));
    }

    public abstract Guid RuleTypeId { get; }
    
    public abstract RuleResultTypeEnum Run();
}
