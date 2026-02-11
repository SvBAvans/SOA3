namespace Cinema.Util;

public class WeekdayUtil
{
    public static bool IsWeekday(DateTime date)
    {
        return date.DayOfWeek is DayOfWeek.Monday or DayOfWeek.Tuesday or DayOfWeek.Wednesday or DayOfWeek.Thursday;
    }
}