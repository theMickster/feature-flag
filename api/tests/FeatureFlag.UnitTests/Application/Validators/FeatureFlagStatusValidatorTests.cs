using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Application.Validators;
using FeatureFlag.Common.Filtering;
using FeatureFlag.Domain.Models.FeatureFlag;
using FeatureFlag.Domain.Models.FeatureFlagConfig;
using FeatureFlag.Domain.Models.FeatureFlagStatus;
using FeatureFlag.UnitTests.Setup;
using FluentValidation.TestHelper;

namespace FeatureFlag.UnitTests.Application.Validators;

[ExcludeFromCodeCoverage]
public sealed class FeatureFlagStatusValidatorTests : UnitTestBase
{
    private readonly FeatureFlagStatusValidator _sut;
    private FeatureFlagStatusInputParams _model = new();
    private readonly Mock<IReadFeatureFlagService> _mockReadFeatureFlagService = new();
    private readonly Mock<IReadFeatureFlagConfigService> _mockReadFeatureFlagConfigService = new();

    public FeatureFlagStatusValidatorTests()
    {
        _sut = new FeatureFlagStatusValidator(_mockReadFeatureFlagService.Object, _mockReadFeatureFlagConfigService.Object);
    }

    [Fact]
    public async Task Validator_succeeds_when_all_data_is_validAsync()
    {
        _mockReadFeatureFlagService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync( new FeatureFlagModel{Id = Guid.NewGuid(), Name = "FeatureFlagName", DisplayName = "My Cool Feature Flag"} );

        _mockReadFeatureFlagConfigService
            .Setup(x => x.GetFeatureFlagConfigsAsync(It.IsAny<FeatureFlagConfigParameter>()))
            .ReturnsAsync(new FeatureFlagConfigSearchResultModel
            {
                PageNumber = 1, PageSize = 50, TotalRecords = 10,
                Results = new List<FeatureFlagConfigModel>
                {
                    new(){ FeatureFlagId = new Guid("5a817661-85b8-4e21-a9d1-7807af2bb2c6"), 
                            Applications = new() {new () {Id = new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444"), Name = "Adventure Works" } },
                            Environments = new() {new () {Id = new Guid("b121e69e-6f82-4504-a1d9-4952e1d0f9f5"), Name = "Production" } }
                    }
                }
            });

        _model = new FeatureFlagStatusInputParams
        {
            FeatureFlagId = new Guid("5a817661-85b8-4e21-a9d1-7807af2bb2c6"),
            ApplicationId = new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444"),
            EnvironmentId = new Guid("b121e69e-6f82-4504-a1d9-4952e1d0f9f5")
        };

        var validationResult = await _sut.TestValidateAsync(_model);

        using (new AssertionScope())
        {
            validationResult.ShouldNotHaveAnyValidationErrors();
            validationResult.IsValid.Should().BeTrue();
        }
    }

    [Fact]
    public async Task Validator_returns_invalid_when_feature_flag_is_not_found_Async()
    {
        _mockReadFeatureFlagService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((FeatureFlagModel)null!);

        _model = new FeatureFlagStatusInputParams
        {
            FeatureFlagId = new Guid("5a817661-85b8-4e21-a9d1-7807af2bb2c6"),
            ApplicationId = new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444"),
            EnvironmentId = new Guid("b121e69e-6f82-4504-a1d9-4952e1d0f9f5")
        };

        var validationResult = await _sut.TestValidateAsync(_model);

        using (new AssertionScope())
        {
            validationResult.ShouldHaveValidationErrorFor(a => a.FeatureFlagId)
                .WithErrorCode("Rule-01");
        }
    }

}
