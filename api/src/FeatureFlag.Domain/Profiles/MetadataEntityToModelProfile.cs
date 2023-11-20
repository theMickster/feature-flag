using AutoMapper;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Models.Application;
using FeatureFlag.Domain.Models.Environment;
using FeatureFlag.Domain.Models.RuleType;

namespace FeatureFlag.Domain.Profiles;
public class MetadataEntityToModelProfile : Profile
{
    public MetadataEntityToModelProfile()
    {
        CreateMap<ApplicationEntity, ApplicationModel>()
            .ForMember(x => x.Id, o => o.MapFrom(y => y.MetadataId));

        CreateMap<EnvironmentEntity, EnvironmentModel>()
            .ForMember(x => x.Id, o => o.MapFrom(y => y.MetadataId));

        CreateMap<RuleTypeEntity, RuleTypeModel>()
            .ForMember(x => x.Id, o => o.MapFrom(y => y.MetadataId));
    }
}
