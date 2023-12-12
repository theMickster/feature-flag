using FeatureFlag.Application.Interfaces.Services.RuleEvaluator;
using FeatureFlag.Application.Services.RulesEngine;
using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RulesEngine;
using FeatureFlag.UnitTests.Setup;

namespace FeatureFlag.UnitTests.Application.Services.RulesEngine;

[ExcludeFromCodeCoverage]
public sealed class RulesEngineServiceTests : UnitTestBase
{
    private readonly Mock<IRuleFactory> _mockRuleFactory = new();
    private RulesEngineService _sut;

    public RulesEngineServiceTests()
    {
        _mockRuleFactory.Setup(x => x.BuildRules(It.IsAny<RulesEngineInputModel>()))
            .Returns(new List<IRule>());
        
        _sut = new RulesEngineService(_mockRuleFactory.Object);
    }

    [Fact]
    public void Constructor_throws_correct_null_parameter_exceptions()
    {
        _ = ((Action)(() => _sut = new RulesEngineService(null!)))
            .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
            .And.ParamName.Should().Be("ruleFactory");
    }

    [Fact]
    public void Run_should_return_off_when_rule_set_has_AlwaysDisabledRule()
    {
        var input = new RulesEngineInputModel();
        var ruleSet = new List<IRule> { new AlwaysDisabledRule(new RuleModel(), Guid.NewGuid(), []) };
        _mockRuleFactory.Setup(x => x.BuildRules(input)).Returns(ruleSet);
        
        var result = _sut.Run(input);
        
        result.Outcome.Should().Be(RuleResultTypeEnum.Off);
    }
    
    [Fact]
    public void Run_should_return_on_when_rule_set_has_AlwaysEnabledRule()
    {
        var input = new RulesEngineInputModel();
        var ruleSet = new List<IRule> { new AlwaysEnabledRule(new RuleModel(), Guid.NewGuid(), []) };
        _mockRuleFactory.Setup(x => x.BuildRules(input)).Returns(ruleSet);
        
        var result = _sut.Run(input);
        
        result.Outcome.Should().Be(RuleResultTypeEnum.On);
    }

    [Theory]
    [ClassData(typeof(RulesForTests))]
    public void Run_should_return_correct_outcome(List<IRule> rules, RuleResultTypeEnum expectedOutcome)
    {
        var input = new RulesEngineInputModel();
        _mockRuleFactory.Setup(x => x.BuildRules(input)).Returns(rules);
        
        var result = _sut.Run(input);
        
        result.Outcome.Should().Be(expectedOutcome);
    }

    public class RulesForTests : TheoryData<List<IRule>, RuleResultTypeEnum>
    {
        public RulesForTests()
        {
            Add([new SomeRuleThatReturnsOn(new RuleModel(), Guid.NewGuid(), [])], RuleResultTypeEnum.On);
            
            Add([new SomeRuleThatReturnsOff(new RuleModel(), Guid.NewGuid(), [])], RuleResultTypeEnum.Off);
            
            Add([new SomeRuleThatReturnsNotApplicable(new RuleModel(), Guid.NewGuid(), [])], RuleResultTypeEnum.NotApplicable);
            
            Add([new SomeRuleThatReturnsOn(new RuleModel(), Guid.NewGuid(), []),
                new SomeRuleThatReturnsOff(new RuleModel(), Guid.NewGuid(), []),
                new SomeRuleThatReturnsNotApplicable(new RuleModel(), Guid.NewGuid(), []),
                new SomeRuleThatReturnsOn(new RuleModel(), Guid.NewGuid(), [])], RuleResultTypeEnum.On);

            Add([new SomeRuleThatReturnsNotApplicable(new RuleModel(), Guid.NewGuid(), []),
                new SomeRuleThatReturnsOn(new RuleModel(), Guid.NewGuid(), []),
                new SomeRuleThatReturnsOff(new RuleModel(), Guid.NewGuid(), []),
                new SomeRuleThatReturnsOff(new RuleModel(), Guid.NewGuid(), [])], RuleResultTypeEnum.Off);
            
            Add([new SomeRuleThatReturnsNotApplicable(new RuleModel(), Guid.NewGuid(), []),
                new SomeRuleThatReturnsOn(new RuleModel(), Guid.NewGuid(), []),
                new SomeRuleThatReturnsOff(new RuleModel(), Guid.NewGuid(), []),
                new SomeRuleThatReturnsNotApplicable(new RuleModel(), Guid.NewGuid(), [])], RuleResultTypeEnum.NotApplicable);
        }
    }


    private class SomeRuleThatReturnsOn (RuleModel ruleModel, Guid applicationUserId, List<Guid>? applicationRoles)
        : RuleBase(ruleModel, applicationUserId, applicationRoles)
    {
        public override Guid RuleTypeId => new("1ac5029e-267b-4b25-974d-476050ecea6b");

        public override RuleResultTypeEnum Run() => RuleResultTypeEnum.On;
    }

    private class SomeRuleThatReturnsOff(RuleModel ruleModel, Guid applicationUserId, List<Guid>? applicationRoles)
        : RuleBase(ruleModel, applicationUserId, applicationRoles)
    {
        public override Guid RuleTypeId => new("f2f9bfdd-f0ca-44ec-a537-bb1b091e1830");

        public override RuleResultTypeEnum Run() => RuleResultTypeEnum.Off;
    }
    
    private class SomeRuleThatReturnsNotApplicable(RuleModel ruleModel, Guid applicationUserId, List<Guid>? applicationRoles)
        : RuleBase(ruleModel, applicationUserId, applicationRoles)
    {
        public override Guid RuleTypeId => new("164645c2-148a-4397-b2cc-e5326bcf08e5");

        public override RuleResultTypeEnum Run() => RuleResultTypeEnum.NotApplicable;
    }
}
