using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emmola.Helpers.Tests
{
  [TestClass]
  public class CollectionHelperTest
  {
    [TestMethod]
    public void FillArrayTest()
    {
      Assert.IsTrue(new int[4].Fill(2).All(i => i == 2));
      Assert.AreEqual(new int[] { 0, 1, 1, 0 }.Implode(", "), new int[4].Fill(1, 1, 3).Implode(", "));
    }

    [TestMethod]
    public void ImplodeTest()
    {
      Assert.AreEqual("1, 2, 3", new int[] { 1, 2, 3 }.Implode(", "));
    }

    [TestMethod]
    public void ToQueryStringTest()
    {
      var nvc = new NameValueCollection() { { "Foo Bar", "Hello world" }, { "Key", "Value" } };
      Assert.AreEqual("Foo+Bar=Hello+world&Key=Value", nvc.ToQueryString());
    }
  }
}
