using Asp.Versioning;
using FeatureFlag.API.Controllers.Base;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Domain.Models.Application;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlag.API.Controllers.v1.Application;

/// <summary>
/// The controller that coordinates retrieving application name metadata.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Application")]
[Route("api/v{version:apiVersion}/applications", Name = "Read Application Controller v1")]
[Produces("application/json")]
public class ReadApplicationController : ReadMetadataBaseController<ApplicationModel>
{
    /// <summary>
    /// The controller that coordinates retrieving application name metadata.
    /// </summary>
    public ReadApplicationController(ILogger<ReadApplicationController> logger, IReadApplicationService readService) 
        : base (logger, readService)
    {
    }

    /// <summary>
    /// Retrieve an application using its unique identifier
    /// </summary>
    /// <param name="id">the unique identifier</param>
    /// <returns>A single <seealso cref="ApplicationModel"/></returns>
    [HttpGet("{id:Guid}", Name = "GetApplicationById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var model = await ReadMetadataService.GetModelById(id);
        return model == null ? NotFound("Unable to locate model.") : Ok(model);
    }

    /// <summary>
    /// Retrieves the list of applications
    /// </summary>
    /// <returns>List of <seealso cref="ApplicationModel"/></returns>
    [HttpGet(Name = "GetApplications")]
    [Produces(typeof(List<ApplicationModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList()
    {
        var models = await ReadMetadataService.GetListAsync();
        if (models.Count == 0)
        {
            return NotFound("Unable to locate records.");
        }
        return Ok(models);
    }
}