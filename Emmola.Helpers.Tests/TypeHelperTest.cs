using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Emmola.Helpers.Tests
{
  [TestClass]
  public class TypeHelperTest
  {
    [TestMethod]
    public void IsSimpleTypeTest()
    {
      Assert.IsTrue(typeof(string).IsSimpleType());
      Assert.IsTrue(typeof(int).IsSimpleType());
      Assert.IsTrue(typeof(Status).IsSimpleType());
      Assert.IsTrue(typeof(decimal).IsSimpleType());
      Assert.IsFalse(typeof(Foo).IsSimpleType());
    }

    [TestMethod]
    public void FindSubTypeTest()
    {
      Assert.AreEqual(2, typeof(FooBar).FindSubTypesInMe().Count());
      Assert.AreEqual(1, typeof(Foo).FindSubTypesInMe().Count());
      Assert.AreEqual(3, typeof(FooBar).FindSubTypesInMe(false).Count());
    }

    [TestMethod]
    public void IsEnumerableTest()
    {
      Assert.IsFalse(typeof(string).IsEnumerable());
      Assert.IsTrue(typeof(object[]).IsEnumerable());
      Assert.IsTrue(typeof(IEnumerable<int>).IsEnumerable());
      Assert.IsTrue(typeof(IEnumerable).IsEnumerable());
    }

    [TestMethod]
    public void IsNullableTypeTest()
    {
      Assert.IsTrue(typeof(long?).IsNullableType());
      Assert.IsFalse(typeof(string).IsNullableType());
      Assert.IsFalse(typeof(FooBar).IsNullableType());
    }

    [TestMethod]
    public void GetNullableValueTypeTest()
    {
      Assert.AreEqual(typeof(int), typeof(int?).GetNullableValueType());
      Assert.IsNull(typeof(int).GetNullableValueType());
    }

    [TestMethod]
    public void GetElementTypeExt()
    {
      Assert.AreEqual(typeof(string), typeof(IEnumerable<string>).GetElementTypeExt());
      Assert.AreEqual(typeof(string), typeof(string[]).GetElementTypeExt());
    }

    [TestMethod]
    public void AttributeHelperTest()
    {
      Assert.IsTrue(typeof(Bar).HasAttribute<FlagAttribute>());
      Assert.IsTrue(typeof(Bar).HasAttribute(typeof(FlagAttribute)));
      Assert.IsFalse(typeof(Bar).HasAttribute<NonSerializedAttribute>());
      Assert.IsTrue(typeof(Bar).GetMethod("SayHi").HasAttribute<FlagAttribute>());
    }

    [TestMethod]
    public void AnyPropertyUsingTest()
    {
      Assert.IsTrue(typeof(Bar).AnyPropertyUsing(typeof(FooBar)));
      Assert.IsTrue(typeof(Foo).AnyPropertyUsing(typeof(FooBar)));
      Assert.IsFalse(typeof(HelloWorld).AnyPropertyUsing(typeof(FooBar)));
    }

    [TestMethod]
    public void PropertiesTest()
    {
      var properties = typeof(Foo).GetPublicProperties();
      Assert.IsFalse(properties.Any(p => p.Name == "InsideProperty"));
      Assert.IsTrue(properties.Any(p => p.Name == "Foobar"));
    }

    [TestMethod]
    public void DisplayNameTest()
    {
      Assert.AreEqual("HelloWorld", typeof(HelloWorld).GetDisplayNameAttr().DisplayName);
      Assert.AreEqual("Text", typeof(HelloWorld).GetProperty("Text").GetDisplayAttr().Name);
      Assert.AreEqual("HelloWorld", typeof(HelloWorld).GetName());
      Assert.AreEqual("Text", typeof(HelloWorld).GetProperty("Text").GetName());
    }
  }

  public class FlagAttribute : Attribute
  {
  }

  [Flag]
  public abstract class FooBar
  {
    [Flag]
    public virtual void SayHi() { }
  }

  public class Foo : FooBar
  {
    public FooBar Foobar { get; set; }
  }

  public class Bar : Foo
  {
    public override void SayHi() { }

    public IEnumerable<FooBar> Foobars { get; set; }

    private string InsideProperty { get; set; }
  }

  [DisplayName("HelloWorld")]
  public class HelloWorld
  {
    [Display(Name = "Text")]
    public string Text { get; set; }
  }

  public enum Status
  {
    [Display(Name = "ONGOING")]
    Ongoing,
    Failure,
    Success
  }
}
