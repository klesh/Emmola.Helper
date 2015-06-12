using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Emmola.Helpers
{
  public static class ObjectHelper
  {
    /// <summary>
    /// Return a shallow dump format with type name
    /// </summary>
    public static string Dump<T>(T instance, int tabs = 1)
    {
      if (instance == null)
        return null;

      var type = instance == null ? typeof(T) : instance.GetType();

      var typeName = type.Name;
      if (typeName.StartsWith("System.Data.Entity.DynamicProxies"))
        typeName = type.BaseType.Name;

      var builder = new StringBuilder();
      builder.AppendTabbedLine(tabs, "<Start {0}>", typeName);

      if (instance == null)
      {
        builder.AppendTabbedLine(tabs + 1, "null");
      }
      else if (type.IsSimpleType())
      {
        builder.AppendTabbedLine(tabs + 1, Convert.ToString(instance));
      }
      else
      {
        var properties = type.GetProperties().Where(p => p.PropertyType.IsSimpleType());
        foreach (var property in properties)
        {
          builder.AppendTabbedLine(tabs + 1, string.Format("  {0}:{1}", property.Name, property.GetValue(instance)));
        }
      }

      builder.AppendTabbedLine(tabs, string.Format("<End {0}>", typeName));
      return builder.ToString();
    }

    /// <summary>
    /// Return inst's property value;
    /// </summary>
    public static object Get(object inst, string name)
    {
      return inst.GetType().GetProperty(name).GetValue(inst);
    }

    /// <summary>
    /// Set inst's property value;
    /// </summary>
    public static void Set(object inst, string name, object value)
    {
      inst.GetType().GetProperty(name).SetValue(inst, value);
    }

    /// <summary>
    /// Return inst's property value and Convert.ToString
    /// </summary>
    public static string GetString(object inst, string name)
    {
      return Convert.ToString(Get(inst, name));
    }

    /// <summary>
    /// Convert property value of inst to readable format
    /// DateTime : return DATE/TIME format when DataTypeAttribute serve
    /// Decimal: return ToMoney when DataTypeAttribute.DataType == DataType.Currency
    /// IEnumerable: return string.JOIN(", ", value);
    /// </summary>
    /// <param name="inst">Instance</param>
    /// <param name="name">Property Name</param>
    public static string ToReadable(object inst, string name)
    {
      var property = inst.GetType().GetProperty(name);
      var value = property.GetValue(inst);

      if (value is Enum)
        return ((Enum)value).ToReadable();

      if (value is bool?)
        return ((bool?)value).ToReadable();

      if (value is int?)
        return ((int?)value).ToReadable();

      if (value is DateTime?)
      {
        var dataTypeAttr = property.GetAttribute<DataTypeAttribute>();
        var datetime = (DateTime?)value;
        if (dataTypeAttr != null)
        {
          if (dataTypeAttr.DataType == DataType.Date)
            return datetime.ToDateString();
          else if (dataTypeAttr.DataType == DataType.Time)
            return datetime.ToTimeString();
          var dateOnly = dataTypeAttr != null && dataTypeAttr.DataType == DataType.Date;
          return dateOnly ? datetime.ToDateString() : datetime.ToDateString();
        }
        return datetime.ToReadable();
      }

      if (value is decimal?)
      {
        var dataTypeAttr = property.GetAttribute<DataTypeAttribute>();
        var money = dataTypeAttr != null && dataTypeAttr.DataType == DataType.Currency;
        var number = (decimal?)value;
        return money ? number.ToMoney() : number.ToReadable();
      }

      if (property.PropertyType.IsEnumerable())
      {
        return string.Join(", ", (IEnumerable)value);
      }

      return Convert.ToString(value);
    }

    /// <summary>
    /// Similar to JavaScript logic, 0, "", null, "False" are threated false
    /// </summary>
    //public static Nullable<bool> ToBool<T>(this Nullable<T> self)
    //  where T : struct
    //{
    //  return self == null ? (bool?)null : Convert.ToBoolean(self.Value);
    //}

    public static Nullable<T> ToNullable<T>(object inst)
      where T : struct
    {
      if (inst == null)
        return null;
      try {
        return (Nullable<T>)Convert.ChangeType(inst, typeof(T));
      } catch {
        return null;
      }
    }

    /// <summary>
    /// Convert anomynous object to QueryString format
    /// </summary>
    /// <param name="inst"></param>
    /// <returns></returns>
    public static string ToQueryString(object inst, bool skipNull = true)
    {
      var nvc = new NameValueCollection();
      var obt = inst.GetType();
      foreach (var pi in obt.GetPublicProperties())
      {
        var value = pi.GetValue(inst);
        if (value == null && skipNull)
          continue;

        if (pi.PropertyType.IsEnumerable())
        {
          foreach (var v in (IEnumerable)value)
            nvc.Add(pi.Name, Convert.ToString(v));
        }
        else
        {
          nvc.Add(pi.Name, Convert.ToString(value));
        }
      }
      return nvc.ToQueryString();
    }

    /// <summary>
    /// Convert an object to byte[]
    /// </summary>
    /// <param name="inst">object to be converted</param>
    /// <returns>byte[]</returns>
    public static byte[] ToBinary(object inst)
    {
      var binaryFormater = new BinaryFormatter();
      using (var stream = new MemoryStream())
      {
        binaryFormater.Serialize(stream, inst);
        return stream.ToArray();
      }
    }

    public static T FromBinary<T>(byte[] bytes)
    {
      var binaryFormater = new BinaryFormatter();
      using (var stream = new MemoryStream(bytes))
      {
        return (T)binaryFormater.Deserialize(stream);
      }
    }
  }
}
