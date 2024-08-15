namespace JMayer.Example.WindowsService.BSM;

/// <summary>
/// The statis class contains extension methods for the DateTime.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// The method returns the date time as a string in the 01JAN format.
    /// </summary>
    /// <param name="dateTime">The date time to be formatted.</param>
    /// <returns>A formatted string.</returns>
    public static string ToDayMonthFormat(this DateTime dateTime)
        => $"{dateTime.Day:00}{dateTime.ToString("MMMM").Substring(0, 3).ToUpper()}";
}
