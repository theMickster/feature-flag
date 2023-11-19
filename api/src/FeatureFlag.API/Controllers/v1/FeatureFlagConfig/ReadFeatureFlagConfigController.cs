using Asp.Versioning;
using FeatureFlag.Domain.Models;
using FeatureFlag.Domain.Models.FeatureFlagConfig;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlag.API.Controllers.v1.FeatureFlagConfig;

/// <summary>
/// The controller that coordinates retrieving feature flag configuration data.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Feature Flag Config")]
[Route("api/v{version:apiVersion}/featureFlagConfigurations", Name = "Read Feature Flag Configuration Controller v1")]
[Produces("application/json")]
public class ReadFeatureFlagConfigController : ControllerBase
{
    private readonly ILogger<ReadFeatureFlagConfigController> _logger;

    /// <summary>
    /// The controller that coordinates retrieving feature flag configuration data.
    /// </summary>
    public ReadFeatureFlagConfigController(ILogger<ReadFeatureFlagConfigController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieve a feature flag configuration using its unique identifier
    /// </summary>
    /// <param name="featureFlagId">the feature flag unique identifier</param>
    /// <param name="featureFlagConfigurationId">the feature flag configuration's unique identifier</param>
    /// <returns>A single feature flag configuration</returns>
    [HttpGet("{featureFlagConfigurationId:guid}", Name = "GetFeatureFlagConfigByIdAsync")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagStatusModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFeatureFlagConfigByIdAsync(Guid featureFlagId, Guid featureFlagConfigurationId)
    {

        var model = new FeatureFlagConfigModel
        {
            Id = featureFlagConfigurationId,
            Name = "AlwaysOnSampleFeature",
            FeatureFlagId = Guid.NewGuid()
        };

        return Ok(model);
    }
}
