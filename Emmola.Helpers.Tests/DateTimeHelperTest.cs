using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
  }
}
