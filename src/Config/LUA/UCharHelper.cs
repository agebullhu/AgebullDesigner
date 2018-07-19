using System.Globalization;
using System.Text;

namespace Agebull.Common.LUA
{
    /// <summary>
    /// LUA支持
    /// </summary>
    public static class UCharHelper
    {
        /// <summary>
        /// 转为LUA格式
        /// </summary>
        /// <param name="str">文本</param>
        /// <returns>LUA格式文本</returns>
        public static string ToLuaString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            StringBuilder builder = new StringBuilder();
            foreach (var ch in str)
            {
                if (ch > 255 || ch == '\\' || ch == '\'' || ch == '\"' || ch == '{' || ch == '}')
                {
                    builder.AppendFormat("/u{0:x4}", (int)ch);
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 转为LUA格式
        /// </summary>
        /// <param name="str">文本</param>
        /// <returns>LUA格式文本</returns>
        public static string FromLuaChar(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            StringBuilder builder = new StringBuilder();
            int u = 0;
            StringBuilder ubuilder = new StringBuilder();
            foreach (var ch in str)
            {
                switch (ch)
                {
                    case '/':
                        if (u > 0)
                        {
                            builder.Append(ubuilder);
                            ubuilder.Clear();
                        }
                        u = 1;
                        ubuilder.Append(ch);
                        continue;
                    case 'u':
                        if (u == 1)
                        {
                            u = 2;
                            ubuilder.Append(ch);
                            continue;
                        }
                        break;
                    case '\r':
                        continue;
                    case '\t':
                    case ' ':
                    case '\u2028':
                    case '\u2029':
                    case '\u000B':
                    case '\u000C':
                        if (u > 0)
                        {
                            builder.Append(ubuilder);
                            ubuilder.Clear();
                            u = 0;
                        }
                        builder.Append(ch);
                        continue;
                    case '\n':
                        if (u > 0)
                        {
                            builder.Append(ubuilder);
                            ubuilder.Clear();
                            u = 0;
                        }
                        builder.Append('\r');
                        builder.Append(ch);
                        continue;

                }
                if (u == 2)
                {
                    ubuilder.Append(ch);
                    if (ubuilder.Length < 6)
                        continue;
                    if (int.TryParse(ubuilder.ToString().Substring(2),NumberStyles.AllowHexSpecifier , null, out var nch))
                    {
                        builder.Append((char) nch);
                    }
                    else
                    {
                        builder.Append(ubuilder);
                    }
                    ubuilder.Clear();
                    u = 0;
                    continue;
                }
                if (u > 0)
                {
                    builder.Append(ubuilder);
                    ubuilder.Clear();
                    u = 0;
                }
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
