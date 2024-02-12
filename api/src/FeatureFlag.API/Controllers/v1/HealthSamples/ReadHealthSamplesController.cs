using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FeatureFlag.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace FeatureFlag.API.Controllers.v1.HealthSamples;

/// <summary>
/// The controller that coordinates retrieving application health data.
/// </summary>
[Authorize]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Health Samples")]
[Route("api/v{version:apiVersion}/healthSamples", Name = "Read Health Samples Controller v1")]
[Produces("application/json")]
public class ReadHealthSamplesController(ILogger<ReadHealthSamplesController> logger) : HealthSamplesBaseController(logger)
{
    /// <summary>
    /// Retrieve a health sample using its unique identifier
    /// </summary>
    /// <param name="id">the unique identifier</param>
    /// <returns>A single <seealso cref="HealthSamplesBaseController.HealthSampleModel"/></returns>
    [Authorize(Policy = AuthPolicyConstants.RequireAdministratorPolicy)]
    [HttpGet("{id:Guid}", Name = "GetHealthSampleById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HealthSampleModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByIdAsync([Required]Guid id)
    {
        if (!ModelState.IsValid || id == Guid.Empty)
        {
            ControllerLogger.LogInformation("Invalid health sample request. A valid health sample unique identifier is required.");
            return BadRequest("Invalid health sample request.");
        }
        
        var model = HealthSamples.FirstOrDefault(x => x.Id == id);
        
        return model == null ? NotFound("Unable to locate model.") : Ok(model);
    }

    /// <summary>
    /// Retrieves the list of health samples
    /// </summary>
    /// <returns>List of <seealso cref="HealthSamplesBaseController.HealthSampleModel"/></returns>
    [HttpGet(Name = "GetHealthSamples")]
    [Produces(typeof(List<HealthSampleModel>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetList()
    {
        ControllerLogger.LogInformation("Health sample list request received");
        return Ok(HealthSamples);
    }


}
