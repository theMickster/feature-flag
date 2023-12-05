using AutoMapper;
using FeatureFlag.Domain.Entities.Rule;
using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Domain.Profiles;

public sealed class RuleEntityToModelProfile : Profile
{
    public RuleEntityToModelProfile()
    {
        CreateMap<RuleEntity, RuleModel>();

        CreateMap<TimeRangeRuleEntity, DateRangeRuleModel>();

        CreateMap<ApplicationRoleRuleEntity, ApplicationRoleRuleModel>();
    }
}
