using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlag.API.Controllers.v1.FeatureFlag;

/// <summary>
/// The controller that coordinates retrieving feature flag data.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Feature Flag")]
[Route("api/v1/featureFlags", Name = "Read Feature Flag Controller v1")]
[Produces("application/json")]
public class ReadFeatureFlagController : ControllerBase
{
    private readonly ILogger<ReadFeatureFlagController> _logger;

    public ReadFeatureFlagController(ILogger<ReadFeatureFlagController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
}
