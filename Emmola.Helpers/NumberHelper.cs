using Emmola.Helpers.i18n;
using System;

namespace Emmola.Helpers
{
  public static class NumberHelper
  {

    /// <summary>
    /// Return human friendly format
    /// </summary>
    public static string ToReadable(this decimal? self, string defaultText = null)
    {
      return self == null ? defaultText : self.Value.ToReadable();
    }

    /// <summary>
    /// Return human friendly format
    /// </summary>
    public static string ToReadable(this decimal self)
    {
      return self.ToString("G29");
    }

    /// <summary>
    /// Return a capacity readable string
    /// </summary>
    public static string ToCapacity(this long self)
    {
      string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB" };
      double len = (double)self;
      int order = 0;
      while (len >= 1024 && order + 1 < sizes.Length)
      {
        order++;
        len = len / 1024;
      }
      return String.Format("{0:0.##}{1}", len, sizes[order]);
    }


    /// <summary>
    /// Return a Money format
    /// </summary>
    public static string ToMoney(this decimal self)
    {
      return self.ToString("N");
    }

    /// <summary>
    /// Return a Money format
    /// </summary>
    public static string ToMoney(this decimal? self)
    {
      return self == null ? "0.00" : self.Value.ToMoney();
    }

    /// <summary>
    /// Return readable format with comma
    /// </summary>
    public static string ToReadable(this int? self)
    {
      return self == null ? null : self.Value.ToString("N0");
    }

    /// <summary>
    /// Return percentage format.
    /// </summary>
    public static string ToPercentage(float usage)
    {
      return String.Format("{0}%", Math.Round(usage));
    }

    /// <summary>
    /// Return Yes/No if not null.
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string ToReadable(this bool? self)
    {
      return self == null ? null : self.Value ? Res.True : Res.False;
    }

    /// <summary>
    /// Calculate pages base on pagesize
    /// </summary>
    /// <param name="pageSize"></param>
    public static long CalculatePages(this long records, long pageSize)
    {
      var totalPages = records / pageSize; // 取得总页数
      if (totalPages == 0 && records > 0)
        totalPages = 1;
      else if (records % pageSize > 0)
        totalPages += 1;
      return totalPages;
    }
  }
}
