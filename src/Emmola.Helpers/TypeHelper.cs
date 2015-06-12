using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Emmola.Helpers
{
  public static class TypeHelper
  {
    readonly static Type _stringType = typeof(string);
    readonly static Type _decimalType = typeof(decimal);

    /// <summary>
    /// Check if type is simple type, such as value type, string, enum, decimal。
    /// </summary>
    public static bool IsSimpleType(this Type self)
    {
      return self.IsValueType || self.IsEnum || self == _stringType || self == _decimalType;
    }

    /// <summary>
    /// Return all SubTypes in specified Assemblies
    /// </summary>
    /// <param name="assemblies">Assemblies to search</param>
    /// <param name="concreateOnly">Concreate type only</param>
    /// <param name="nameSpace">Search with in specifed namespace</param>
    public static IEnumerable<Type> FindSubTypes(this Type self
      , IEnumerable<Assembly> assemblies
      , bool concreateOnly = true
      , string nameSpace = null)
    {
      var types = new List<Type>();
      foreach (var assembly in assemblies)
      {
        foreach (var type in assembly.GetTypes())
        {
          if (self.IsAssignableFrom(type))
          {
            if (concreateOnly == true && (type.IsAbstract || type.IsInterface || type == self))
              continue;

            if (nameSpace.IsValid() && !type.Namespace.StartsWith(nameSpace))
              continue;

            types.Add(type);
          }
        }
      }
      return types;
    }

    /// <summary>
    /// Return all SubTypes in current AppDomain
    /// </summary>
    /// <param name="concreateOnly">Concreate type only</param>
    /// <param name="nameSpace">Search with in specifed namespace</param>
    public static IEnumerable<Type> FindSubTypes(this Type self, bool concreateOnly = true, string nameSpace = null)
    {
      return self.FindSubTypes(AppDomain.CurrentDomain.GetAssemblies(), concreateOnly, nameSpace);
    }

    /// <summary>
    /// Return all SubTypes in calling Assembly
    /// </summary>
    /// <param name="concreateOnly">Concreate type only</param>
    /// <param name="nameSpace">Search with in specifed namespace</param>
    public static IEnumerable<Type> FindSubTypesInMe(this Type self, bool concreateOnly = true, string nameSpace = null)
    {
      return self.FindSubTypes(new Assembly[] { Assembly.GetCallingAssembly() }, concreateOnly, nameSpace);
    }

    /// <summary>
    /// Check if is IEnumerable, including Array, GenericIEnumerable but NOT string!
    /// </summary>
    public static bool IsEnumerable(this Type self)
    {
      return self != _stringType && typeof(IEnumerable).IsAssignableFrom(self);
    }

    /// <summary>
    /// Check if Nullable<> Type
    /// </summary>
    public static bool IsNullableType(this Type self)
    {
      return self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// Return Nullable value type.
    /// </summary>
    public static Type GetNullableValueType(this Type self)
    {
      if (!self.IsNullableType())
        return null;
      return self.GenericTypeArguments[0];
    }

    /// <summary>
    /// Same as typeof(T[]).GetElementType but applicable to IEnumerable<T>
    /// </summary>
    public static Type GetElementTypeExt(this Type self)
    {
      if (self.HasElementType)
        return self.GetElementType();
      if (self.IsEnumerable() && self.IsGenericType) 
        return self.GetGenericArguments().First();
      return null;
    }

    /// <summary>
    /// Check if contains a specified Attribute
    /// </summary>
    /// <param name="attributeType">Type of Attribute to find</param>
    public static bool HasAttribute(this MemberInfo self, Type attributeType, bool inherit = true)
    {
      return Attribute.GetCustomAttribute(self, attributeType, inherit) != null;
    }

    /// <summary>
    /// HasAttribute Generic Version
    /// </summary>
    public static bool HasAttribute<T>(this MemberInfo self, bool inherit = true) where T : Attribute
    {
      return self.HasAttribute(typeof(T), inherit);
    }

    /// <summary>
    /// Return specified Attribute Type instance
    /// </summary>
    public static T GetAttribute<T>(this MemberInfo self, bool inherit = true) where T : Attribute
    {
      return Attribute.GetCustomAttribute(self, typeof(T), inherit) as T;
    }

    /// <summary>
    /// Run action if Attribute exists
    /// </summary>
    public static void IfAttribute<T>(this MemberInfo self, Action<T> action, bool inherit = true) 
      where T : Attribute
    {
      var attribute = self.GetAttribute<T>();
      if (attribute != null)
        action(attribute);
    }

    /// <summary>
    /// Return if any property using given type, IEnumerable<type> is considered as true.
    /// </summary>
    /// <param name="type">type to find</param>
    /// <param name="exactMatch">inherite considered equal</param>
    public static bool AnyPropertyUsing(this Type self, Type type, bool inherit = true)
    {
      Func<Type, bool> match = (Type t) => inherit ? t.IsAssignableFrom(type) :  t == type;
      return self.GetProperties().Any(p => 
      {
        var pt = p.PropertyType;
        if (pt.IsEnumerable())
          return match(pt.GetElementTypeExt());
        else
          return match(pt);
      });
    }

    /// <summary>
    /// Return all public Properties 
    /// </summary>
    public static PropertyInfo[] GetPublicProperties(this Type type)
    {
      return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
    }

    /// <summary>
    /// Return all public Properties 
    /// </summary>
    public static IEnumerable<PropertyInfo> GetReadWriteProperties(this Type type)
    {
      return type.GetPublicProperties().Where(p => p.CanRead && p.CanWrite);
    }

    /// <summary>
    /// Return its DisplayAttribute instance;
    /// </summary>
    public static DisplayAttribute GetDisplayAttr(this MemberInfo self)
    {
      return self.GetAttribute<DisplayAttribute>();
    }

    /// <summary>
    /// Return its DisplayNameAttribute instance;
    /// </summary>
    public static DisplayNameAttribute GetDisplayNameAttr(this MemberInfo self)
    {
      return self.GetAttribute<DisplayNameAttribute>();
    }

    /// <summary>
    /// Return DisplayAttribute.Name or DisplayNameAttribute.DisplayName
    /// </summary>
    /// <param name="defaultName">In case none of them is found, and will fallback to TypeName if no defaultName given</param>
    /// <returns></returns>
    public static string GetName(this MemberInfo self, string defaultName = null)
    {
      var displayAttr = self.GetDisplayAttr();
      if (displayAttr != null)
        return displayAttr.Name;

      var displayNameAttr = self.GetDisplayNameAttr();
      if (displayNameAttr != null)
        return displayNameAttr.DisplayName;

      return defaultName.OrDefault(self.Name);
    }

    /// <summary>
    /// Return its Description from DisplayAttribute instance, return null when none is defined
    /// </summary>
    public static string GetDescription(this MemberInfo self)
    {
      var displayAttr = self.GetDisplayAttr();
      if (displayAttr != null)
        return displayAttr.Description;
      return null;
    }
  }
}
