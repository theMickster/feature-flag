using Asp.Versioning;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models.FeatureFlag;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace FeatureFlag.API.Controllers.v1.FeatureFlag;

/// <summary>
/// The controller that coordinates retrieving feature flag data.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Feature Flag")]
[Route("api/v{version:apiVersion}/featureFlags", Name = "Read Feature Flag Controller v1")]
[Produces("application/json")]
public class ReadFeatureFlagController : ControllerBase
{
    private readonly ILogger<ReadFeatureFlagController> _logger;
    private readonly IReadFeatureFlagService _readFeatureFlagService;

    /// <summary>
    /// The controller that coordinates retrieving feature flag data.
    /// </summary>
    public ReadFeatureFlagController(ILogger<ReadFeatureFlagController> logger, IReadFeatureFlagService readReadFeatureFlagService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _readFeatureFlagService =
            readReadFeatureFlagService ?? throw new ArgumentNullException(nameof(readReadFeatureFlagService));
    }

    /// <summary>
    /// Retrieve a feature flag using its unique identifier
    /// </summary>
    /// <param name="featureFlagId">the unique identifier</param>
    /// <returns>A single feature flag</returns>
    [HttpGet("{featureFlagId:guid}", Name = "GetFeatureFlagById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid featureFlagId)
    {
        var model = await _readFeatureFlagService.GetByIdAsync(featureFlagId);
        return model == null ? NotFound("Unable to locate model.") : Ok(model);
    }

    /// <summary>
    /// Retrieves a paged list of feature flags
    /// </summary>
    /// <param name="parameters">store pagination query string</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagSearchResultModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFeatureFlagListAsync([FromQuery] FeatureFlagParameter parameters)
    {
        var searchResult = await _readFeatureFlagService.GetFeatureFlagsAsync(parameters).ConfigureAwait(false);

        if (searchResult.Results.Any())
        {
            return Ok(searchResult);
        }

        var logErrorParams = new
        {
            Status = AppLoggingConstants.StatusBadRequest,
            Operation = nameof(GetFeatureFlagListAsync),
            DateTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
            Message = "Unable to locate results based upon input query parameters.",
            ErrorCode = AppLoggingConstants.HttpGetRequestErrorCode,
            ServiceConstants.ServiceId,
            AdditionalInfo = parameters
        };

        _logger.LogError(JsonSerializer.Serialize(logErrorParams), 
            logErrorParams.Status, logErrorParams.Operation, logErrorParams.DateTime, logErrorParams.Message, 
            logErrorParams.ErrorCode,logErrorParams.ServiceId, logErrorParams.AdditionalInfo);

        return BadRequest(logErrorParams.Message);

    }
}
