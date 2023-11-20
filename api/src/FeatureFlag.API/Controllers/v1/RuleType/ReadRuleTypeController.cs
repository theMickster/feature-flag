using Asp.Versioning;
using FeatureFlag.API.Controllers.Base;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Domain.Models.RuleType;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlag.API.Controllers.v1.RuleType;

/// <summary>
/// The controller that coordinates retrieving rule type metadata.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Rule Type")]
[Route("api/v{version:apiVersion}/ruleTypes", Name = "Read Rule Type Controller v1")]
[Produces("application/json")]
public class ReadRuleTypeController : ReadMetadataBaseController<RuleTypeModel>
{
    /// <summary>
    /// The controller that coordinates retrieving application name metadata.
    /// </summary>
    public ReadRuleTypeController(ILogger<ReadRuleTypeController> logger, IReadRuleTypeService readService)
        : base(logger, readService)
    {
    }

    /// <summary>
    /// Retrieve a rule type using its unique identifier
    /// </summary>
    /// <param name="ruleTypeId">the unique identifier</param>
    /// <returns>A single <seealso cref="RuleTypeModel"/></returns>
    [HttpGet("{ruleTypeId:Guid}", Name = "GetRuleTypeById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RuleTypeModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid ruleTypeId)
    {
        var model = await ReadMetadataService.GetModelById(ruleTypeId);
        return model == null ? NotFound("Unable to locate model.") : Ok(model);
    }

    /// <summary>
    /// Retrieves the list of rule types
    /// </summary>
    /// <returns>List of <seealso cref="RuleTypeModel"/></returns>
    [HttpGet(Name = "GetRuleTypes")]
    [Produces(typeof(List<RuleTypeModel>))]
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
