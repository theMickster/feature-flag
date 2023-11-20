using FeatureFlag.Application.Interfaces.Services.Base;
using FeatureFlag.Domain.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlag.API.Controllers.Base;

/// <summary>
/// The base class for all metadata controllers.
/// </summary>
/// <typeparam name="TMetadataModel">the end-user/contract metadata model.</typeparam>
public abstract class ReadMetadataBaseController<TMetadataModel> : ControllerBase where TMetadataModel : MetadataBaseModel
{
    /// <summary>
    /// The controller's logger
    /// </summary>
    protected readonly ILogger<ReadMetadataBaseController<TMetadataModel>> ControllerLogger;

    /// <summary>
    /// The data service used to query metadata.
    /// </summary>
    protected readonly IReadMetadataBaseService<TMetadataModel> ReadMetadataService;

    /// <summary>
    /// The base class for all metadata controllers.
    /// </summary>
    /// <typeparam name="TMetadataModel">the end-user/contract metadata model.</typeparam>
    protected ReadMetadataBaseController(ILogger<ReadMetadataBaseController<TMetadataModel>> logger, IReadMetadataBaseService<TMetadataModel> readMetadataService)
    {
        ControllerLogger = logger ?? throw new ArgumentNullException(nameof(logger));
        ReadMetadataService = readMetadataService ?? throw new ArgumentNullException(nameof(readMetadataService));
    }
    
}
