using Asp.Versioning;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Text.Json;
using FeatureFlag.Domain.Models.FeatureFlagStatus;

namespace FeatureFlag.API.Controllers.v1.FeatureFlagConfig;

/// <summary>
/// The controller that coordinates retrieving feature flag configuration data.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Feature Flag Configurations")]
[Route("api/v{version:apiVersion}/featureFlags/{id:Guid}/configurations", Name = "Read Feature Flag Configuration Controller v1")]
[Produces("application/json")]
public class ReadFeatureFlagConfigController : ControllerBase
{
    private readonly ILogger<ReadFeatureFlagConfigController> _logger;
    private readonly IReadFeatureFlagConfigService _readFeatureFlagConfigService;

    /// <summary>
    /// The controller that coordinates retrieving feature flag configuration data.
    /// </summary>
    public ReadFeatureFlagConfigController(ILogger<ReadFeatureFlagConfigController> logger, IReadFeatureFlagConfigService readFeatureFlagConfigService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _readFeatureFlagConfigService = readFeatureFlagConfigService ?? throw new ArgumentNullException(nameof(readFeatureFlagConfigService));
    }

    /// <summary>
    /// Retrieve a feature flag configuration using its unique identifier
    /// </summary>
    /// <param name="id">the feature flag unique identifier</param>
    /// <param name="featureFlagConfigurationId">the feature flag configuration's unique identifier</param>
    /// <returns>A single feature flag configuration</returns>
    [HttpGet("{featureFlagConfigurationId:guid}", Name = "GetFeatureFlagConfigByIdAsync")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagStatusModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFeatureFlagConfigByIdAsync(
        [BindRequired]
        Guid id,
        [BindRequired]
        Guid featureFlagConfigurationId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = await _readFeatureFlagConfigService.GetByIdAsync(featureFlagConfigurationId, id);

        return model == null ? NotFound("Unable to locate model.") : Ok(model);
    }
    
    /// <summary>
    /// Retrieve a list of feature flag configurations by feature flag id.
    /// </summary>
    /// <param name="id">the feature flag unique identifier</param>
    /// <returns></returns>
    [HttpGet(Name = "GetFeatureFlagConfigsByFeatureFlagIdAsync")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FeatureFlagStatusModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFeatureFlagConfigsByFeatureFlagIdAsync([BindRequired] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var configParameter = new FeatureFlagConfigParameter { FeatureFlagId = id };
        var searchResult = await _readFeatureFlagConfigService.GetFeatureFlagConfigsAsync(configParameter);

        if (searchResult.Results != null && searchResult.Results.Count != 0)
        {
            return Ok(searchResult);
        }

        var logErrorParams = new
        {
            Status = AppLoggingConstants.StatusBadRequest,
            Operation = nameof(GetFeatureFlagConfigsByFeatureFlagIdAsync),
            DateTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
            Message = "Unable to locate results based upon input query parameters.",
            ErrorCode = AppLoggingConstants.HttpGetRequestErrorCode,
            ServiceConstants.ServiceId,
            AdditionalInfo = configParameter
        };

        _logger.LogError(JsonSerializer.Serialize(logErrorParams),
            logErrorParams.Status, logErrorParams.Operation, logErrorParams.DateTime, logErrorParams.Message,
            logErrorParams.ErrorCode, logErrorParams.ServiceId, logErrorParams.AdditionalInfo);

        return BadRequest(logErrorParams.Message);
    }

}
