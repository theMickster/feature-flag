using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Application.Interfaces.Services.RuleEvaluator;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Domain.Models.FeatureFlagStatus;
using Microsoft.Extensions.Logging;

namespace FeatureFlag.Application.Services;

[ServiceLifetimeScoped]
public sealed class ReadFeatureFlagStatus : IReadFeatureFlagStatus
{
    private readonly ILogger<ReadFeatureFlagStatus> _logger;
    private readonly IRuleEvaluatorService _ruleEvaluatorService;
    private readonly IReadFeatureFlagConfigService _readFeatureFlagConfigService;
    private readonly IReadFeatureFlagService _readFeatureFlagService;

    public ReadFeatureFlagStatus(
        ILogger<ReadFeatureFlagStatus> logger, 
        IRuleEvaluatorService ruleEvaluatorService, 
        IReadFeatureFlagConfigService readFeatureFlagConfigService,
        IReadFeatureFlagService readFeatureFlagService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ruleEvaluatorService = ruleEvaluatorService ?? throw new ArgumentNullException(nameof(ruleEvaluatorService));
        _readFeatureFlagConfigService = readFeatureFlagConfigService ?? throw new ArgumentNullException(nameof(readFeatureFlagConfigService));
        _readFeatureFlagService = readFeatureFlagService ?? throw new ArgumentNullException(nameof(readFeatureFlagService));
    }

    /// <summary>
    /// Retrieve the feature flag's status based upon the context of the user inputs.
    /// </summary>
    /// <param name="inputParams"></param>
    /// <returns></returns>
    public async Task<FeatureFlagStatusModel> GetFeatureFlagStatusAsync(FeatureFlagStatusInputParams inputParams)
    {
        var featureFlagModel = await _readFeatureFlagService.GetByIdAsync(inputParams.FeatureFlagId);
        if (featureFlagModel == null)
        {

        }



        throw new NotImplementedException();
    }
}
