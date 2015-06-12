using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emmola.Helpers;
using System.Threading;
using System.Globalization;

namespace Emmola.Helpers.Tests
{
  [TestClass]
  public class DateTimeHelperTest
  {
    [TestMethod]
    public void ToFilenameTest()
    {
      Assert.AreEqual("2015-06-12-113025-000", new DateTime(2015, 6, 12, 11, 30, 25).ToFileName());
    }

    [TestMethod]
    public void MaxMinTest()
    {
      var now = DateTime.Now;
      var tommorow = now.AddDays(1);

      Assert.AreEqual(now, DateTimeHelper.Min(now, tommorow));
      Assert.AreEqual(tommorow, DateTimeHelper.Max(now, tommorow));
    }

    [TestMethod]
    public void ToReadableTest()
    {
      Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

      var justnow = DateTime.Now.AddSeconds(-1);
      Assert.AreEqual("Just now", justnow.ToReadable());

      var threeMinAgo = justnow.AddMinutes(-3);
      Assert.AreEqual("3 minutes ago", threeMinAgo.ToReadable());

      var tommorow = DateTime.Now.AddDays(1);
      Assert.AreEqual("Tommorow", tommorow.ToReadable());

      var afterTwoHours = DateTime.Now.AddHours(2);
      Assert.AreEqual("After 2 hours", afterTwoHours.ToReadable());

      var yearAgo = DateTime.Now.AddYears(-1);
      Assert.AreEqual(yearAgo.ToDateString(), yearAgo.ToReadable(true));

    }
  }
}
