using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Application.Interfaces.Services.RulesEngine;
using FeatureFlag.Application.Services.RulesEngine;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models.FeatureFlagStatus;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FeatureFlag.Application.Services;

[ServiceLifetimeScoped]
public sealed class ReadFeatureFlagStatus(
    ILogger<ReadFeatureFlagStatus> logger,
    IRulesEngineService rulesEngineService,
    IReadFeatureFlagConfigService readFeatureFlagConfigService,
    IReadFeatureFlagService readFeatureFlagService,
    IValidator<FeatureFlagStatusInputParams> validator)
    : IReadFeatureFlagStatus
{
    private readonly ILogger<ReadFeatureFlagStatus> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IRulesEngineService _rulesEngineService = rulesEngineService ?? throw new ArgumentNullException(nameof(rulesEngineService));
    private readonly IReadFeatureFlagConfigService _readFeatureFlagConfigService = readFeatureFlagConfigService ?? throw new ArgumentNullException(nameof(readFeatureFlagConfigService));
    private readonly IValidator<FeatureFlagStatusInputParams> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    private readonly IReadFeatureFlagService _readFeatureFlagService = readFeatureFlagService ?? throw new ArgumentNullException(nameof(readFeatureFlagService));

    /// <summary>
    /// Retrieve the feature flag's status based upon the context of the user inputs.
    /// </summary>
    /// <param name="inputParams"></param>
    /// <returns></returns>
    public async Task<(FeatureFlagStatusModel, List<ValidationFailure>)> GetFeatureFlagStatusAsync(FeatureFlagStatusInputParams inputParams)
    {
        var validationResult = await _validator.ValidateAsync(inputParams);

        if (validationResult.Errors.Count != 0)
        {
            _logger.LogInformation("Validator errors occurred while attempting to retrieve feature flag status.");
            return (new FeatureFlagStatusModel(), validationResult.Errors);
        }
        
        var featureFlag = await _readFeatureFlagService.GetByIdAsync(inputParams.FeatureFlagId);
        if (featureFlag == null)
        {
            return (new FeatureFlagStatusModel(),
                [new() { ErrorCode = "FeatureFlagStatus-Rule-01", 
                    ErrorMessage = $"Unable to locate feature flag by id: {inputParams.FeatureFlagId}" }]);
        }

        var configs = await _readFeatureFlagConfigService.GetFeatureFlagConfigsAsync(
            new FeatureFlagConfigParameter {FeatureFlagId = inputParams.FeatureFlagId });

        if (configs.TotalRecords == 0 || configs.Results == null || configs.Results.Count == 0)
        {
            return (new FeatureFlagStatusModel(),
                    [new() { ErrorCode = "FeatureFlagStatus-Rule-02", ErrorMessage = $"Unable to locate any feature flag configurations for feature flag id {inputParams.FeatureFlagId}"}]);
        }

        var allRules = configs.Results.SelectMany(x => x.Rules).ToList();

        var rulesEngineInput = new RulesEngineInput
        {
            EvaluationDate = inputParams.LocalDate,
            ApplicationUserRoles = [],
            ApplicationUserId = Guid.NewGuid(),
            Rules = allRules
        };
        
        var rulesEngineOutcome = _rulesEngineService.Run(rulesEngineInput);
        
        var featureFlagStatus = new FeatureFlagStatusModel
        {
            Id = inputParams.FeatureFlagId,
            ApplicationId = inputParams.ApplicationId,
            EnvironmentId = inputParams.EnvironmentId,
            Status = rulesEngineOutcome.Outcome.ToString(),
            Name = featureFlag.Name,
            DisplayName = featureFlag.DisplayName
        };

        return (featureFlagStatus, validationResult.Errors);
    }
}
