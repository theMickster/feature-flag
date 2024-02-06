using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models.FeatureFlagStatus;
using FluentValidation;

namespace FeatureFlag.Application.Validators;

public class FeatureFlagStatusValidator : AbstractValidator<FeatureFlagStatusInputParams>
{
    private readonly IReadFeatureFlagService _readFeatureFlagService;
    private readonly IReadFeatureFlagConfigService _readFeatureFlagConfigService;

    public FeatureFlagStatusValidator(IReadFeatureFlagService readFeatureFlagService, IReadFeatureFlagConfigService readFeatureFlagConfigService)
    {
        _readFeatureFlagService = readFeatureFlagService ?? throw new ArgumentNullException(nameof(readFeatureFlagService));
        _readFeatureFlagConfigService = readFeatureFlagConfigService ?? throw new ArgumentNullException(nameof(readFeatureFlagConfigService));

        RuleFor(x => x)
            .MustAsync(async (model, cancellation) => await FeatureFlagMustExist(model.FeatureFlagId))
            .OverridePropertyName(x => x.FeatureFlagId)
            .WithMessage(FeatureFlagInvalid)
            .WithErrorCode("FeatureFlagStatus-Rule-01");

        RuleFor(x => x)
            .MustAsync(async (model, cancellation) => await FeatureFlagConfigurationMustExist(model.FeatureFlagId, model.ApplicationId, model.EnvironmentId))
            .OverridePropertyName("FeatureFlagStatusModel")
            .WithMessage(ModelInputsInvalid)
            .WithErrorCode("FeatureFlagStatus-Rule-02");

        RuleFor(x => x.TimeZoneOffset)
            .InclusiveBetween(-12, 14)
            .When(x => x.TimeZoneOffset != null)
            .WithMessage(LocalTimeZoneInvalid)
            .WithErrorCode("FeatureFlagStatus-Rule-03");
    }

    public static string FeatureFlagInvalid => "Feature Flag Id must exist in the system.";

    public static string ModelInputsInvalid => "The feature flag has not been properly configured for the given application and environment.";
    
    public static string LocalTimeZoneInvalid => "The timezone offset of the user is outside the bounds of valid timezone offsets.";

    private async Task<bool> FeatureFlagMustExist(Guid featureFlagId)
    {
        var result = await _readFeatureFlagService.GetByIdAsync(featureFlagId);
        return result != null && result.Id != Guid.Empty;
    }

    private async Task<bool> FeatureFlagConfigurationMustExist(Guid featureFlagId, Guid applicationId, Guid environmentId)
    {
        var searchResult = await _readFeatureFlagConfigService.GetFeatureFlagConfigsAsync(new FeatureFlagConfigParameter { FeatureFlagId = featureFlagId});

        if (searchResult.Results == null || searchResult.Results.Count == 0)
        {
            return false;
        }

        return searchResult.Results.Any(x => x.FeatureFlagId == featureFlagId 
                                         && x.Applications.Any(y => y.Id == applicationId)
                                         && x.Environments.Any(z => z.Id == environmentId));
    }

}
