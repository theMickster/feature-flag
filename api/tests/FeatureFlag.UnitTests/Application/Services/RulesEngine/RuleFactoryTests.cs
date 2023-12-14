using FeatureFlag.Application.Services.RulesEngine;
using FeatureFlag.Domain.Models.Rule;

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
}
