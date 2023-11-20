using AutoMapper;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Models.FeatureFlag;

namespace FeatureFlag.Domain.Profiles;

public sealed class FeatureFlagEntityToModelProfile : Profile
{
    public FeatureFlagEntityToModelProfile()
    {
        CreateMap<FeatureFlagEntity, FeatureFlagModel>()
            .ForMember(x => x.Id, options => options.MapFrom(y => y.FeatureFlagId))
            .ReverseMap();
    }
}
