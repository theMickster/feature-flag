using FeatureFlag.Application.Services.RuleEvaluator.Logic;

namespace FeatureFlag.UnitTests.Application.Services.RuleEvaluator.Logic;

[ExcludeFromCodeCoverage]
public sealed class DateWindowRuleTests
{
    private DateWindowRule _sut;

    [Fact]
    public void Constructor_throws_correct_null_parameter_exceptions()
    {
        using (new AssertionScope())
        {
            _ = ((Action)(() => _sut = new DateWindowRule(
                    null!,
                    Guid.NewGuid(),
                    new List<Guid>(),
                    DateTime.Now)))
                .Should().Throw<ArgumentNullException>("because we expect a null argument exception.")
                .And.ParamName.Should().Be("ruleModel");


        }
    }
}
