using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Application.Interfaces.Services.RulesEngine;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Domain.Models.FeatureFlagStatus;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FeatureFlag.Application.Services;

[ServiceLifetimeScoped]
public sealed class ReadFeatureFlagStatus : IReadFeatureFlagStatus
{
    private readonly ILogger<ReadFeatureFlagStatus> _logger;
    private readonly IRulesEngineService _ruleEvaluatorService;
    private readonly IValidator<FeatureFlagStatusInputParams> _validator;
    
    public ReadFeatureFlagStatus(
        ILogger<ReadFeatureFlagStatus> logger, 
        IRulesEngineService ruleEvaluatorService,
        IValidator<FeatureFlagStatusInputParams> validator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ruleEvaluatorService = ruleEvaluatorService ?? throw new ArgumentNullException(nameof(ruleEvaluatorService));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

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


        throw new NotImplementedException();
    }
}
