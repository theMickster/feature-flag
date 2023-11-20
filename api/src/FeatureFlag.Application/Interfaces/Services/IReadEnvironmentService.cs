using FeatureFlag.Application.Interfaces.Services.Base;
using FeatureFlag.Domain.Models.Environment;

namespace FeatureFlag.Application.Interfaces.Services;

public interface IReadEnvironmentService : IReadMetadataBaseService<EnvironmentModel>
{
}