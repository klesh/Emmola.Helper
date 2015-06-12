using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emmola.Helpers;

namespace Emmola.Helpers.Tests
{
  [TestClass]
  public class StringHelperTest
  {
    [TestMethod]
    public void DigestTest()
    {
      Assert.AreEqual("68E109F0F40CA72A15E05CC22786F8E6", "HelloWorld".DigestMD5());
      Assert.AreEqual("DB8AC1C259EB89D4A131B253BACFCA5F319D54F2", "HelloWorld".DigestSHA1());
    }

    [TestMethod]
    public void RandomTextTest()
    {
      for (var i = 0; i < 20; i ++ )
      {
        Assert.AreEqual(8, StringHelper.RandomText(8).Length);
        Assert.IsTrue(StringHelper.RandomDigits().All(c => c >= '0' && c <= '9'));
        Assert.IsTrue(StringHelper.RandomLetters().All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')));
      }
    }

    [TestMethod]
    public void ToHexStringTest()
    {
      Assert.AreEqual("080C1E", new byte[] { 8, 12, 30 }.ToHexString());
    }

    [TestMethod]
    public void IsEmptyNullValidTest()
    {
      string nullString = null;
      string emptyString = "";
      string validString = "ABC";

      Assert.IsTrue(emptyString.IsEmpty());
      Assert.IsFalse(nullString.IsEmpty());

      Assert.IsFalse(emptyString.IsNull());
      Assert.IsTrue(nullString.IsNull());

      Assert.IsFalse(emptyString.IsValid());
      Assert.IsFalse(nullString.IsValid());
      Assert.IsTrue(validString.IsValid());
    }

    [TestMethod]
    public void RepeatStringTest()
    {
      Assert.AreEqual("ABAB", "AB".Repeat(2));
    }

    [TestMethod]
    public void TitleCaseTest()
    {
      Assert.AreEqual("Foo Bar", "foo bar".ToTitleCase());
    }

    [TestMethod]
    public void EllipsisCutTest()
    {
      Assert.AreEqual("ABC…", "ABCDEFG".Ellipsis(4));
    }

    [TestMethod]
    public void CutStringTest()
    {
      Assert.AreEqual("ABCD", "ABCDEFG".Cut(4));
      Assert.AreEqual("ABC", "ABCDEFG".Cut(-4));
    }

    [TestMethod]
    public void StringOrDefaultTest()
    {
      Assert.AreEqual("ABC", "".OrDefault("ABC"));
      Assert.AreEqual("ABC", ((string)null).OrDefault("ABC"));
      Assert.AreEqual("CBA", "CBA".OrDefault("ABC"));
      Assert.AreEqual("Hello world", "".OrDefault("Hello {0}", "world"));
    }

    [TestMethod]
    public void StringMaskTest()
    {
      Assert.AreEqual("foo***@f*****.com", "foobar@foobar.com".Mask());
      Assert.AreEqual("12***678", "12345678".Mask());
    }

    [TestMethod]
    public void IsDigitsOnlyTest()
    {
      Assert.IsTrue("12345678".IsDigitOnly());
      Assert.IsFalse("1234ABC5678".IsDigitOnly());
    }

    [TestMethod]
    public void IcEqualsTest()
    {
      Assert.IsTrue("ABC".IcEquals("aBc"));
      Assert.IsFalse("ABC".IcEquals("AB"));
    }

    [TestMethod]
    public void AllValidTest()
    {
      Assert.AreEqual(2, new string[] { "A", "B", null, "" }.AllValid().Length);
    }
  }
}
