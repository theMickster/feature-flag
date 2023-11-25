using Asp.Versioning;
using FeatureFlag.API.QueryParams;
using FeatureFlag.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FeatureFlag.Domain.Models.FeatureFlagStatus;

namespace FeatureFlag.API.Controllers.v1.FeatureFlagStatus;

/// <summary>
/// The controller that coordinates a feature flag's status.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Feature Flag Status")]
[Route("api/v{version:apiVersion}/featureFlagStatus", Name = "Read Feature Flag Status Controller v1")]
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
    /// <returns>A single feature flag's status</returns>
    [HttpGet(Name = "GetFeatureFlagStatus")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagStatusModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFeatureFlagStatusAsync([FromQuery][Required]FeatureFlagStatusQueryParameters queryParams )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = new FeatureFlagStatusModel
        {
            Id = queryParams.FeatureFlagId,
            ApplicationId = queryParams.ApplicationId,
            EnvironmentId = queryParams.EnvironmentId,
            Name = "AlwaysOnSampleFeature",
            DisplayName = "An Always Enabled (Always On) Application Feature",
            Status = "On"
        };

        return Ok(model);
    }
}
