using FeatureFlag.Application.Services.RulesEngine;
using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RuleType;
using FeatureFlag.UnitTests.Setup;

namespace FeatureFlag.UnitTests.Application.Services.RulesEngine.Logic;

[ExcludeFromCodeCoverage]
public sealed class AlwaysDisabledRuleTests : UnitTestBase
{
    private AlwaysDisabledRule _sut;
    private readonly RuleInput _model = new()
    {
        EvaluationDate = DateTime.UtcNow,
        Rule = new RuleModel()
        {
            AllowRule = false,
            Parameters = new RuleParameterModel(),
            Priority = 1,
            RuleType = new RuleTypeModel()

        }
    };

    public AlwaysDisabledRuleTests()
    {
        _sut = new AlwaysDisabledRule(_model);
    }
    
    [Fact]
    public void Run_should_return_off()
    {
        var result = _sut.Run();
        
        result.Should().Be(RuleResultTypeEnum.Off);
    }

    [Fact]
    public void RuleTypeId_is_correct()
    {
        _sut = new AlwaysDisabledRule(_model);
        _sut.RuleTypeId.Should().Be(new Guid("26d17b10-dc3a-4e74-97b1-e19eec79f6a0"));
    }
}
