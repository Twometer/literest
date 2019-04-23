using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LiteRestReloaded.Util
{
    public static class Extensions
    {
        public static T GetAttributeObject<T>(this MemberInfo info, bool inherit = false)
        {
            object[] attrs = info.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length == 0 || !(attrs[0] is T))
                return default;
            return (T)attrs[0];
        }

        public static T GetAttributeObject<T>(this ParameterInfo info, bool inherit = false)
        {
            object[] attrs = info.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length == 0 || !(attrs[0] is T))
                return default;
            return (T)attrs[0];
        }


        public static bool HasAttribute<T>(this MemberInfo info)
        {
            return info.GetCustomAttributes(typeof(T), false).Length != 0;
        }

        public static bool HasAttribute<T>(this ParameterInfo info)
        {
            return info.GetCustomAttributes(typeof(T), false).Length != 0;
        }

    }
}
