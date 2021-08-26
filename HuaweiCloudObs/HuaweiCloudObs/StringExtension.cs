namespace HuaweiCloudObs
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
}
