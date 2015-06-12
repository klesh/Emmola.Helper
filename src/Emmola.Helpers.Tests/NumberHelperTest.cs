using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emmola.Helpers.Tests
{
  [TestClass]
  public class NumberHelperTest
  {
    [TestMethod]
    public void DecimalToReadableTest()
    {
      decimal money = 10.00m;
      decimal money2 = 10.01m;
      decimal money3 = 10000000000m;
      decimal money4 = 10000000000.01m;
      Assert.AreEqual("10", money.ToReadable());
      Assert.AreEqual("10.01", money2.ToReadable());
      Assert.AreEqual("10,000,000,000", money3.ToReadable());
      Assert.AreEqual("10,000,000,000.01", money4.ToReadable());
    }

    [TestMethod]
    public void DecimalToMoneyTest()
    {
      decimal money = 10m;
      decimal money2 = 10.01m;
      decimal money3 = 10000000000m;
      decimal money4 = 10000000000.01m;
      Assert.AreEqual("10.00", money.ToMoney());
      Assert.AreEqual("10.01", money2.ToMoney());
      Assert.AreEqual("10,000,000,000.00", money3.ToMoney());
      Assert.AreEqual("10,000,000,000.01", money4.ToMoney());
    }

    [TestMethod]
    public void FloatToPercentageTest()
    {
      Assert.AreEqual("12%", (0.12f).ToPercentage());
    }
  }
}
