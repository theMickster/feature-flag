using FeatureFlag.API.Controllers.v1.FeatureFlagStatus;
using FeatureFlag.API.QueryParams;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Domain.Models.FeatureFlagStatus;
using FeatureFlag.UnitTests.Setup;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Net;

namespace FeatureFlag.UnitTests.API;

[ExcludeFromCodeCoverage]
public sealed class ReadFeatureFlagStatusControllerTests : UnitTestBase
{
    private ReadFeatureFlagStatusController _sut;
    private readonly Mock<ILogger<ReadFeatureFlagStatusController>> _mockLogger = new();
    private readonly Mock<IReadFeatureFlagStatus> _mockReadFeatureFlagStatus = new();

    public ReadFeatureFlagStatusControllerTests()
    {
        _sut = new ReadFeatureFlagStatusController(_mockLogger.Object, _mockReadFeatureFlagStatus.Object);
    }

    [Fact]
    public void Constructor_throws_correct_exceptions()
    {
        using (new AssertionScope())
        {
            _ = ((Action)(() => _sut = new ReadFeatureFlagStatusController(
                    null!,
                    _mockReadFeatureFlagStatus.Object
                    )))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("logger");

            _ = ((Action)(() => _sut = new ReadFeatureFlagStatusController(
                    _mockLogger.Object,
                    null!)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("readFeatureFlagStatus");
            
        }
    }

    [Fact]
    public async Task GetFeatureFlagStatusAsync_returns_bad_request_when_model_state_is_invalid()
    {
        _sut.ModelState.AddModelError("someFakeErrorGoesHere", "A unit test error goes here");

        var result = await _sut.GetFeatureFlagStatusAsync(new FeatureFlagStatusQueryParameters());
        var objectResult = result as BadRequestObjectResult;

        using (new AssertionScope())
        {
            objectResult.Should().NotBeNull();
            objectResult!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            objectResult!.Value!.Should().NotBeNull();
        }
    }

    [Fact]
    public async Task GetFeatureFlagStatusAsync_returns_bad_request_when_service_errors_occur()
    {
        var validationErrors = new List<ValidationFailure>
        {
            new()
            {
                ErrorCode = "U001",
                ErrorMessage = "A unit test error goes here"
            }
        };

        _mockReadFeatureFlagStatus.Setup(x => x.GetFeatureFlagStatusAsync(It.IsAny<FeatureFlagStatusInputParams>()))
            .ReturnsAsync( ( new FeatureFlagStatusModel(), validationErrors));

        var result = await _sut.GetFeatureFlagStatusAsync(new FeatureFlagStatusQueryParameters());
        var objectResult = result as BadRequestObjectResult;

        using (new AssertionScope())
        {
            objectResult.Should().NotBeNull();
            objectResult!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            objectResult!.Value!.Should().NotBeNull();
            objectResult!.Value!.ToString().Should().NotBeNullOrWhiteSpace();
        }
    }
}
