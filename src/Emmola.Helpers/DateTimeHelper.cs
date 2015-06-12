using Emmola.Helpers.i18n;
using System;

namespace Emmola.Helpers
{
  public static class DateTimeHelper
  {
    public static string DATETIME_FORMAT = "yyyy/M/d H:m";
    public static string DATE_FORMAT = "yyyy/M/d";
    public static string TIME_FORMAT = "H:m";

    /// <summary>
    /// Return a Date string, set DateTimeHelper.DATE_FORMAT to change output format
    /// </summary>
    public static string ToDateString(this DateTime self)
    {
      return self.ToString(DATE_FORMAT);
    }

    /// <summary>
    /// Return a Date string, set DateTimeHelper.TIME_FORMAT to change output format
    /// </summary>
    public static string ToTimeString(this DateTime self)
    {
      return self.ToString(TIME_FORMAT);
    }

    /// <summary>
    /// Return a Date And Time string, set DateTimeHelper.DATETIME_FORMAT to change output format
    /// </summary>
    public static string ToDateTimeString(this DateTime self)
    {
      return self.ToString(DATETIME_FORMAT);
    }

    /// <summary>
    /// Return a Date string, set DateTimeHelper.DATE_FORMAT to change output format
    /// </summary>
    public static string ToDateString(this DateTime? self)
    {
      return self == null ? null : self.Value.ToDateString();
    }

    /// <summary>
    /// Return a Date string, set DateTimeHelper.DATE_FORMAT to change output format
    /// </summary>
    public static string ToTimeString(this DateTime? self)
    {
      return self == null ? null : self.Value.ToTimeString();
    }

    /// <summary>
    /// Return a Date And Time string, set DateTimeHelper.DATETIME_FORMAT to change output format
    /// </summary>
    public static string ToDateTimeString(this DateTime? self)
    {
      return self == null ? null : self.Value.ToDateTimeString();
    }

    /// <summary>
    /// Return a readable timespan to DateTime.Now if not null.
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string ToReadable(this DateTime? self, bool dateOnly = false)
    {
      return self.ReadableSpanTo(DateTime.Now, dateOnly);
    }

    /// <summary>
    /// Return a readable timespan to DateTime.Now if not null.
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string ToReadable(this DateTime self, bool dateOnly = false)
    {
      return self.ReadableSpanTo(DateTime.Now, dateOnly);
    }

    /// <summary>
    /// Return a readable string, positive as before, negitive as after.
    /// </summary>
    /// <param name="self"></param>
    public static string ToReadable(this TimeSpan self)
    {
      var days = self.Days;
      if (days == 1)
        return Res.YESTERDAY;
      else if (days == 2)
        return Res.DAY_BEFORE_YESTERDAY;
      else if (days == -1)
        return Res.TOMMOROW;
      else if (days == -2)
        return Res.DAY_AFTER_TOMMORW;
      else if (days > 2)
        return Res.DAYS_AGO.FormatMe(days);
      else if (days < -2)
        return Res.DAYS_AFTER.FormatMe(Math.Abs(days));

      if (self.Hours > 0)
        return Res.HOURS_AGO.FormatMe(self.Hours);
      else if (self.Hours < 0)
        return Res.HOURS_AFTER.FormatMe(Math.Abs(self.Hours));

      if (self.Minutes > 0)
        return Res.MINUTES_AGO.FormatMe(self.Minutes);
      else if (self.Minutes < 0)
        return Res.MINUTES_AFTER.FormatMe(Math.Abs(self.Minutes));

      return self.Ticks < 0 ? Res.RIGHT_NOW : Res.JUST_NOW;
    }

    /// <summary>
    /// Return readable TimeSpan, or DateTime format if over a month.
    /// </summary>
    /// <param name="compare">DateTime to be compared</param>
    /// <param name="dateOnly">Show short date format while span over a month</param>
    public static string ReadableSpanTo(this DateTime? self, DateTime compare, bool dateOnly = false)
    {
      if (self == null)
        return null;

      return self.ToReadable(dateOnly);
    }

    /// <summary>
    /// Return readable TimeSpan, or DateTime format if over a month.
    /// </summary>
    /// <param name="compare">DateTime to be compared</param>
    /// <param name="dateOnly">Show short date format while span over a month</param>
    public static string ReadableSpanTo(this DateTime self, DateTime compare, bool dateOnly = false)
    {
      var span = dateOnly ? compare.Date - self.Date : compare - self;

      if (Math.Abs(span.Days) > 31)
        return self.ToString(dateOnly ? DATE_FORMAT : DATETIME_FORMAT);

      return span.ToReadable();
    }

    /// <summary>
    /// Return a formatted string suitable as file name
    /// </summary>
    /// <param name="datetime"></param>
    /// <returns>string like 2015-05-29-102733-485</returns>
    public static string ToFileName(this DateTime datetime)
    {
      return datetime.ToString("yyyy-MM-dd-HHmmss-fff");
    }
    
    /// <summary>
    /// Return earlier DateTime of 2 DateTimes
    /// </summary>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    /// <returns>earlier</returns>
    public static DateTime Min(DateTime date1, DateTime date2)
    {
      return new DateTime(Math.Min(date1.Ticks, date2.Ticks));
    }

    /// <summary>
    /// Return later DateTime of 2 DateTimes
    /// </summary>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    /// <returns>later</returns>
    public static DateTime Max(DateTime date1, DateTime date2)
    {
      return new DateTime(Math.Max(date1.Ticks, date2.Ticks));
    }

    /// <summary>
    /// Return null when all are null, return valid one if another is null
    /// </summary>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    public static DateTime? Max(DateTime? date1, DateTime? date2)
    {
      if (date1 == null && date2 == null)
        return null;
      if (date1.HasValue != date2.HasValue)
        return date1 ?? date2;
      return Max(date1.Value, date2.Value);
    }

    /// <summary>
    /// Return null if anyone is null.
    /// </summary>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    public static DateTime? Min(DateTime? date1, DateTime? date2)
    {
      if (date1 == null && date2 == null)
        return null;
      if (date1.HasValue != date2.HasValue)
        return null;
      return Min(date1.Value, date2.Value);
    }

    /// <summary>
    /// Converts a given DateTime into a Unix timestamp
    /// </summary>
    /// <param name="value">Any DateTime</param>
    /// <returns>The given DateTime in Unix timestamp format</returns>
    public static long ToUnixTimeStamp(this DateTime self)
    {
      return (self.Ticks - new DateTime(1970, 1, 1).Ticks) / TimeSpan.TicksPerSecond;
    }

    /// <summary>
    /// Convert a Unix timestamp to DateTime
    /// </summary>
    /// <param name="value">Unit timestamp</param>
    /// <returns>DateTime</returns>
    public static DateTime FromUnixTimeStamp(long timestamp)
    {
      return new DateTime(timestamp * TimeSpan.TicksPerSecond + new DateTime(1970, 1, 1).Ticks);
    }

    /// <summary>
    /// Return itself when is valid and later than DateTime.Now, otherwise return DateTime.Now
    /// </summary>
    /// <param name="dateTime"></param>
    public static DateTime LaterOrNow(this DateTime? dateTime)
    {
      return dateTime == null || dateTime.Value < DateTime.Now ? DateTime.Now : dateTime.Value;
    }
  }
}
