# Introduction
.NET Helper functions collection for reducing reinventing and better readable code

# QuickStart
install package from nuget
```
PM> Install-Package Emmola.Helpers
```
import namespace:
```
using Emmola.Helpers;
```
off to go!


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

### IsEmpty()/IsNull()/IsValid()/Invalid()
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

## "abc".ToUTF8Bytes()
Convert to UTF8 byte array;

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

### DateTime.Now.ToDateString()
Return a Date string, set DateTimeHelper.DATE_FORMAT to change output format

### DateTime.Now.ToTimeString()
Return a Date string, set DateTimeHelper.TIME_FORMAT to change output format

### DateTime.Now.ToDateTimeString()
Return a Date And Time string, set DateTimeHelper.DATETIME_FORMAT to change output format


### DateTimeHelper.FromUnixTimestamp(long unixTimeStamp)
Convert unix timestamp to DateTime

### (DateTime?).LaterOrNow()
Return DateTime if itself has a valid DateTime and later than DateTime.Now, otherwise return DateTime.Now

### new TimeSpan(123).ToReadable()
Return human readable format, like Just now, 3 minutes ago, Tommorow...

### DateTime.Now.ToReadable()
Return human readable format relative to DateTime.Now
```
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
```

## CollectionHelper

### new T[].Fill(element, start, end)
Fill an Array with specified element
```
      Assert.IsTrue(new int[4].Fill(2).All(i => i == 2));
      Assert.AreEqual(new int[] { 0, 1, 1, 0 }.Implode(", "), new int[4].Fill(1, 1, 3).Implode(", "));
```

### new object[]{...}.Implode(separator)
Shortcut to String.Join(separator, params object[])
```
      Assert.AreEqual("1, 2, 3", new int[] { 1, 2, 3 }.Implode(", "));
```

### new HashSet<T>().AddRang(IEnumerable<T>)
Same as List<T>.AddRange(IEnumerable<T>)

### ICollection<T>.AddIfNotNull(elementMaybeNull)
Add element only if it's not null

### new NameValueCollection().ToQueryString()
Convert a NameValueCollection into QueryString format
```
      var nvc = new NameValueCollection() { { "Foo Bar", "Hello world" }, { "Key", "Value" } };
      Assert.AreEqual("Foo+Bar=Hello+world&Key=Value", nvc.ToQueryString());
```

### new NameValueCollection().GetNullable<T>(key)
Try to fetch nullable from NameValueCollection

### new IEnumerable<T>().AnySame()
Check if any two elements are Equal in collection

### new IEnumerable<T>().SimilarityTo(otherIEnumerableT)
Calculate similarity to other collection.


## NumberHelper

### (1000000m).ToReadable()
Convert to readable string
```
      decimal money = 10.00m;
      decimal money2 = 10.01m;
      decimal money3 = 10000000000m;
      decimal money4 = 10000000000.01m;
      Assert.AreEqual("10", money.ToReadable());
      Assert.AreEqual("10.01", money2.ToReadable());
      Assert.AreEqual("10,000,000,000", money3.ToReadable());
      Assert.AreEqual("10,000,000,000.01", money4.ToReadable());
```


### (1000000m).ToMoney()
Convert to money format
```
      decimal money = 10m;
      decimal money2 = 10.01m;
      decimal money3 = 10000000000m;
      decimal money4 = 10000000000.01m;
      Assert.AreEqual("10.00", money.ToMoney());
      Assert.AreEqual("10.01", money2.ToMoney());
      Assert.AreEqual("10,000,000,000.00", money3.ToMoney());
      Assert.AreEqual("10,000,000,000.01", money4.ToMoney());
```

### ((long)12312312).ToCapacity()
Convert to capacity format string, like 1B, 1KB, 1MB etc

### (0.12f).ToPercentage()
Convert to percentage format 

## ObjectHelper

### ObjectHelper.Get(instance, propertyName)
Dynamically get instance's property value

### ObjectHelper.Set(instance, propertyName, propertyValue)
Dynamically set instance's property value

### ObjectHelper.GetString(instance, propertyName)
Dynamically get instance's property value anc convert to string

### ObjectHelper.ToReadable(instance, propertyName)
Base on instance' property DataTypeAttribute, to call off ToReadable/ToMoney/ToDateTimeString automatically.

### ObjectHelper.ToQueryString(instance)
Convert an anomynous object to QueryString format


### ObjectHelper.ToBinary(instance)
Convert instance to byte[] using BinaryFormatter

### ObjectHelper.FromBinary<T>(byte[])
Convert byte[] back to specified T


## SystemHelper

### SystemHelper.IsMono
Check if running under Mono

### SystemHelper.IsWindows
Check if running under Windows

### SystemHelper.IsUnix
Check if running under *nix System(Unix/Linux/Mac)

### SystemHelper.IsMac
Check if running under Mac

### SystemHelper.IsLinux
Check if runfing under Linux

### SystemHelper.IsUnknown
Check if running under Unknon OS

### SystemHelper.Is32bit
Check if running under 32bit system

### SystemHelper.Is64bit
Check if running under 64bit system

### SystemHelper.Is32BitProcess
Check pointer length

### SystemHelper.Is64BitProcess
Check pointer length

### SystemHelper.Run(string command)
Run a shell command and return output result

### SystemHelper.Run(string command, string args)
Run a shell command with args and return output result

### SystemHelper.Os
Operating System fullname

### SystemHelper.GetDirectorySize(path)
Returun total size of its all sub files/folders in bytes


## Classes.SimplePerfCounter
Simple performance counter

### new SimplePerfCounter().OsFullname
Same as SystemHelper.Os

### new SimplePerfCounter().CpuCores
Return cpu cores

### new SimplePerfCounter().MemoryTotal
Return total physical memory in bytes

### new SimplePerfCounter().HarddiskTotal
Return total partition space of current AppDomain BaseDirectory in bytes.

### new SimplePerfCounter().GetCpuUsage()
Return average Cpu Usage during two calls

### new SimplePerfCounter().GetMemoryUsage()
Return average Memory Usage during two calls under Windows
Return current Memory Usage under *nix OS

### new SimplePerfCounter().GetHarddiskUsage()
Return partition usage of current AppDomain BaseDirectory


## WebHelper

### WebHelper.CombineUrl(params string[])
Similar to Path.Combine

### WebHelper.GetClientIP(NameValueCollection headers)
Extract client ip from header collection

### WebHelper.GetHttpClient(string host, string format)
Return a new HttpClient instance accept specified format

### WebHelper.GetJsonHttpClient(string host)
Return a new HttpClient instance accept JSON format
  
### WebHelper.GetXmlHttpClient(string host)
Return a new HttpClient instance accept XML format

### new HttpContent().ReadAsHtmlAsync()
Read html content with charset META TAG info rather than header info