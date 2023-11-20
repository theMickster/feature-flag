using Asp.Versioning;
using FeatureFlag.API.Controllers.Base;
using FeatureFlag.API.Controllers.v1.Application;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Domain.Models.Environment;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlag.API.Controllers.v1.Environment;

/// <summary>
/// The controller that coordinates retrieving environment name metadata.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Environment")]
[Route("api/v{version:apiVersion}/environments", Name = "Read Environment Controller v1")]
[Produces("application/json")]
public class ReadEnvironmentController : ReadMetadataBaseController<EnvironmentModel>
{
    /// <summary>
    /// The controller that coordinates retrieving application name metadata.
    /// </summary>
    public ReadEnvironmentController(ILogger<ReadEnvironmentController> logger, IReadEnvironmentService readService)
        : base(logger, readService)
    {
    }

    /// <summary>
    /// Retrieve an environment using its unique identifier
    /// </summary>
    /// <param name="id">the unique identifier</param>
    /// <returns>A single <seealso cref="EnvironmentModel"/></returns>
    [HttpGet("{id:Guid}", Name = "GetEnvironmentById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnvironmentModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var model = await ReadMetadataService.GetModelById(id);
        return model == null ? NotFound("Unable to locate model.") : Ok(model);
    }

    /// <summary>
    /// Retrieves the list of environments
    /// </summary>
    /// <returns>List of <seealso cref="EnvironmentModel"/></returns>
    [HttpGet(Name = "GetEnvironments")]
    [Produces(typeof(List<EnvironmentModel>))]
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
