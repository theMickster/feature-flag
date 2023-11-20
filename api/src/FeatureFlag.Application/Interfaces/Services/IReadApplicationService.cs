using FeatureFlag.Application.Interfaces.Services.Base;
using FeatureFlag.Domain.Models.Application;

namespace FeatureFlag.Application.Interfaces.Services;

public interface IReadApplicationService : IReadMetadataBaseService<ApplicationModel>
{
}