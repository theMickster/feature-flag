using AutoMapper;
using FeatureFlag.Application.Data;
using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Application.Services;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Domain.Profiles;
using FeatureFlag.UnitTests.Setup;
using FeatureFlag.UnitTests.Setup.Fixtures;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;

namespace FeatureFlag.UnitTests.Application.Services;

[ExcludeFromCodeCoverage]
public sealed class ReadFeatureFlagConfigServiceTests : UnitTestBase, IClassFixture<FeatureFlagDbContextFixture>
{
    private readonly IMapper _mapper;
    private ReadFeatureFlagConfigService _sut;
    private readonly Mock<FeatureFlagDbContext> _mockDbContext;
    private readonly Mock<IDbContextFactory<FeatureFlagDbContext>> _mockDbContextFactory = new();

    public ReadFeatureFlagConfigServiceTests(FeatureFlagDbContextFixture fixture)
    {
        var mapConfig = new MapperConfiguration(config =>
            config.AddMaps(typeof(FeatureFlagConfigEntityToModelProfile).Assembly)
        );
        mapConfig.AssertConfigurationIsValid();
        _mapper = mapConfig.CreateMapper();

        _mockDbContext = new Mock<FeatureFlagDbContext>();

        _mockDbContext.SetupGet(x => x.FeatureFlagConfigurations)
            .Returns(fixture.GetFeatureFlagConfigEntities().AsQueryable().BuildMockDbSet().Object);

        _mockDbContext.SetupGet(x => x.FeatureFlags)
            .Returns(fixture.GetFeatureFlagEntities().AsQueryable().BuildMockDbSet().Object);

        _mockDbContextFactory.Setup(x => x.CreateDbContext()).Returns(_mockDbContext.Object);
        _mockDbContextFactory.Setup(x => x.CreateDbContextAsync(default)).ReturnsAsync(_mockDbContext.Object);

        _sut = new ReadFeatureFlagConfigService(_mapper, _mockDbContextFactory.Object);
    }
    
    [Fact]
    public void Type_has_correct_structure()
    {
        using (new AssertionScope())
        {
            typeof(ReadFeatureFlagConfigService)
                .Should().Implement<IReadFeatureFlagConfigService>();

            typeof(ReadFeatureFlagConfigService)
                .IsDefined(typeof(ServiceLifetimeScopedAttribute), false)
                .Should().BeTrue();
        }
    }

    [Fact]
    public void constructor_throws_correct_exceptions()
    {
        using (new AssertionScope())
        {
            _ = ((Action)(() => _sut = new ReadFeatureFlagConfigService(
                    null!,
                    _mockDbContextFactory.Object)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("mapper");

            _ = ((Action)(() => _sut = new ReadFeatureFlagConfigService(
                    _mapper,
                    null!)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("dbContextFactory");
        }
    }

    [Fact]
    public async Task GetById_returns_null_succeedsAsync()
    {
        var result = await _sut.GetByIdAsync(DoesNotExistGuid001, DoesNotExistGuid002);
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetById_returns_correct_model_succeedsAsync()
    {
        var result = await _sut.GetByIdAsync(new Guid("525fc686-673b-4787-b34d-101548ca6af3"), new Guid("5e1f9ed6-ddde-4d9e-8771-a613e2066430"));

        using (new AssertionScope())
        {
            result!.Should().NotBeNull();
            result.Name.Should().Be("Test FeatureFlagConfigEntity 003");
            result.Applications.Count.Should().Be(2);
            result.Environments.Count.Should().Be(3);
            result.Rules.Count.Should().Be(1);
            
            var onlyRule = result.Rules[0]!;
            onlyRule.RuleType.Id.Should().Be(new Guid("63adc07a-3793-4b9c-ab49-7ad3e805d3ef"));
            onlyRule.Parameters.DateRange.Should().NotBeNull();
            onlyRule.Parameters.DateRange.StartDate.Should().Be(new DateTime(2023, 10, 01));
            onlyRule.Parameters.DateRange.EndDate.Should().Be(new DateTime(2023, 12, 31));

        }
    }
}
