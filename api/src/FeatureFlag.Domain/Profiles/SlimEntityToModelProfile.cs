using AutoMapper;
using FeatureFlag.Domain.Entities.Slim;
using FeatureFlag.Domain.Models.Application;
using FeatureFlag.Domain.Models.Environment;
using FeatureFlag.Domain.Models.RuleType;

namespace FeatureFlag.Domain.Profiles;

public class SlimEntityToModelProfile : Profile
{
    public SlimEntityToModelProfile()
    {
        CreateMap<ApplicationSlimEntity, ApplicationModel>()
            .ForPath(x => x.Id, o => o.MapFrom(y => y.MetadataId));

        CreateMap<EnvironmentSlimEntity, EnvironmentModel>()
            .ForPath(x => x.Id, o => o.MapFrom(y => y.MetadataId));

        CreateMap<RuleTypeSlimEntity, RuleTypeModel>()
            .ForPath(x => x.Id, o => o.MapFrom(y => y.MetadataId))
            .ForPath(x => x.Name, o => o.MapFrom(y => y.Name))
            .ForPath(x => x.Description, o => o.Ignore());
    }
}
