using FeatureFlag.Common.Utilities.Extensions;
using System.ComponentModel;

namespace FeatureFlag.UnitTests.Common.Utilities.Extensions;

[ExcludeFromCodeCoverage]
public class DateTimeExtensionsTests
{
    [Fact]
    public void IsDateInDateRange_NoTimeComponent_Succeeds_Returns_True()
    {
        var myDate = new DateTime(2016, 04, 26, 05, 29, 00);
        var myStartDate = new DateTime(2016, 04, 01);
        var myEndDate = new DateTime(2016, 04, 30);

        var isInDateRange = myDate.IsDateInDateRange(myStartDate, myEndDate, true);

        Assert.True(isInDateRange);
    }


    [Fact]
    public void IsDateInDateRange_NoTimeComponent_Succeeds_Returns_False()
    {
        var myDate = new DateTime(2016, 04, 26, 05, 29, 00);
        var myStartDate = new DateTime(2016, 05, 01);
        var myEndDate = new DateTime(2016, 05, 31);

        var isInDateRange = myDate.IsDateInDateRange(myStartDate, myEndDate, true);

        Assert.False(isInDateRange);
    }

    [Fact]
    public void IsDateInDateRange_Fails_Throws_Exception()
    {
        var myDate = new DateTime(2016, 04, 26, 05, 29, 00);
        var myStartDate = new DateTime(2016, 04, 30);
        var myEndDate = new DateTime(2016, 04, 01);

        var exception =
            Assert.Throws<InvalidEnumArgumentException>(() => myDate.IsDateInDateRange(myStartDate, myEndDate));
    }

    [Fact]
    public void IsDateInDateRange_with_time_component_returns_true()
    {
        var myDate = new DateTime(2020, 2, 15, 12, 12, 12);
        var startDate = new DateTime(2020, 2, 15, 12, 10, 12);
        var endDate = new DateTime(2020, 2, 15, 12, 14, 12);

        var isInDateRange = myDate.IsDateInDateRange(startDate, endDate, false);

        isInDateRange.Should().BeTrue();
    }

    [Fact]
    public void IsDateInDateRange_with_time_component_returns_false()
    {
        var myDate = new DateTime(2020, 2, 15, 12, 09, 12);
        var startDate = new DateTime(2020, 2, 15, 12, 10, 12);
        var endDate = new DateTime(2020, 2, 15, 12, 14, 12);

        var isInDateRange = myDate.IsDateInDateRange(startDate, endDate, false);

        isInDateRange.Should().BeFalse();
    }

    [Fact]
    public void End_of_day_succeeds()
    {
        var date001 = new DateTime(2019, 02, 18);
        var date002 = new DateTime(2016, 04, 26);
        var date003 = new DateTime(2012, 08, 19);
        var date004 = new DateTime(2015, 03, 21);
        var date005 = new DateTime(2020, 07, 20);
        var date006 = new DateTime(2020, 02, 29);

        using (new AssertionScope())
        {
            date001.EndOfDay().Should().Be(new DateTime(2019, 02, 18, 23, 59, 59, 999));
            date002.EndOfDay().Should().Be(new DateTime(2016, 04, 26, 23, 59, 59, 999));
            date003.EndOfDay().Should().Be(new DateTime(2012, 08, 19, 23, 59, 59, 999));
            date004.EndOfDay().Should().Be(new DateTime(2015, 03, 21, 23, 59, 59, 999));
            date005.EndOfDay().Should().Be(new DateTime(2020, 07, 20, 23, 59, 59, 999));
            date006.EndOfDay().Should().Be(new DateTime(2020, 02, 29, 23, 59, 59, 999));
        }
    }

    [Fact]
    public void End_of_month_succeeds()
    {
        var date001 = new DateTime(2019, 02, 18);
        var date002 = new DateTime(2016, 04, 26);
        var date003 = new DateTime(2012, 08, 19);
        var date004 = new DateTime(2015, 03, 21);
        var date005 = new DateTime(2020, 07, 20);
        var date006 = new DateTime(2020, 02, 29);
        var date007 = new DateTime(2021, 02, 04);
        var date008 = new DateTime(2019, 11, 11);
        var date009 = new DateTime(2008, 12, 25);

        using (new AssertionScope())
        {
            date001.EndOfMonth().Should().Be(new DateTime(2019, 02, 28, 23, 59, 59, 999));
            date002.EndOfMonth().Should().Be(new DateTime(2016, 04, 30, 23, 59, 59, 999));
            date003.EndOfMonth().Should().Be(new DateTime(2012, 08, 31, 23, 59, 59, 999));
            date004.EndOfMonth().Should().Be(new DateTime(2015, 03, 31, 23, 59, 59, 999));
            date005.EndOfMonth().Should().Be(new DateTime(2020, 07, 31, 23, 59, 59, 999));
            date006.EndOfMonth().Should().Be(new DateTime(2020, 02, 29, 23, 59, 59, 999));
            date007.EndOfMonth().Should().Be(new DateTime(2021, 02, 28, 23, 59, 59, 999));
            date008.EndOfMonth().Should().Be(new DateTime(2019, 11, 30, 23, 59, 59, 999));
            date009.EndOfMonth().Should().Be(new DateTime(2008, 12, 31, 23, 59, 59, 999));
        }
    }

    [Fact]
    public void End_of_year_succeeds()
    {
        var date001 = new DateTime(2019, 02, 18);
        var date002 = new DateTime(2016, 04, 26);
        var date003 = new DateTime(2012, 08, 19);
        var date004 = new DateTime(2015, 03, 21);
        var date005 = new DateTime(2020, 07, 20);
        var date006 = new DateTime(2020, 02, 29);
        var date007 = new DateTime(2021, 02, 28);
        var date008 = new DateTime(2019, 11, 11);
        var date009 = new DateTime(2008, 12, 25);

        using (new AssertionScope())
        {
            date001.EndOfYear().Should().Be(new DateTime(2019, 12, 31, 23, 59, 59, 999));
            date002.EndOfYear().Should().Be(new DateTime(2016, 12, 31, 23, 59, 59, 999));
            date003.EndOfYear().Should().Be(new DateTime(2012, 12, 31, 23, 59, 59, 999));
            date004.EndOfYear().Should().Be(new DateTime(2015, 12, 31, 23, 59, 59, 999));
            date005.EndOfYear().Should().Be(new DateTime(2020, 12, 31, 23, 59, 59, 999));
            date006.EndOfYear().Should().Be(new DateTime(2020, 12, 31, 23, 59, 59, 999));
            date007.EndOfYear().Should().Be(new DateTime(2021, 12, 31, 23, 59, 59, 999));
            date008.EndOfYear().Should().Be(new DateTime(2019, 12, 31, 23, 59, 59, 999));
            date009.EndOfYear().Should().Be(new DateTime(2008, 12, 31, 23, 59, 59, 999));
        }
    }

    [Fact]
    public void Start_of_day_succeeds()
    {
        var date001 = new DateTime(2019, 02, 18);
        var date002 = new DateTime(2016, 04, 26);
        var date003 = new DateTime(2012, 08, 19);
        var date004 = new DateTime(2015, 03, 21);
        var date005 = new DateTime(2020, 07, 20);
        var date006 = new DateTime(2020, 02, 29);
        var date007 = new DateTime(2021, 02, 28);
        var date008 = new DateTime(2019, 11, 11);
        var date009 = new DateTime(2008, 12, 25);

        using (new AssertionScope())
        {
            date001.StartOfDay().Should().Be(new DateTime(2019, 02, 18, 00, 00, 00, 000));
            date002.StartOfDay().Should().Be(new DateTime(2016, 04, 26, 00, 00, 00, 000));
            date003.StartOfDay().Should().Be(new DateTime(2012, 08, 19, 00, 00, 00, 000));
            date004.StartOfDay().Should().Be(new DateTime(2015, 03, 21, 00, 00, 00, 000));
            date005.StartOfDay().Should().Be(new DateTime(2020, 07, 20, 00, 00, 00, 000));
            date006.StartOfDay().Should().Be(new DateTime(2020, 02, 29, 00, 00, 00, 000));
            date007.StartOfDay().Should().Be(new DateTime(2021, 02, 28, 00, 00, 00, 000));
            date008.StartOfDay().Should().Be(new DateTime(2019, 11, 11, 00, 00, 00, 000));
            date009.StartOfDay().Should().Be(new DateTime(2008, 12, 25, 00, 00, 00, 000));
        }
    }

    [Fact]
    public void Start_of_month_succeeds()
    {
        var date001 = new DateTime(2019, 02, 18);
        var date002 = new DateTime(2016, 04, 26);
        var date003 = new DateTime(2012, 08, 19);
        var date004 = new DateTime(2015, 03, 21);
        var date005 = new DateTime(2020, 07, 20);
        var date006 = new DateTime(2020, 02, 29);
        var date007 = new DateTime(2021, 02, 28);
        var date008 = new DateTime(2019, 11, 11);
        var date009 = new DateTime(2008, 12, 25);

        using (new AssertionScope())
        {
            date001.StartOfMonth().Should().Be(new DateTime(2019, 02, 01, 00, 00, 00, 000));
            date002.StartOfMonth().Should().Be(new DateTime(2016, 04, 01, 00, 00, 00, 000));
            date003.StartOfMonth().Should().Be(new DateTime(2012, 08, 01, 00, 00, 00, 000));
            date004.StartOfMonth().Should().Be(new DateTime(2015, 03, 01, 00, 00, 00, 000));
            date005.StartOfMonth().Should().Be(new DateTime(2020, 07, 01, 00, 00, 00, 000));
            date006.StartOfMonth().Should().Be(new DateTime(2020, 02, 01, 00, 00, 00, 000));
            date007.StartOfMonth().Should().Be(new DateTime(2021, 02, 01, 00, 00, 00, 000));
            date008.StartOfMonth().Should().Be(new DateTime(2019, 11, 01, 00, 00, 00, 000));
            date009.StartOfMonth().Should().Be(new DateTime(2008, 12, 01, 00, 00, 00, 000));
        }
    }

    [Fact]
    public void Start_of_year_succeeds()
    {
        var date001 = new DateTime(2019, 02, 18);
        var date002 = new DateTime(2016, 04, 26);
        var date003 = new DateTime(2012, 08, 19);
        var date004 = new DateTime(2015, 03, 21);
        var date005 = new DateTime(2020, 07, 20);
        var date006 = new DateTime(2020, 02, 29);
        var date007 = new DateTime(2021, 02, 28);
        var date008 = new DateTime(2019, 11, 11);
        var date009 = new DateTime(2008, 12, 25);

        using (new AssertionScope())
        {
            date001.StartOfYear().Should().Be(new DateTime(2019, 01, 01, 00, 00, 00, 000));
            date002.StartOfYear().Should().Be(new DateTime(2016, 01, 01, 00, 00, 00, 000));
            date003.StartOfYear().Should().Be(new DateTime(2012, 01, 01, 00, 00, 00, 000));
            date004.StartOfYear().Should().Be(new DateTime(2015, 01, 01, 00, 00, 00, 000));
            date005.StartOfYear().Should().Be(new DateTime(2020, 01, 01, 00, 00, 00, 000));
            date006.StartOfYear().Should().Be(new DateTime(2020, 01, 01, 00, 00, 00, 000));
            date007.StartOfYear().Should().Be(new DateTime(2021, 01, 01, 00, 00, 00, 000));
            date008.StartOfYear().Should().Be(new DateTime(2019, 01, 01, 00, 00, 00, 000));
            date009.StartOfYear().Should().Be(new DateTime(2008, 01, 01, 00, 00, 00, 000));
        }
    }

    [Fact]
    public void TruncateMinutesAndSeconds_Fact_Succeeds()
    {
        var myActualDate = new DateTime(2001, 12, 25, 15, 30, 14);
        var myExpectedDate = new DateTime(2001, 12, 25, 15, 0, 0);

        myActualDate = myActualDate.TruncateMinutesAndSeconds();

        Assert.Equal(myExpectedDate, myActualDate);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void TruncateMinutesAndSeconds_Theory_Succeeds(DateTime actual, DateTime expected)
    {
        actual = actual.TruncateMinutesAndSeconds();

        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { new DateTime(2001, 12, 25, 15, 30, 14), new DateTime(2001, 12, 25, 15, 0, 0) },
            new object[] { new DateTime(2002, 02, 15, 12, 01, 35), new DateTime(2002, 02, 15, 12, 0, 0) },
            new object[] { new DateTime(2003, 03, 14, 18, 15, 03), new DateTime(2003, 03, 14, 18, 0, 0) }
        };
}
