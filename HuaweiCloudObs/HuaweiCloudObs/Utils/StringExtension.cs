using System;
using System.Linq;
using System.Runtime.Serialization;

namespace HuaweiCloudObs.Utils
{
    public static class StringExtension
    {
        /// <summary>
        /// 驼峰命名 即首字母小写
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string t) => $"{char.ToLower(t[0])}{t[1..]}";
    }

    public static class EnumExtension
    {
        public static string GetEnumMemberOrDefault(this Enum e)
        {
            return e.GetType()
           .GetField(e.ToString())
           .GetCustomAttributes(typeof(EnumMemberAttribute), false)
           .SingleOrDefault() is not EnumMemberAttribute attribute ?
           e.ToString() : attribute.Value;
        }
    }
}
