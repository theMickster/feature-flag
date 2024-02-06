using AutoMapper;
using FeatureFlag.Domain.Entities.Rule;
using FeatureFlag.Domain.Models.Rule;

namespace FeatureFlag.Domain.Profiles;

public sealed class RuleEntityToModelProfile : Profile
{
    public RuleEntityToModelProfile()
    {
        CreateMap<RuleEntity, RuleModel>();

        CreateMap<RuleParameterEntity, RuleParameterModel>();

        CreateMap<DateRangeRuleEntity, DateRangeRuleModel>();

        CreateMap<TimeRangeRuleEntity, TimeRangeRuleModel>()
            .ForMember(x => x.StartTime, o => o.MapFrom(y => TimeOnly.FromDateTime(y.StartDate)))
            .ForMember(x => x.EndTime, o => o.MapFrom(y => TimeOnly.FromDateTime(y.EndDate)));

        CreateMap<TimeRangeRuleModel, TimeRangeRuleEntity>()
            .ForMember(x => x.StartDate,
                o => o.MapFrom(y => new DateTime(new DateOnly(01, 01, 2001), y.StartTime)))
            .ForMember(x => x.EndDate,
                o => o.MapFrom(y => new DateTime(new DateOnly(01, 01, 2001), y.EndTime)));

        CreateMap<ApplicationRoleRuleEntity, ApplicationRoleRuleModel>();
    }
}
