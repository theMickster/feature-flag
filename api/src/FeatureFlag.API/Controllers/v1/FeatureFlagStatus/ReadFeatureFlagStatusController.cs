using Asp.Versioning;
using FeatureFlag.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlag.API.Controllers.v1.FeatureFlagStatus;

/// <summary>
/// The controller that coordinates a feature flag's status.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Feature Flag")]
[Route("api/v{version:apiVersion}/featureFlags", Name = "Read Feature Flag Status Controller v1")]
[Produces("application/json")]
public class ReadFeatureFlagStatusController : ControllerBase
{
    private readonly ILogger<ReadFeatureFlagStatusController> _logger;

    /// <summary>
    /// The controller that coordinates a feature flag's status.
    /// </summary>
    public ReadFeatureFlagStatusController(ILogger<ReadFeatureFlagStatusController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieve a feature flag's status
    /// </summary>
    /// <param name="featureFlagId">the unique identifier</param>
    /// <returns>A single feature flag's status</returns>
    [HttpGet("{featureFlagId:guid}/status", Name = "GetFeatureFlagStatusById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagStatusModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFeatureFlagStatusAsync(Guid featureFlagId)
    {

        var model = new FeatureFlagStatusModel
        {
            Id = featureFlagId,
            Name = "AlwaysOnSampleFeature",
            DisplayName = "An Always Enabled (Always On) Application Feature",
            Status = "On"
        };

        return Ok(model);
    }
}
