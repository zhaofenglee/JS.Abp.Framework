using System.Security.Cryptography.X509Certificates;

namespace JS.AspNetCore.Util
{
    public static class Str
    {
        /// <summary>
        /// 获取字符串不包含最后一位字符
        /// </summary>
        public static string NoLastString(this string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                return text.Substring(0, text.Length - 1);
            }
            else
            {
                return text;
            }

        }
        /// <summary>
        /// 判断是否包含字符（无视大小写）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool ContainsString(this string text, string value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                return text.ToLower().Contains(value);
            }
        }
    }
}