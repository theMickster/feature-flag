using FeatureFlag.Application.Services.RulesEngine;
using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RuleType;
using FeatureFlag.UnitTests.Setup;

namespace FeatureFlag.UnitTests.Application.Services.RulesEngine.Logic;

[ExcludeFromCodeCoverage]
public sealed class DateWindowRuleTests : UnitTestBase
{
    private DateWindowRule _sut;
    private RuleInput _model = new();

    [Fact] 
    public void Constructor_throws_correct_null_parameter_exception_when_input_is_null()
    {
        using (new AssertionScope())
        {
            _ = ((Action)(() => _sut = new DateWindowRule(null!)))
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

        _ = ((Action)(() => _sut = new DateWindowRule(ruleInput)))
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

        _ = ((Action)(() => _sut = new DateWindowRule(_model)))
            .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
            .And.ParamName.Should().Be("DateRange");
    }

    [Fact]
    public void Constructor_throws_correct_null_parameter_exception_when_date_range_is_null()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                AllowRule = true,
                Parameters = new RuleParameterModel()
                {
                    DateRange = null
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = DateTime.Now
        };

        _ = ((Action)(() => _sut = new DateWindowRule(_model)))
            .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
            .And.ParamName.Should().Be("DateRange");
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
                    DateRange = new DateRangeRuleModel
                    {
                        StartDate = new DateTime(2023, 10, 1),
                        EndDate = new DateTime(2023, 10, 31)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate01
        };

        _sut = new DateWindowRule(_model);
        _sut.RuleTypeId.Should().Be(new Guid("63adc07a-3793-4b9c-ab49-7ad3e805d3ef"));
    }

    [Fact]
    public void Run_returns_not_applicable_correctly()
    {
        _model = new RuleInput
        {
            Rule = new RuleModel
            {
                Id = Guid.NewGuid(),
                AllowRule = false,
                RuleType = new RuleTypeModel { Description = "An xunit rule type." },
                Parameters = new RuleParameterModel
                {
                    DateRange = new DateRangeRuleModel
                    {
                        StartDate = new DateTime(2023, 10, 1),
                        EndDate = new DateTime(2023, 10, 31)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate01
        };
;
        _sut = new DateWindowRule(_model);
        
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
                Id = Guid.NewGuid(),
                AllowRule = false,
                RuleType = new RuleTypeModel { Description = "An xunit rule type." },
                Parameters = new RuleParameterModel
                {
                    DateRange = new DateRangeRuleModel
                    {
                        StartDate = new DateTime(2023, 11, 1),
                        EndDate = new DateTime(2023, 11, 30)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate01
        };

        _sut = new DateWindowRule(_model);

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
                Id = Guid.NewGuid(),
                AllowRule = true,
                RuleType = new RuleTypeModel { Description = "An xunit rule type." },
                Parameters = new RuleParameterModel
                {
                    DateRange = new DateRangeRuleModel
                    {
                        StartDate = new DateTime(2023, 09, 1),
                        EndDate = new DateTime(2023, 09, 30)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate01
        };

        _sut = new DateWindowRule(_model);

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
                Id = Guid.NewGuid(),
                AllowRule = true,
                RuleType = new RuleTypeModel { Description = "An xunit rule type." },
                Parameters = new RuleParameterModel
                {
                    DateRange = new DateRangeRuleModel
                    {
                        StartDate = new DateTime(2023, 11, 1),
                        EndDate = new DateTime(2023, 11, 30)
                    }
                }
            },
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = [],
            EvaluationDate = StaticTestDate01
        };

        _sut = new DateWindowRule(_model);

        var result = _sut.Run();
        result.Should().Be(RuleResultTypeEnum.On);
    }
}
