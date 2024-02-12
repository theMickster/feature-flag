using FeatureFlag.API.Controllers.Base;

namespace FeatureFlag.API.Controllers.v1.HealthSamples;

/// <summary>
/// Base class used for fake health sample data.
/// </summary>
public abstract class HealthSamplesBaseController : FeatureFlagBaseController
{
    /// <summary>
    /// The controller's logger
    /// </summary>
    protected readonly ILogger<HealthSamplesBaseController> ControllerLogger;

    /// <summary>
    /// Some fake data.
    /// </summary>
    protected readonly IReadOnlyCollection<HealthSampleModel> HealthSamples;

    /// <summary>
    /// The base class for all health samples controllers.
    /// </summary>
    protected HealthSamplesBaseController(ILogger<HealthSamplesBaseController> logger)
    {
        ControllerLogger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        HealthSamples = new List<HealthSampleModel>()
        {
            new() {Id = new Guid("d53e38db-8b90-429b-86cb-54e99b88f7b2")},
            new() {Id = new Guid("6fc95a25-416e-4428-8c3d-86afb5ce4ccd")},
            new() {Id = new Guid("a101fc8e-f52c-437e-be8c-026e1c6330e9")},
            new() {Id = new Guid("7623e6cf-5c93-4337-abdc-a717119c5bc3")},
            new() {Id = new Guid("d23a2132-8621-4881-ad61-b1e98141e1b0")}
        };
    }

    /// <summary>
    /// A mock class for health sampling
    /// </summary>
    public sealed class HealthSampleModel
    {
        /// <summary>
        /// A unique identifier for the health sample.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Healthy response code
        /// </summary>
        public string Status => "Healthy";
    }
}
