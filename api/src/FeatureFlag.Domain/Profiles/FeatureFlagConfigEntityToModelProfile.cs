using AutoMapper;
using FeatureFlag.Domain.Entities;
using FeatureFlag.Domain.Models.FeatureFlagConfig;

namespace FeatureFlag.Domain.Profiles;

public sealed class FeatureFlagConfigEntityToModelProfile : Profile
{
    public FeatureFlagConfigEntityToModelProfile()
    {
        CreateMap<FeatureFlagConfigEntity, FeatureFlagConfigModel>();
    }

}
