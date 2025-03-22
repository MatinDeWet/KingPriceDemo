using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace KingPriceDemo.Application.Common.Extensions
{
    public static class AttributeTools
    {
        public static string GetDisplayName(this MemberInfo source, bool useNameDefault = true)
        {
            var dispAttr = source.GetCustomAttribute<DisplayAttribute>();
            if (dispAttr != null && !string.IsNullOrEmpty(dispAttr.Name))
                return dispAttr.Name;

            var dispNameAttr = source.GetCustomAttribute<DisplayNameAttribute>();
            if (dispNameAttr != null && !string.IsNullOrEmpty(dispNameAttr.DisplayName))
                return dispNameAttr.DisplayName;

            return useNameDefault ? source.Name.ToStringWithSpaces() : null!;
        }

        public static string GetDisplayName(this FieldInfo source, bool useNameDefault = true) => ((MemberInfo)source).GetDisplayName(useNameDefault);

        public static string GetDisplayName<T>(this T source, bool useNameDefault = true) where T : struct
        {
            FieldInfo fieldInfo = source.GetType()
                .GetField(source.ToString()!)!;

            if (fieldInfo is null)
                return useNameDefault
                    ? source.ToString()!.ToStringWithSpaces()
                    : null!;

            return fieldInfo.GetDisplayName(useNameDefault);
        }

        public static string GetDisplayName(this Type source, bool useNameDefault = true)
        {
            var dispAttr = source.GetCustomAttribute<DisplayAttribute>();
            if (dispAttr != null && !string.IsNullOrEmpty(dispAttr.Name))
                return dispAttr.Name;

            var dispNameAttr = source.GetCustomAttribute<DisplayNameAttribute>();
            if (dispNameAttr != null && !string.IsNullOrEmpty(dispNameAttr.DisplayName))
                return dispNameAttr.DisplayName;

            return useNameDefault
                ? source.GetName()
                    .ToStringWithSpaces()
                : null!;
        }

        public static string GetDisplayName(this TypeInfo source, bool useNameDefault = true)
        {
            return source.AsType().GetDisplayName(useNameDefault);
        }
    }
}
