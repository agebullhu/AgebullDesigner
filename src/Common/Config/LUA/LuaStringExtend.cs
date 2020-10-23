using Agebull.EntityModel.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Agebull.Common.LUA
{
    /// <summary>
    /// 开放到LUA的文本扩展
    /// </summary>
    public static class LuaStringExtend
    {
        #region 拼音

        /// <summary>
        ///     得到文字的拼音
        /// </summary>
        /// <param name="chinese"> </param>
        /// <returns> </returns>
        public static string PinYin(string chinese)
        {
            return ChinessPinYin.PinYin(chinese);
        }

        /// <summary>
        ///     得到文字每个字的拼音的第一个
        /// </summary>
        /// <param name="chinese"> </param>
        /// <returns> </returns>
        public static string ShengMu(string chinese)
        {
            return ChinessPinYin.ShengMu(chinese);
        }

        /// <summary>
        ///     将字符串转为字母表示(中文用声母,其它不变)
        /// </summary>
        /// <param name="chinese"> </param>
        /// <returns> </returns>
        public static string ToFieldName(string chinese)
        {
            return chinese.ToAsciiField();
        }

        #endregion

        #region Base64编码

        /// <summary>
        ///     得到文字每个字的拼音的第一个
        /// </summary>
        /// <param name="code"> </param>
        /// <returns> 返回编码的值($SG$) </returns>
        public static string EncodeBase64(string code)
        {
            return UnicodeBase64.EncodeBase64(code);
        }

        /// <summary>
        ///     对文本进行Base64解码
        /// </summary>
        /// <param name="code"> </param>
        /// <returns> 返回编码的值($SG$) </returns>
        public static string DecodeBase64(string code)
        {
            return UnicodeBase64.DecodeBase64(code);
        }

        #endregion

        #region 命名支持

        /// <summary>
        ///     使用匈牙利法命名
        /// </summary>
        /// <param name="str">内容</param>
        /// <returns>字符器</returns>
        public static string ToHungaryName(string str)
        {
            try
            {
                var words = GlobalConfig.ToWords(str);
                var sb = new StringBuilder();
                foreach (var word in words)
                    sb.Append(word.ToUWord());
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        ///     使用驼峰命名
        /// </summary>
        /// <param name="str">内容</param>
        /// <returns>字符器</returns>
        public static string ToHumpName(string str)
        {
            try
            {
                var words = GlobalConfig.ToWords(str);
                var sb = new StringBuilder();
                sb.Append(words[0].ToLower());
                for (var index = 1; index < words.Count; index++)
                    sb.Append(words[index].ToUWord());
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        ///     使用Pascal命名
        /// </summary>
        /// <param name="str">内容</param>
        /// <returns>字符器</returns>
        public static string ToPascalName(string str)
        {
            try
            {
                var words = GlobalConfig.ToWords(str);
                var sb = new StringBuilder();
                foreach (var word in words)
                    sb.Append(word.ToUWord());
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        ///     使用下划线命名
        /// </summary>
        /// <param name="str">内容</param>
        /// <returns>字符器</returns>
        public static string ToUnderName(string str)
        {
            try
            {
                var words = GlobalConfig.ToWords(str);
                var sb = new StringBuilder();
                var first = true;
                foreach (var word in words)
                {
                    if (first)
                        first = false;
                    else
                        sb.Append('_');
                    sb.Append(word.ToLower());
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }

        #endregion

        #region 文本

        /// <summary>
        ///     拆分到单词(每个标点或大写字母作为分隔符)
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns>复数形式</returns>
        public static string[] SpliteWord(string word)
        {
            return StringHelper.SpliteWord(word);
        }

        /// <summary>
        ///     一个字符器转为名称
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="toFirstUpper">是否首字母大写</param>
        /// <returns>字符器</returns>
        public static string ToWordName(string str, bool toFirstUpper = true)
        {
            return GlobalConfig.ToWordName(str);
        }

        /// <summary>
        ///     一个字符器转为名称
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="link">连接字符</param>
        /// <param name="toFirstUpper">是否首字母大写</param>
        /// <returns>字符器</returns>
        public static string ToLinkWordName(string str, string link, bool toFirstUpper)
        {
            return GlobalConfig.ToWordName(str);
        }

        /// <summary>
        ///     到一个单词的复数形式
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns>复数形式</returns>
        public static string ToPluralism(string word)
        {
            return StringHelper.ToPluralism(word);
        }

        /// <summary>
        ///     列表转为以一个字符分隔的文本
        /// </summary>
        /// <param name="ls"> 列表 </param>
        /// <param name="dot"> 分隔符 </param>
        /// <returns> 文本 </returns>
        public static string ListToString(IEnumerable ls, char dot = ',')
        {
            return StringHelper.ListToString(ls, dot);
        }

        /// <summary>
        ///     列表转为以一个字符分隔的文本
        /// </summary>
        /// <param name="ls"> 列表 </param>
        /// <returns> 文本 </returns>
        public static string DictionaryToString(Dictionary<string, string> ls)
        {
            return StringHelper.ToString(ls);
        }

        /// <summary>
        ///     替换多个内容对
        /// </summary>
        /// <param name="str"></param>
        /// <param name="rs">成对的替换,请保证是成对的</param>
        /// <returns></returns>
        public static string MulitReplace(string str, params string[] rs)
        {
            return str.MulitReplace(rs);
        }

        /// <summary>
        ///     替换多个内容为一个
        /// </summary>
        /// <param name="str"></param>
        /// <param name="last">替换到</param>
        /// <param name="org">被替换</param>
        /// <returns></returns>
        public static string MulitReplaceOne(string str, string last, params string[] org)
        {
            return str.MulitReplace2(last, org);
        }

        /// <summary>
        ///     为空或是缺省文本
        /// </summary>
        /// <param name="word"> </param>
        /// <param name="def"> </param>
        /// <returns> </returns>
        public static bool IsNullOrDefault(string word, string def)
        {
            return word.IsNullOrDefault(def);
        }

        /// <summary>
        ///     检测一个文字是否一个单词
        /// </summary>
        /// <param name="text"> </param>
        /// <returns> </returns>
        public static bool IsName(string text)
        {
            return text.IsName();
        }

        /// <summary>
        ///     到首字母大写的文本
        /// </summary>
        /// <param name="word"> </param>
        /// <returns> </returns>
        public static string ToUWord(string word)
        {
            return word.ToUWord();
        }

        /// <summary>
        ///     到首字母小写的文本
        /// </summary>
        /// <param name="word"> </param>
        /// <returns> </returns>
        public static string ToLWord(string word)
        {
            return word.ToLWord();
        }


        /// <summary>
        ///     到首字母大写的文本
        /// </summary>
        /// <param name="a"> </param>
        /// <param name="b"></param>
        /// <returns> </returns>
        public static bool IsEquals(string a, string b)
        {
            var aemp = string.IsNullOrWhiteSpace(a);
            var bemp = string.IsNullOrWhiteSpace(b);
            if (aemp && bemp)
                return false;
            if (aemp || bemp)
                return false;
            a = a.Trim();
            b = b.Trim();
            return string.Equals(a.Trim(), b.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     取得文本的长度(全角算两个)
        /// </summary>
        /// <param name="word"> </param>
        /// <returns> </returns>
        public static int GetLen(string word)
        {
            return word.GetLen();
        }

        #endregion

        #region 其它


        /// <summary>
        ///     从C#的类型转为SQLite的类型
        /// </summary>
        /// <param name="key"> 字段的KEY</param>
        public static string ToDataBaseType(string key)
        {
            var column = GlobalConfig.GetConfig<FieldConfig>(key);
            if (column == null)
                return null;
            switch (column.DbType.ToLower())
            {
                case "decimal":
                case "numeric":
                    if (column.Datalen <= 10 || column.Datalen > 18)
                        column.Datalen = 18;
                    if (column.Scale <= 0 || column.Scale > 18)
                        column.Scale = 8;
                    return $"decimal({column.Datalen},{column.Scale})";
                case "binary":
                    if (column.Datalen <= 0 || column.Datalen >= 4000)
                        column.Datalen = -1;
                    else if (column.Datalen < 100)
                        column.Datalen = 100;
                    return $"{column.DbType}({(column.Datalen <= 0 ? "max" : column.Datalen.ToString())})";
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                    if (column.Datalen < 0 || column.Datalen > 4000)
                        column.Datalen = 0;
                    return string.Format("{1}({0})", column.Datalen == 0 ? "max" : column.Datalen.ToString(), column.DbType.ToUpper());
                default:
                    return column.DbType.ToUpper();
            }
        }

        #endregion
    }
}