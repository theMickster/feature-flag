using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RulesEngine;
using FeatureFlag.Domain.Models.RuleType;
using FeatureFlag.UnitTests.Setup;

namespace FeatureFlag.UnitTests.Application.Services.RulesEngine.Logic;

[ExcludeFromCodeCoverage]
public sealed class TimeWindowRuleTests : UnitTestBase
{
    private TimeWindowRule _sut;
    private readonly RuleModel _model = new();

    [Fact]
    public void Constructor_throws_correct_null_parameter_exceptions()
    {
        var aTime = new TimeOnly(10, 45, 52);
        
        using (new AssertionScope())
        {
            _ = ((Action)(() => _sut = new TimeWindowRule(
                    null!,
                    Guid.NewGuid(),
                    new List<Guid>(),
                    aTime)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("ruleModel");

            _ = ((Action)(() => _sut = new TimeWindowRule(
                    new RuleModel
                    {
                        Parameters = new RuleParameterModel
                            { DateRange = new DateRangeRuleModel { StartDate = DefaultStartDate, EndDate = DefaultEndDate } }
                    },
                    Guid.NewGuid(),
                    null!,
                    aTime)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("applicationRoles");

            _ = ((Action)(() => _sut = new TimeWindowRule(
                    new RuleModel { Parameters = null },
                    Guid.NewGuid(),
                    new List<Guid>(),
                    aTime)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("ruleModel");
        }
    }


    [Fact]
    public void RuleTypeId_is_correct()
    {
        _model.Parameters = new RuleParameterModel
        {
            TimeRange = new TimeRangeRuleModel
            {
                StartTime = new TimeOnly(8, 00, 00),
                EndTime = new TimeOnly(17, 00, 00)
            }
        };
        _sut = new TimeWindowRule(_model, new Guid(), [], StaticTestTime01);
        _sut.RuleTypeId.Should().Be(new Guid("b51e6e73-7a56-4547-9cb4-3287992cca0c"));
    }

    [Fact]
    public void Run_returns_not_applicable_correctly()
    {
        _model.AllowRule = false;
        _model.Id = Guid.NewGuid();
        _model.RuleType = new RuleTypeModel { Description = "An xunit rule type." };
        _model.Parameters = new RuleParameterModel
        {
            TimeRange = new TimeRangeRuleModel
            {
                StartTime = new TimeOnly(8, 00, 00),
                EndTime = new TimeOnly(17, 00, 00)
            }
        };

        _sut = new TimeWindowRule(_model, new Guid(), [], StaticTestTime01);

        var result = _sut.Run();
        result.Should().Be(RuleResultTypeEnum.NotApplicable);
    }

    [Fact]
    public void Run_returns_off_when_is_disallow_correctly()
    {
        _model.AllowRule = false;
        _model.Id = Guid.NewGuid();
        _model.RuleType = new RuleTypeModel { Description = "An xunit rule type." };
        _model.Parameters = new RuleParameterModel
        {
            TimeRange = new TimeRangeRuleModel
            {
                StartTime = new TimeOnly(18, 00, 00),
                EndTime = new TimeOnly(23, 59, 59)
            }
        };

        _sut = new TimeWindowRule(_model, new Guid(), [], StaticTestTime01);

        var result = _sut.Run();
        result.Should().Be(RuleResultTypeEnum.Off);
    }

    [Fact]
    public void Run_returns_off_when_is_allow_correctly()
    {
        _model.AllowRule = true;
        _model.Id = Guid.NewGuid();
        _model.RuleType = new RuleTypeModel { Description = "An xunit rule type." };
        _model.Parameters = new RuleParameterModel
        {
            TimeRange = new TimeRangeRuleModel
            {
                StartTime = new TimeOnly(00, 30, 00),
                EndTime = new TimeOnly(5, 59, 59)
            }
        };

        _sut = new TimeWindowRule(_model, new Guid(), [], StaticTestTime01);

        var result = _sut.Run();
        result.Should().Be(RuleResultTypeEnum.Off);
    }


    [Fact]
    public void Run_returns_on_when_is_allow_correctly()
    {
        _model.AllowRule = true;
        _model.Id = Guid.NewGuid();
        _model.RuleType = new RuleTypeModel { Description = "An xunit rule type." };
        _model.Parameters = new RuleParameterModel
        {
            TimeRange = new TimeRangeRuleModel
            {
                StartTime = new TimeOnly(20, 30, 00),
                EndTime = new TimeOnly(23, 59, 59)
            }
        };

        _sut = new TimeWindowRule(_model, new Guid(), [], StaticTestTime01);

        var result = _sut.Run();
        result.Should().Be(RuleResultTypeEnum.On);
    }
}
