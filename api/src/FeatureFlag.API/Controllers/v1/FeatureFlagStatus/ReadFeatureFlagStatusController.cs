using Asp.Versioning;
using FeatureFlag.API.Controllers.Base;
using FeatureFlag.API.QueryParams;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Models.FeatureFlagStatus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace FeatureFlag.API.Controllers.v1.FeatureFlagStatus;

/// <summary>
/// The controller that coordinates a feature flag's status.
/// </summary>
/// <remarks>
/// The controller that coordinates a feature flag's status.
/// </remarks>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Feature Flag Status")]
[Route("api/v{version:apiVersion}/featureFlagStatus", Name = "Read Feature Flag Status Controller v1")]
[Produces("application/json")]
public class ReadFeatureFlagStatusController(ILogger<ReadFeatureFlagStatusController> logger, IReadFeatureFlagStatus readFeatureFlagStatus) 
    : FeatureFlagBaseController
{
    private readonly ILogger<ReadFeatureFlagStatusController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IReadFeatureFlagStatus _readFeatureFlagStatus = readFeatureFlagStatus ?? throw new ArgumentNullException(nameof(readFeatureFlagStatus));

    /// <summary>
    /// Retrieve a feature flag's status
    /// </summary>
    /// <returns>A single feature flag's status</returns>
    [HttpGet(Name = "GetFeatureFlagStatus")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeatureFlagStatusModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFeatureFlagStatusAsync([FromQuery][Required]FeatureFlagStatusQueryParameters queryParams )
    {
        EventLogInfoParameters.Add(AppLoggingConstants.Operation, AppLoggingConstants.OperationFeatureFlagStatusRead);

        if (!ModelState.IsValid)
        {
            EventLogErrorParameters.Add(AppLoggingConstants.Status, AppLoggingConstants.StatusBadRequest);
            _logger.LogError(JsonConvert.SerializeObject(EventLogErrorParameters));

            return BadRequest(ModelState);
        }

        var inputs = new FeatureFlagStatusInputParams
        {
            ApplicationId = queryParams.ApplicationId,
            EnvironmentId = queryParams.EnvironmentId,
            FeatureFlagId = queryParams.FeatureFlagId,
            TimeZoneOffset = queryParams.TimeZoneOffset,
            CurrentUtcDate = DateTime.UtcNow
        };

        var (result, errors) = await _readFeatureFlagStatus.GetFeatureFlagStatusAsync(inputs);

        if (errors.Count > 0)
        {
            var errorString = string.Join(",", errors.Select(x => $"Error Code: {x.ErrorCode} Error Message: {x.ErrorMessage}"));
            EventLogErrorParameters.Add(AppLoggingConstants.Status, AppLoggingConstants.StatusBadRequest);
            EventLogErrorParameters.Add(AppLoggingConstants.Errors, errorString);
            _logger.LogError(JsonConvert.SerializeObject(EventLogErrorParameters));
            
            return BadRequest(errors.Select(x => new { x.ErrorCode, x.ErrorMessage, x.PropertyName, x.Severity }) );
        }

        EventLogInfoParameters.Add(AppLoggingConstants.Status, AppLoggingConstants.StatusOk);
        _logger.LogInformation(JsonConvert.SerializeObject(EventLogInfoParameters));
        
        return Ok(result);
    }
}
