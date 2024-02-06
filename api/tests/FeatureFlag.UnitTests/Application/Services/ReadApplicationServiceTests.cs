using FeatureFlag.Application.Interfaces.Services;
using FeatureFlag.Application.Services;
using FeatureFlag.Common.Attributes;
using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Settings;
using FeatureFlag.UnitTests.Setup;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FeatureFlag.UnitTests.Application.Services;

[ExcludeFromCodeCoverage]
public sealed class ReadApplicationServiceTests : PersistenceUnitTestBase
{
    private ReadApplicationService _sut;
    private readonly Mock<IOptionsSnapshot<CacheSettings>> _mockOptionsSnapshotConfig = new();
    private readonly MemoryCache _memoryCache;

    public ReadApplicationServiceTests()
    {
        _mockOptionsSnapshotConfig.Setup(i => i.Value)
            .Returns(new CacheSettings
            {
                TimeoutInSeconds = 10
            });
        
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _sut = new ReadApplicationService(Mapper, MockMetadataDbContextFactory.Object, _memoryCache,
            _mockOptionsSnapshotConfig.Object);
    }

    [Fact]
    public void Type_has_correct_structure()
    {
        using (new AssertionScope())
        {
            typeof(ReadApplicationService)
                .Should().Implement<IReadApplicationService>();

            typeof(ReadApplicationService)
                .IsDefined(typeof(ServiceLifetimeScopedAttribute), false)
                .Should().BeTrue();
        }
    }

    [Fact]
    public void constructor_throws_correct_exceptions()
    {
        using (new AssertionScope())
        {
            _ = ((Action)(() => _sut = new ReadApplicationService(
                    null!,
                    MockMetadataDbContextFactory.Object, 
                    _memoryCache,
                    _mockOptionsSnapshotConfig.Object)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("mapper");

            _ = ((Action)(() => _sut = new ReadApplicationService(
                    Mapper,
                    null!,
                    _memoryCache,
                    _mockOptionsSnapshotConfig.Object)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("dbContextFactory");

            _ = ((Action)(() => _sut = new ReadApplicationService(
                    Mapper,
                    MockMetadataDbContextFactory.Object,
                    null!,
                    _mockOptionsSnapshotConfig.Object)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("memoryCache");

            _ = ((Action)(() => _sut = new ReadApplicationService(
                    Mapper,
                    MockMetadataDbContextFactory.Object,
                    _memoryCache,
                    null!)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("cacheSettings");
        }
    }

    [Fact]
    public async Task GetListAsync_succeeds()
    {
        var results = await _sut.GetListAsync();
        var cacheResults = await _sut.GetListAsync();

        using (new AssertionScope())
        {
            results.Count.Should().Be(2);
            results.Count(x => x.Name == "Beers" && x.Id == new Guid("37eb8f87-2b6f-4129-a574-335ab6602138")).Should()
                .Be(1);

            cacheResults.Count.Should().Be(2);
            cacheResults.Count(x => x.Name == "Adventure Works" && x.Id == new Guid("b4b84b57-1b70-4d79-a28b-30a91e3a7444")).Should()
                .Be(1);
        }
    }

    [Fact]
    public async Task GetModelById_succeeds()
    {
        var result = await _sut.GetModelById(new Guid("37eb8f87-2b6f-4129-a574-335ab6602138"));
        using (new AssertionScope())
        {
            result.Should().NotBeNull();
            result!.Name.Should().Be("Beers");
        }
    }

    [Fact]
    public async Task GetModelByName_succeeds()
    {
        var result = await _sut.GetModelByName("Beers");
        using (new AssertionScope())
        {
            result.Should().NotBeNull();
            result!.Id.Should().Be(new Guid("37eb8f87-2b6f-4129-a574-335ab6602138"));
        }
    }

    [Fact]
    public void CacheKey_is_correct()
    {
        _sut.CacheKey.Should().Be(CacheKeyConstants.ApplicationList);
    }

}
