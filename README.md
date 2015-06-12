# Introduction
.NET Helper functions collection for reducing reinventing and better readable code


## TypeHelper
Assuming we have:
```
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
```

### typeof(string).IsSimpleType()
Check if a Type is a simple type (ValueType/String/Decimal)

```
      Assert.IsTrue(typeof(string).IsSimpleType());
      Assert.IsTrue(typeof(int).IsSimpleType());
      Assert.IsTrue(typeof(Status).IsSimpleType());
      Assert.IsTrue(typeof(decimal).IsSimpleType());
      Assert.IsFalse(typeof(Foo).IsSimpleType());
```

### typeof(object[]).IsEnumerable()
Check if a Type is Array/IEnumerable/IEnumerable<> but NOT string
```
      Assert.IsFalse(typeof(string).IsEnumerable());
      Assert.IsTrue(typeof(object[]).IsEnumerable());
      Assert.IsTrue(typeof(IEnumerable<int>).IsEnumerable());
      Assert.IsTrue(typeof(IEnumerable).IsEnumerable());

```

### typeof(long?).IsNullableType()
Check if a Type is Nullable<>
```
      Assert.IsTrue(typeof(long?).IsNullableType());
      Assert.IsFalse(typeof(string).IsNullableType());
      Assert.IsFalse(typeof(FooBar).IsNullableType());
```

### typeof(int?).GetNullableValueType())
Get Nullable<> value type, return null if Type is not a Nullable<>
```
      Assert.AreEqual(typeof(int), typeof(int?).GetNullableValueType());
      Assert.IsNull(typeof(int).GetNullableValueType());
```

### typeof(FooBar).FindSubTypesInMe(inherit = false)
Find all sub types in current assembly

```
      Assert.AreEqual(2, typeof(FooBar).FindSubTypesInMe().Count());
      Assert.AreEqual(1, typeof(Foo).FindSubTypesInMe().Count());
      Assert.AreEqual(3, typeof(FooBar).FindSubTypesInMe(false).Count());
```

### typeof(IEnumerable<string>).GetElementTypeExt()
Get element type of Array/IEnumerable<>

```
      Assert.AreEqual(typeof(string), typeof(IEnumerable<string>).GetElementTypeExt());
      Assert.AreEqual(typeof(string), typeof(string[]).GetElementTypeExt());
```

### typeof(Bar).HasAttribute<T>() / HasAttribute(Type attributeType)
Check if a MemberInfo contains specified Attribute
```
      Assert.IsTrue(typeof(Bar).HasAttribute<FlagAttribute>());
      Assert.IsTrue(typeof(Bar).HasAttribute(typeof(FlagAttribute)));
      Assert.IsFalse(typeof(Bar).HasAttribute<NonSerializedAttribute>());
      Assert.IsTrue(typeof(Bar).GetMethod("SayHi").HasAttribute<FlagAttribute>());
```

### typeof(Bar).GetAttribute<T>() / GetAttribute(Type attributeType)
Get specified Attribute instance of MemberInfo
```
      Assert.IsTrue(typeof(Bar).HasAttribute<FlagAttribute>());
      Assert.IsTrue(typeof(Bar).HasAttribute(typeof(FlagAttribute)));
      Assert.IsFalse(typeof(Bar).HasAttribute<NonSerializedAttribute>());
      Assert.IsTrue(typeof(Bar).GetMethod("SayHi").HasAttribute<FlagAttribute>());
```

### typeof(Foo).AnyPropertyUsing(typeof(FooBar))
Check if a Type holding any property using specified PropertyType or IEnumerable<PropertyType>

```
      Assert.IsTrue(typeof(Bar).AnyPropertyUsing(typeof(FooBar)));
      Assert.IsTrue(typeof(Foo).AnyPropertyUsing(typeof(FooBar)));
      Assert.IsFalse(typeof(HelloWorld).AnyPropertyUsing(typeof(FooBar)));
```

### typeof(Foo).GetPublicProperties()
Get all public properties
```
      var properties = typeof(Foo).GetPublicProperties();
      Assert.IsFalse(properties.Any(p => p.Name == "InsideProperty"));
      Assert.IsTrue(properties.Any(p => p.Name == "Foobar"));
```

### typeof(HelloWorld).GetDisplayNameAttr()
Get DisplayNameAttribute instance

```
      Assert.AreEqual("HelloWorld", typeof(HelloWorld).GetDisplayNameAttr().DisplayName);
```

### typeof(HelloWorld).GetProperty("Text").GetDisplayAttr()
Get DisplayAttribute instance

```
      Assert.AreEqual("Text", typeof(HelloWorld).GetProperty("Text").GetDisplayAttr().Name);
```

###
Get all public readable/writable properties
```
      var properties = typeof(Foo).GetReadWriteProperties();
      Assert.IsFalse(properties.Any(p => p.Name == "InsideProperty"));
      Assert.IsTrue(properties.Any(p => p.Name == "Foobar"));
      Assert.IsFalse(properties.Any(p => p.Name == "ReadOnly"));
```

## StringHelper

### "HelloWorld".DigestMD5() / "HelloWorld".DigestSHA1()

```
      Assert.AreEqual("68E109F0F40CA72A15E05CC22786F8E6", "HelloWorld".DigestMD5());
      Assert.AreEqual("DB8AC1C259EB89D4A131B253BACFCA5F319D54F2", "HelloWorld".DigestSHA1());
```

### StringHelper.RandomText(length) / StringHelper.RandomDigits(length) / StringHelper.RandomLetters(length)
Generate random string, length default value: 6
```
      for (var i = 0; i < 20; i ++ )
      {
        Assert.AreEqual(8, StringHelper.RandomText(8).Length);
        Assert.IsTrue(StringHelper.RandomDigits().All(c => c >= '0' && c <= '9'));
        Assert.IsTrue(StringHelper.RandomLetters().All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')));
      }
```

### new byte[] { ... }.ToHexString()
Convert byte[] to hex format string
```
      Assert.AreEqual("080C1E", new byte[] { 8, 12, 30 }.ToHexString());
```

### IsEmpty()/IsNull()/IsValid
Check if a String is empty/null/valid
```
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
```

### "AB".Repeat(times)
Repeat itself with specified times
```
      Assert.AreEqual("ABAB", "AB".Repeat(2));
```

### "foo bar".ToTitleCase()
Convert to title case
```
      Assert.AreEqual("Foo Bar", "foo bar".ToTitleCase());
```

### "ABCDEFG".Ellipsis(length)
Ellipsisify a string
```
      Assert.AreEqual("ABC…", "ABCDEFG".Ellipsis(4));
```

### "ABCDEFG".Cut(length)
Cut a string into specified length, if length is negative cut off opposite length in the end
```
      Assert.AreEqual("ABCD", "ABCDEFG".Cut(4));
      Assert.AreEqual("ABC", "ABCDEFG".Cut(-4));
```

### "".OrDefault("ABC", params object[])
Return specified default value while a string is not valid
```
      Assert.AreEqual("ABC", "".OrDefault("ABC"));
      Assert.AreEqual("ABC", ((string)null).OrDefault("ABC"));
      Assert.AreEqual("CBA", "CBA".OrDefault("ABC"));
      Assert.AreEqual("Hello world", "".OrDefault("Hello {0}", "world"));
```

### "foobar@foobar.com".Mask(maskChar = '*')
Mask up a string to protect sensitive information
```
      Assert.AreEqual("foo***@f*****.com", "foobar@foobar.com".Mask());
      Assert.AreEqual("12***678", "12345678".Mask());
```

### "12345678".IsDigitOnly()
Check if a string consist of digits only
```
      Assert.IsTrue("12345678".IsDigitOnly());
      Assert.IsFalse("1234ABC5678".IsDigitOnly());
```

### "ABC".IcEquals("aBc")
Shortcut of String.Equals InvariantCultureIgnoreCase
```
      Assert.IsTrue("ABC".IcEquals("aBc"));
      Assert.IsFalse("ABC".IcEquals("AB"));
```

### new string[] { "A", "B", null, "" }.AllValid()
Pick up all valid strings out of string[]
```
      Assert.AreEqual(2, new string[] { "A", "B", null, "" }.AllValid().Length);
```

### "{0} ({1})".FormatMe(params object[] args)
Shortcut of String.Format()
```
  "{0} ({1})".FormatMe("Hi", "there")
```

## EnumHelper
Assuming we hav a enum type
```
  public enum Progress
  {
    [Display(Name = "Processing", Description = "Current stataus: Processing")]
    Ongoing,
    [Display(Name = "Finished", Description = "Current stataus: Finished")]
    Success,
    Failure
  }
```

### Progress.Ongoing.ToReadable()
Return DisplayAttribute.Name or fallback to literal member name
```
      Assert.AreEqual("Failure", Progress.Failure.ToReadable());
      Assert.AreEqual("Processing", Progress.Ongoing.ToReadable());
```

### Progress.Success.GetDescription()
Return DisplayAttribute.Description
```
      Assert.AreEqual("Current stataus: Finished", Progress.Success.GetDescription());
```

## DateTimeHelper

### DateTime.Now.ToFilename()
Return a formatted string suitable for filename
```
      Assert.AreEqual("2015-06-12-113025-000", new DateTime(2015, 6, 12, 11, 30, 25).ToFileName());
```

### DateTimeHelper.Min(a, b) / DateTimeHelper.Max(a, b)
Math.Min(a, b) / Max.Max(a, b) for DateTime. Also available for Nullable<DateTime>
```
      var now = DateTime.Now;
      var tommorow = now.AddDays(1);

      Assert.AreEqual(now, DateTimeHelper.Min(now, tommorow));
      Assert.AreEqual(tommorow, DateTimeHelper.Max(now, tommorow));
```

### DateTime.Now.ToUnixTimestamp()
Convert DateTime to Unix timestamp


### DateTimeHelper.FromUnixTimestamp(long unixTimeStamp)
Convert unix timestamp to DateTime

### (DateTime?).LaterOrNow()
Return DateTime if itself has a valid DateTime and later than now, otherwise return DateTime.Now