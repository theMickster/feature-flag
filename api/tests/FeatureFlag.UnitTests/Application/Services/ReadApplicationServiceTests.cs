using FeatureFlag.Application.Services;
using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Settings;
using FeatureFlag.UnitTests.Setup;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FeatureFlag.UnitTests.Application.Services;

[ExcludeFromCodeCoverage]
public sealed class ReadApplicationServiceTests : PersistenceUnitTestBase
{
    private readonly ReadApplicationService _sut;
    private readonly Mock<IOptionsSnapshot<CacheSettings>> _mockOptionsSnapshotConfig = new();

    public ReadApplicationServiceTests()
    {
        _mockOptionsSnapshotConfig.Setup(i => i.Value)
            .Returns(new CacheSettings
            {
                TimeoutInSeconds = 10
            });
        
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        _sut = new ReadApplicationService(Mapper, MockMetadataDbContextFactory.Object, memoryCache,
            _mockOptionsSnapshotConfig.Object);
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
