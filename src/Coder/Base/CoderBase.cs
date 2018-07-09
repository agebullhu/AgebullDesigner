using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// �������ɻ���
    /// </summary>
    public class CoderBase
    {
        #region �ı�����
        /// <summary>
        /// ������ĸ��д���Ƹ�ʽ
        /// </summary>
        /// <param name="str">������</param>
        /// <returns>������</returns>
        public static string ToWordName(string str)
        {
            try
            {
                var words = ToWords(str);
                StringBuilder sb = new StringBuilder();
                words.ForEach(p =>
                {
                    sb.Append(p.ToUWord());
                });
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        /// ���շ����Ƹ�ʽ
        /// </summary>
        /// <param name="str">������</param>
        /// <param name="head">����ͷ</param>
        /// <returns>������</returns>
        public static string ToTfWordName(string str, string head = "")
        {
            try
            {
                var words = ToWords(str);
                StringBuilder sb = new StringBuilder();
                sb.Append(head);
                sb.Append(words[0].ToLower());
                for (var index = 1; index < words.Count; index++)
                {
                    var word = words[index];
                    sb.Append(word.ToUWord());
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }
        /// <summary>
        /// �����Ƹ�ʽ
        /// </summary>
        /// <param name="str">������</param>
        /// <param name="link">�����ַ�</param>
        /// <param name="uWord">�Ƿ��д</param>
        /// <returns>������</returns>
        public static string ToLinkWordName(string str, string link, bool uWord)
        {
            try
            {
                var words = ToWords(str);
                StringBuilder sb = new StringBuilder();
                bool preEn = words[0][0] < 255;
                sb.Append(uWord ? words[0].ToUWord() : words[0].ToLower());
                for (var index = 1; index < words.Count; index++)
                {
                    var word = words[index];
                    if ((word[0] < 255 && !preEn) || preEn)
                        sb.Append(link);
                    sb.Append(uWord ? word.ToUWord() : word.ToLower());
                    preEn = word[0] < 255;
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                return str;
            }
        }
        /// <summary>
        /// �����Ƹ�ʽ
        /// </summary>
        /// <param name="words">������</param>
        /// <param name="link">�����ַ�</param>
        /// <param name="uWord">�Ƿ��д</param>
        /// <returns>����</returns>
        public static string ToName(List<string> words, char link = '_', bool uWord = false)
        {
            if (words.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            bool preEn = words[0][0] < 255;
            sb.Append(words[0]);
            for (var index = 1; index < words.Count; index++)
            {
                var word = words[index];
                if ((word[0] < 255 && !preEn) || preEn)
                    sb.Append(link);
                sb.Append(uWord ? word.ToUWord() : word.ToLower());
                preEn = word[0] < 255;
            }
            return sb.ToString();
        }

        /// <summary>
        /// ��ֵ�����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> SplitWords(string str)
        {
            var words = new List<string>();
            if (string.IsNullOrWhiteSpace(str))
                return words;
            var baseWords = str.Split(NoneLanguageChar, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (var word in baseWords)
            {
                if (word.All(c => c > 255))
                {
                    words.Add(word);
                    continue;
                }
                if (word.All(c => (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')))
                {
                    words.Add(word);
                    continue;
                }
                if (word.All(c => (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9')))
                {
                    words.Add(word);
                    continue;
                }
                foreach (var c in word)
                {
                    if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                    {
                        sb.Append(c);
                        continue;
                    }
                    if ((c >= 'A' && c <= 'Z') || (c > 255))
                    {
                        if (sb.Length > 0)
                        {
                            words.Add(sb.ToString());
                            sb.Clear();
                        }
                        sb.Append(c);
                        continue;
                    }
                    if (sb.Length > 0)
                    {
                        words.Add(sb.ToString());
                        sb.Clear();
                    }
                }
                if (sb.Length > 0)
                {
                    words.Add(sb.ToString());
                    sb.Clear();
                }
            }
            var result = new List<string>();
            string pre = null;
            foreach (var word in words)
            {
                if (pre != null)
                {
                    if (pre == "I" && word == "D")
                    {
                        if (result.Count > 0)
                            result.RemoveAt(result.Count - 1);
                        result.Add("ID");
                        continue;
                    }
                }
                result.Add(word);
                pre = word;
            }
            return result;
        }

        public static List<string> ToWords(string str, bool uWord = false)
        {
            var words = SplitWords(str);
            if (!uWord || words.Count == 0)
                return words;
            return words.Select(p => p.ToUWord()).ToList();
        }
        public static string ToMyName(string str)
        {
            var words = SplitWords(str);
            if (words.Count == 0)
                return str;
            return ToName(words);
        }

        /// <summary>
        /// ת��Ϊע���ı�
        /// </summary>
        /// <param name="str">Ҫת������ı�</param>
        /// <param name="space">�ո�����</param>
        /// <returns>��ȷ��ʾΪC#ע�͵��ı�</returns>
        protected static string ToRemString(string str, int space = 8)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            var rp = str.Replace("&", "��").Replace("\r", "").Replace("<", "��").Replace(">", "��");
            var sp = rp.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;
            foreach (var line in sp)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sb.AppendLine();
                    sb.Append(' ', space);
                    sb.Append("/// ");
                }
                sb.Append(line.Trim());
            }
            return sb.ToString();
        }
        #endregion
        #region ��������


        #region ��ȡ����

        /// <summary>
        ///     ȡ�ö�Ӧ���͵Ķ����Ƴ���
        /// </summary>
        /// <param name="csharpType">C#������</param>
        /// <returns>��ȡ������</returns>
        public static string GetByteLen(string csharpType)
        {
            switch (csharpType.ToLower())
            {
                case "bool":
                case "byte":
                    return "reader.BaseStream.Position += 1;";
                case "short":
                case "ushort":
                    return "reader.BaseStream.Position += 2;";
                case "guid":
                case "decimal":
                    return "reader.BaseStream.Position += 16;";
                case "long":
                case "ulong":
                case "double":
                    return "reader.BaseStream.Position += 8;";
                case "int":
                case "uint":
                case "float":
                    return "reader.BaseStream.Position += 4;";
                case "char":
                    return "reader.ReadChar();";
                case "string":
                    return "reader.ReadString();";
                default:
                    return @"throw new Exception(""binary value error!"")";
            }
        }

        #endregion

        #endregion
        #region ���ƴ���
        public static char[] SplitChar =
        {
            '��', ' ', '\t','\n', '\r',',', '��', ';',  '��', ':', '��', '.', '��','��'
        };
        public static readonly char[] NoneNameChar =
        {
            '0', '1', '2', '3', '4', '5','6', '7', '8', '9',
            '��', ' ', '\t','\n', '\r','\'', '"',
            ',', '��', ';',  '��', ':', '��', '.', '��','��', '*', '��', '/','��', '\\','��',
            '=','<', '>', '[', ']', '{', '}', '(', ')','��', '��', '��', '��', '��', '��', '��', '��',
            '-', '_', '+', '~', '`', '?', '&', '^', '%', '$', '#', '@', '!',
            '��', '��', '��', '��', '��', '��', '��', '��', '��'
        };
        public static char[] NoneLanguageChar =
        {
            '��', ' ', '\t','\n', '\r','\'', '"',
            ',', '��', ';',  '��', ':', '��', '.', '��','��', '*', '��', '/','��', '\\','��',
            '=','<', '>', '[', ']', '{', '}', '(', ')','��', '��', '��', '��', '��', '��', '��', '��',
            '-', '_', '+', '~', '`', '?', '&', '^', '%', '$', '#', '@', '!',
            '��', '��', '��', '��', '��', '��', '��', '��', '��'
        };
        public static char[] EmptyChar =
        {
            '��', ' ', '\t','\n', '\r'
        };
        public static string[] NoneNameString =
        {
            "0", "1", "2", "3", "4", "5","6", "7", "8", "9",
            //"��", "��", "��", "��", "��", "��","��", "��", "��", "��",
            "��", " ", "\t","\n", "\r","\'", "\"",
            ",", "��", ";",  "��", ":", "��", ".", "��","��", "*", "��", "/","��", "\\","��",
            "=","<", ">", "[", "]", "{", "}", "(", ")","��", "��", "��", "��", "��", "��", "��", "��",
            "-", "_", "+", "~", "`", "?", "&", "^", "%", "$", "#", "@", "!",
            "��", "��", "��", "��", "��", "��", "��", "��", "��"
        };

        public static string[] NoneLanguageString =
        {
            "��", " ", "\t","\n", "\r","\'", "\"",
            ",", "��", ";",  "��", ":", "��", ".", "��","��", "*", "��", "/","��", "\\","��",
            "=","<", ">", "[", "]", "{", "}", "(", ")","��", "��", "��", "��", "��", "��", "��", "��",
            "-", "_", "+", "~", "`", "?", "&", "^", "%", "$", "#", "@", "!",
            "��", "��", "��", "��", "��", "��", "��", "��", "��"
        };

        public static void RepairConfigName(ConfigBase config, bool noName = false)
        {
            if (config == null)
                return;
            config.Caption = ReplceWord(string.Equals(config.Caption, config.Name, StringComparison.OrdinalIgnoreCase) ? config.Name : config.Caption, string.Equals(config.Caption, config.Name, StringComparison.OrdinalIgnoreCase));
            config.Description = ReplceWord(string.Equals(config.Description, config.Name, StringComparison.OrdinalIgnoreCase) ? config.Name : config.Description, string.Equals(config.Description, config.Name, StringComparison.OrdinalIgnoreCase));
            if (!noName)
                config.Name = config.Name?.Trim(NoneLanguageChar).MulitReplace2('_', NoneLanguageChar);
        }


        private static string ReplceWord(string caption, bool isEn)
        {
            if (caption == null)
                return null;
            caption = caption.Trim(NoneLanguageChar);
            if (isEn)
                caption = BaiduFanYi.FanYi(caption);
            caption = caption.Replace("blackcard", "�ڿ�");
            caption = caption.Replace("vleader", "V���");
            return caption;
        }
        #endregion
        #region Ŀ¼��չ
        /// <summary>
        /// ��鲢��ɴ����ļ�·��
        /// </summary>
        /// <param name="project"></param>
        /// <param name="root"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string CheckPath(ProjectConfig project, string root, params string[] names)
        {
            if (names.Length == 0)
            {
                return string.IsNullOrWhiteSpace(project.BranchFolder) 
                    ? root
                    : IOHelper.CheckPath(root, project.BranchFolder);
            }
            if (string.IsNullOrWhiteSpace(project.BranchFolder))
                return IOHelper.CheckPath(root, names);
            var list = new List<string>();
            list.AddRange(names);
            list.Add(project.BranchFolder);
            return IOHelper.CheckPath(root, list.ToArray());
        }
        

        /// <summary>
        /// ��鲢����ĵ��ļ�·��
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static string GetDocumentPath(ProjectConfig project)
        {
            return IOHelper.CheckPath(SolutionConfig.Current.RootPath, SolutionConfig.Current.DocFolder);
            
        }
        #endregion
    }
}