using System.ComponentModel;

namespace FeatureFlag.Common.Utilities.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Extension method used to remove the minute & second components from a date.
    /// Most helpful when 
    /// </summary>
    /// <param name="date">A DateTime</param>
    /// <returns>A datetime with the minute and second components set to 0.</returns>
    /// <example>
    /// <code>
    /// var myDate = DateTime(2000, 07, 15, 12, 30, 55);
    /// var myDateTruncated = myDate.TruncateMinutesAndSeconds();
    /// </code>
    /// </example>
    public static DateTime TruncateMinutesAndSeconds(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
    }

    /// <summary>
    /// Extension method used to determine if one date falls within a date range.
    /// </summary>
    /// <param name="currentDate">Date to be evaluated</param>
    /// <param name="beginDate">Begin (or start date) of date range</param>
    /// <param name="endDate">End date of date range</param>
    /// <param name="removeTimeComponent">Optional; Will remove time component from currentDate.</param>
    /// <returns>A boolean whether the date falls within the range or not.</returns>
    public static bool IsDateInDateRange(this DateTime currentDate, DateTime beginDate, DateTime endDate, bool removeTimeComponent = true)
    {
        if (beginDate > endDate)
        {
            throw new InvalidEnumArgumentException($"Invalid date range arguments. Begin date '{beginDate}' is after end date {endDate}.");
        }

        if (!removeTimeComponent)
        {
            return (currentDate >= beginDate && currentDate <= endDate);
        }

        currentDate = currentDate.Date;
        beginDate = beginDate.Date;
        endDate = endDate.Date;

        return (currentDate >= beginDate && currentDate <= endDate);
    }

    /// <summary>
    /// Extension method used to return a DateTime with the time set to "23:59:59:999" which is the last moment of the day.
    /// </summary>
    /// <param name="currentDate">Date to be evaluated</param>
    /// <returns></returns>
    public static DateTime EndOfDay(this DateTime currentDate)
    {
        return new DateTime(currentDate.Year, currentDate.Month, currentDate.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
    }

    /// <summary>
    /// Extension method used to return a DateTime of the last day of the month with the time set to "23:59:59:999" which is the last moment of the last day of the month.
    /// </summary>
    /// <param name="currentDate">Date to be evaluated</param>
    /// <returns>A DateTime of the last month of <param name="currentDate"></returns>
    public static DateTime EndOfMonth(this DateTime currentDate)
    {
        return new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
    }

    /// <summary>
    /// Extension method used to return a DateTime of the last day of the year with the time set to "23:59:59:999" which is the last moment of the year. 
    /// </summary>
    /// <param name="currentDate">Date to be evaluated</param>
    /// <returns>A DateTime of the last month of <param name="currentDate"></returns>
    public static DateTime EndOfYear(this DateTime currentDate)
    {
        return new DateTime(currentDate.Year, 1, 1).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
    }

    /// <summary>
    /// Extension method used to return a DateTime with the time set to "00:00:00:000" which is the first moment of the day.
    /// </summary>
    /// <param name="currentDate">Date to be evaluated</param>
    /// <returns>A DateTime of the day with the time set to "00:00:00:000".</returns>
    public static DateTime StartOfDay(this DateTime currentDate)
    {
        return new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
    }

    /// <summary>
    /// Extension method used to return a DateTime of the first day of the month with the time set to "00:00:00:000" which is the first moment of the first day of the month.
    /// </summary>
    /// <param name="currentDate">Date to be evaluated</param>
    /// <returns>A DateTime of the first day of the month with the time set to "00:00:00:000".</returns>
    public static DateTime StartOfMonth(this DateTime currentDate)
    {
        return new DateTime(currentDate.Year, currentDate.Month, 1);
    }

    /// <summary>Extension method used to return a DateTime of the first day of the year with the time set to "00:00:00:000" which is the first moment of the first day of the firsr month of the year.
    /// </summary>
    /// <param name="currentDate">Date to be evaluated</param>
    /// <returns>A DateTime of the first day of the year with the time set to "00:00:00:000".</returns>
    public static DateTime StartOfYear(this DateTime currentDate)
    {
        return new DateTime(currentDate.Year, 1, 1);
    }
}
