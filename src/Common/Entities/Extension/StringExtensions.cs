namespace Agebull.Common
{
    /// <summary>
    /// 文本扩展
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// 是否空或空白文本
        /// </summary>
        /// <param name="str">文本</param>
        /// <returns></returns>
        public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// 非空或空白文本则进行格式化(格式化参数为此文本)
        /// </summary>
        /// <param name="str">文本</param>
        /// <returns></returns>
        public static string FromatByNotEmpty(this string str, string fmt) =>
            string.IsNullOrWhiteSpace(str)
            ? null
            : string.Format(fmt, str);
    }
}