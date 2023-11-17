using AutoMapper;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Models.FeatureFlag;

namespace FeatureFlag.Domain.Profiles;

public class FeatureFlagEntityToModelProfile : Profile
{
    public FeatureFlagEntityToModelProfile()
    {
        CreateMap<FeatureFlagEntity, FeatureFlagModel>()
            .ForPath(x => x.Id, options => options.MapFrom(y => y.FeatureFlagId))
            .ForPath(x => x.Name, options => options.MapFrom(y => y.Name))
            .ForPath(x => x.DisplayName, options => options.MapFrom(y => y.DisplayName))
            .ReverseMap();

    }
}
