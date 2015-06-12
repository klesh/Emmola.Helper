using System;
using System.Linq;
using System.Reflection;

namespace Emmola.Helpers
{
  public static class EnumHelper
  {
    /// <summary>
    /// Return its MemberInfo
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static MemberInfo GetMemberInfo(this Enum self)
    {
      return self.GetType().GetMember(self.ToString()).FirstOrDefault(); 
    }

    /// <summary>
    /// Return its Description from DisplayAttribute instance
    /// </summary>
    public static string GetDescription(this Enum self)
    {
      return self.GetMemberInfo().GetDescription();
    }

    /// <summary>
    /// Return its DisplayAttribute.Name or DisplayNameAttribute.DisplayName
    /// </summary>
    public static string ToReadable(this Enum self)
    {
      return self.GetMemberInfo().GetName(self.ToString());
    }
  }
}
