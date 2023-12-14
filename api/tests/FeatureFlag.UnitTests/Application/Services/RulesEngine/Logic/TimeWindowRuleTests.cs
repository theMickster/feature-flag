using FeatureFlag.Application.Services.RulesEngine;
using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RuleType;
using FeatureFlag.UnitTests.Setup;

namespace FeatureFlag.UnitTests.Application.Services.RulesEngine.Logic;

[ExcludeFromCodeCoverage]
public sealed class TimeWindowRuleTests : UnitTestBase
{
    private TimeWindowRule _sut;
    private RuleInput _model = new();

    [Fact]
    public void Constructor_throws_correct_null_parameter_exception_when_input_is_null()
    {
        using (new AssertionScope())
        {
            _ = ((Action)(() => _sut = new TimeWindowRule(null!)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("input");
        }
    }
    
    [Fact]
    public void Constructor_throws_correct_null_parameter_exception_when_rule_is_null()
    {
        var ruleInput = new RuleInput
        {
            Rule = null!,
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = DateTime.Now
        };

        _ = ((Action)(() => _sut = new TimeWindowRule(ruleInput)))
            .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
            .And.ParamName.Should().Be("Rule");
    }

    [Fact]
    public void Constructor_throws_correct_null_parameter_exception_when_parameters_is_null()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                AllowRule = true,
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = DateTime.Now
        };

        _ = ((Action)(() => _sut = new TimeWindowRule(_model)))
            .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
            .And.ParamName.Should().Be("TimeRange");
    }
    
    [Fact]
    public void Constructor_throws_correct_null_parameter_exception_when_time_range_is_null()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                AllowRule = true,
                Parameters = new RuleParameterModel()
                {
                    TimeRange = null
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = DateTime.Now
        };

        _ = ((Action)(() => _sut = new TimeWindowRule(_model)))
            .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
            .And.ParamName.Should().Be("TimeRange");
    }

    [Fact]
    public void Constructor_throws_correct_null_parameter_exception_when_application_roles_is_null()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                AllowRule = true,
                Parameters = new RuleParameterModel()
                {
                    TimeRange = new TimeRangeRuleModel
                    {
                        StartTime = new TimeOnly(8, 00, 00),
                        EndTime = new TimeOnly(17, 00, 00)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = null!,
            EvaluationDate = DateTime.Now
        };

        _ = ((Action)(() => _sut = new TimeWindowRule(_model)))
            .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
            .And.ParamName.Should().Be("ApplicationUserRoles");
    }
    
    [Fact]
    public void RuleTypeId_is_correct()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                AllowRule = true,
                Parameters = new RuleParameterModel
                {
                    TimeRange = new TimeRangeRuleModel
                    {
                        StartTime = new TimeOnly(8, 00, 00),
                        EndTime = new TimeOnly(17, 00, 00)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate02
        };

        _sut = new TimeWindowRule(_model);
        _sut.RuleTypeId.Should().Be(new Guid("b51e6e73-7a56-4547-9cb4-3287992cca0c"));
    }

    [Fact]
    public void Run_returns_not_applicable_correctly()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                RuleType = new RuleTypeModel { Description = "An xunit rule type." },
                AllowRule = false,
                Parameters = new RuleParameterModel
                {
                    TimeRange = new TimeRangeRuleModel
                    {
                        StartTime = new TimeOnly(8, 00, 00),
                        EndTime = new TimeOnly(17, 00, 00)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate02
        };

        _sut = new TimeWindowRule(_model);

        var result = _sut.Run();
        result.Should().Be(RuleResultTypeEnum.NotApplicable);
    }

    [Fact]
    public void Run_returns_off_when_is_disallow_correctly()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                RuleType = new RuleTypeModel { Description = "An xunit rule type." },
                AllowRule = false,
                Parameters = new RuleParameterModel
                {
                    TimeRange = new TimeRangeRuleModel
                    {
                        StartTime = new TimeOnly(18, 00, 00),
                        EndTime = new TimeOnly(23, 59, 59)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate02
        };

        _sut = new TimeWindowRule(_model);

        var result = _sut.Run();
        result.Should().Be(RuleResultTypeEnum.Off);
    }

    [Fact]
    public void Run_returns_off_when_is_allow_correctly()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                RuleType = new RuleTypeModel { Description = "An xunit rule type." },
                AllowRule = true,
                Parameters = new RuleParameterModel
                {
                    TimeRange = new TimeRangeRuleModel
                    {
                        StartTime = new TimeOnly(00, 30, 00),
                        EndTime = new TimeOnly(5, 59, 59)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate02
        };

        _sut = new TimeWindowRule(_model);

        var result = _sut.Run();
        result.Should().Be(RuleResultTypeEnum.Off);
    }

    [Fact]
    public void Run_returns_on_when_is_allow_correctly()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                RuleType = new RuleTypeModel { Description = "An xunit rule type." },
                AllowRule = true,
                Parameters = new RuleParameterModel
                {
                    TimeRange = new TimeRangeRuleModel
                    {
                        StartTime = new TimeOnly(20, 30, 00),
                        EndTime = new TimeOnly(23, 59, 59)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate02
        };

        _sut = new TimeWindowRule(_model);

        var result = _sut.Run();
        result.Should().Be(RuleResultTypeEnum.On);
    }
}
