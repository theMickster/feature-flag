using FeatureFlag.Application.Services.RulesEngine;
using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RuleType;
using FeatureFlag.UnitTests.Setup;

namespace FeatureFlag.UnitTests.Application.Services.RulesEngine.Logic;

[ExcludeFromCodeCoverage]
public sealed class AlwaysEnabledRuleTests : UnitTestBase
{
    private AlwaysEnabledRule _sut;
    private readonly RuleInput _model = new()
    {
        EvaluationDate = DateTime.UtcNow,
        Rule = new RuleModel()
        {
            AllowRule = true,
            Parameters = new RuleParameterModel(),
            Priority = 1,
            RuleType = new RuleTypeModel()
            
        }
    };

    public AlwaysEnabledRuleTests()
    {
        _sut = new AlwaysEnabledRule(_model);
    }

    [Fact]
    public void Run_should_return_on()
    {
        var result = _sut.Run();

        result.Should().Be(RuleResultTypeEnum.On);
    }

    [Fact]
    public void RuleTypeId_is_correct()
    {
        _sut = new AlwaysEnabledRule(_model);
        _sut.RuleTypeId.Should().Be(new Guid("b1b66940-68c3-4bd2-b99e-0538a08de10f"));
    }
}
