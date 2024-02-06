using FeatureFlag.Application.Services.RulesEngine;
using FeatureFlag.Application.Services.RulesEngine.Logic;
using FeatureFlag.Domain.Models.Rule;
using FeatureFlag.Domain.Models.RuleType;

namespace FeatureFlag.UnitTests.Application.Services.RulesEngine;

[ExcludeFromCodeCoverage]
public sealed class RuleFactoryTests
{
    private readonly RuleFactory _sut = new();

    [Fact]
    public void BuildRules_throws_correct_exceptions()
    {
        using (new AssertionScope())
        {
            _ = ((Action)(() => _sut.BuildRules(null!)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("input");

            _ = ((Action)(() => _sut.BuildRules(new RulesEngineInput())))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("Rules");

            _ = ((Action)(() => _sut.BuildRules(new RulesEngineInput{Rules = [] })))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("Rules");
        }
    }

    [Fact]
    public void CreateRule_throws_correct_exceptions()
    {
        using (new AssertionScope())
        {
            _ = ((Action)(() => _sut.BuildRules(new RulesEngineInput
                {
                    Rules = [
                        new ()
                        {
                            Id = Guid.NewGuid(),
                            AllowRule = true,
                            RuleType = null!
                        }
                    ]
                })))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception when the rule type is null.")
                .And.ParamName.Should().Be("ruleInput");
            
            _ = ((Action)(() => _sut.BuildRules(new RulesEngineInput { Rules = [new()
                {
                    AllowRule = true,
                    RuleType = new RuleTypeModel
                    {
                        Name = "Hello Unit Test",
                        Id = new Guid("e16bc541-8027-4644-b121-4e801735f09a")
                    }
                }] })))
                .Should().Throw<NotImplementedException>("because we expect a not implemented exception when the rule type is not supported.");
        }
    }

    [Fact]
    public void CreateRule_returns_a_valid_alwaysDisabled_rule()
    {
        var model = new RulesEngineInput
        {
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = new List<Guid>(),
            EvaluationDate = DateTime.UtcNow,
            Rules =
            [
                new()
                {
                    Id = Guid.NewGuid(),
                    AllowRule = false,
                    RuleType = new RuleTypeModel
                    {
                        Id = new Guid("26d17b10-dc3a-4e74-97b1-e19eec79f6a0"),
                        Name = "AlwaysDisabled",
                        Description = "The rule should ensure that the feature flag is always disabled"
                    }
                }
            ]
        };
        
        var results = _sut.BuildRules(model).ToList().AsReadOnly();

        using (new AssertionScope())
        {
            results.Count.Should().Be(1);
            results[0].RuleTypeId.Should().Be(new Guid("26d17b10-dc3a-4e74-97b1-e19eec79f6a0"));
            results[0].Should().BeOfType<AlwaysDisabledRule>();
        }
    }

    [Fact]
    public void CreateRule_returns_a_valid_alwaysEnabled_rule()
    {
        var model = new RulesEngineInput
        {
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = new List<Guid>(),
            EvaluationDate = DateTime.UtcNow,
            Rules =
            [
                new()
                {
                    Id = Guid.NewGuid(),
                    AllowRule = true,
                    RuleType = new RuleTypeModel
                    {
                        Id = new Guid("b1b66940-68c3-4bd2-b99e-0538a08de10f"),
                        Name = "AlwaysDisabled",
                        Description = "The rule should ensure that the feature flag is always disabled"
                    }
                }
            ]
        };

        var results = _sut.BuildRules(model).ToList().AsReadOnly();

        using (new AssertionScope())
        {
            results.Count.Should().Be(1);
            results[0].RuleTypeId.Should().Be(new Guid("b1b66940-68c3-4bd2-b99e-0538a08de10f"));
            results[0].Should().BeOfType<AlwaysEnabledRule>();
        }
    }

    [Fact]
    public void CreateRule_returns_a_valid_dateWindow_rule()
    {
        var model = new RulesEngineInput
        {
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = new List<Guid>(),
            EvaluationDate = DateTime.UtcNow,
            Rules =
            [
                new()
                {
                    Id = Guid.NewGuid(),
                    AllowRule = true,
                    RuleType = new RuleTypeModel
                    {
                        Id = new Guid("63adc07a-3793-4b9c-ab49-7ad3e805d3ef"),
                        Name = "A unit test date window rule",
                        Description = "The rule should ensure that the feature flag is always disabled"
                    },
                    Parameters = new RuleParameterModel
                    {
                        DateRange = new DateRangeRuleModel
                        {
                            StartDate = DateTime.UtcNow.AddDays(-5),
                            EndDate= DateTime.UtcNow.AddDays(+5)
                        }
                    }
                }
            ]
        };

        var results = _sut.BuildRules(model).ToList().AsReadOnly();

        using (new AssertionScope())
        {
            results.Count.Should().Be(1);
            results[0].RuleTypeId.Should().Be(new Guid("63adc07a-3793-4b9c-ab49-7ad3e805d3ef"));
            results[0].Should().BeOfType<DateWindowRule>();
        }
    }

    [Fact]
    public void CreateRule_returns_a_valid_timeWindow_rule()
    {
        var model = new RulesEngineInput
        {
            ApplicationUserId = Guid.NewGuid(),
            ApplicationUserRoles = new List<Guid>(),
            EvaluationDate = DateTime.UtcNow,
            Rules =
            [
                new()
                {
                    Id = Guid.NewGuid(),
                    AllowRule = true,
                    RuleType = new RuleTypeModel
                    {
                        Id = new Guid("b51e6e73-7a56-4547-9cb4-3287992cca0c"),
                        Name = "Special Unit Test Time Rule",
                        Description = "The rule should ensure that the feature flag is always disabled"
                    },
                    Parameters = new RuleParameterModel
                    {
                        DateRange = new DateRangeRuleModel
                        {
                            StartDate = DateTime.UtcNow.AddDays(-5),
                            EndDate = DateTime.UtcNow.AddDays(+5)
                        },
                        TimeRange = new TimeRangeRuleModel()
                        {
                            StartTime = new TimeOnly(11, 11, 11),
                            EndTime = new TimeOnly(12, 12, 12)
                        }
                    }
                }
            ]
        };

        var results = _sut.BuildRules(model).ToList().AsReadOnly();

        using (new AssertionScope())
        {
            results.Count.Should().Be(1);
            results[0].RuleTypeId.Should().Be(new Guid("b51e6e73-7a56-4547-9cb4-3287992cca0c"));
            results[0].Should().BeOfType<TimeWindowRule>();
        }
    }
}
