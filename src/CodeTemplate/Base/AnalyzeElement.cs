using System.Collections.Generic;
using System.Text;

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     分析时使用的单元
    /// </summary>
    public sealed class AnalyzeElement
    {
        public void SetRace(CodeItemRace race, CodeItemFamily family, CodeItemType type = CodeItemType.None)
        {
            ItemRace = race;
            foreach (var element in Elements)
            {
                element.SetRace(race, family, type);
            }
        }

        /// <summary>
        ///     文件中的起始位置
        /// </summary>
        public int Start { get; set; } = -1;

        /// <summary>
        ///     文件中的结束位置
        /// </summary>
        public int End { get; set; } = -1;

        /// <summary>
        ///     文本长度
        /// </summary>
        public int Lenght => End - Start + 1;

        /// <summary>
        ///     是否块
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        ///     是否内容
        /// </summary>
        public bool IsContent { get; set; }

        /// <summary>
        ///     种族(最大分类)
        /// </summary>
        public CodeItemRace ItemRace
        {
            get;
            set;
        }

        /// <summary>
        ///     辅助对象(当前为文本格式化的参数)
        /// </summary>
        public readonly List<AnalyzeElement> Assists = new List<AnalyzeElement>();

        /// <summary>
        /// 包含的节点
        /// </summary>
        public readonly List<WordElement> Elements = new List<WordElement>();
        /// <summary>
        /// 当前节点
        /// </summary>
        public WordElement CurrentElement
        {
            get;
            set;
        }
        /// <summary>
        /// 节点的原始内容
        /// </summary>
        public string Code()
        {
            StringBuilder code = new StringBuilder();
            foreach (var element in Elements)
            {
                bool cando = !element.IsLine;
                foreach (var word in element.Words)
                {
                    if (word.IsLine)
                    {
                        code.Append("\r\n");
                        if (word.Level > 0)
                            code.Append(' ', word.Level);
                        continue;
                    }
                    if (cando)
                        code.Append(word.Word);
                }
            }
            return code.ToString();
        }
        /// <summary>
        /// 节点的内容
        /// </summary>
        public string Content()
        {
            foreach (var element in Elements)
            {
                if (!element.IsRelease)
                    element.Release();
            }
            StringBuilder code = new StringBuilder();
            if (Elements.Count == 0 /*|| Elements.All(p => p.IsLine)*/)
            {
                return string.Empty;
            }
            if (Elements.Count == 1)
            {
                return Elements[0].Word;
            }
            int i = 0;
            var start = Elements[i++];
            if (start.ItemType == CodeItemType.Line)
            {
                bool s = false;
                foreach (var word in start.Words)
                {
                    if (word.IsLine)
                    {
                        code.Append("\r\n");
                        code.Append("\r\n");
                        s = true;
                        continue;
                    }
                    if (s)
                    {
                        WriteContentWord(word, code);
                    }
                }
            }
            for (; i < Elements.Count - 1; i++)
            {
                foreach (var word in Elements[i].Words)
                {
                    if (word.IsLine)
                    {
                        code.Append("\r\n");
                    }
                    else
                    {
                        WriteContentWord(word, code);
                    }
                }
            }
            foreach (var word in Elements[i].Words)
            {
                if (word.IsLine)
                {
                    break;
                }
                WriteContentWord(word, code);
            }
            return code.ToString();
        }

        private static void WriteContentWord(WordUnit word, StringBuilder code)
        {
            if (word.IsReplenish)
            {
                code.Append(word.Word);
                return;
            }
            foreach (char ch in word.Chars)
            {
                if (ch > 256 || ch == '[' || ch == ']' || ch == '%')
                {
                    code.AppendFormat("/u{0:x4}", (int)ch);
                }
                else
                {
                    code.Append(ch);
                }
            }
        }

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Code();
        }

        /// <summary>
        ///     构造
        /// </summary>
        public AnalyzeElement()
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="element">节点</param>
        public AnalyzeElement(WordElement element)
        {
            CurrentElement = element;
            if (element != null)
                Append(element);
        }
        /// <summary>
        ///     合并单元
        /// </summary>
        /// <param name="element">代码的基本单元</param>
        public void Append(WordElement element)
        {
            if (element == null || Elements.Contains(element))
                return;
            if ((Start < 0 || Start > element.Start) && element.Start >= 0)
                Start = element.Start;
            if (End < element.End && element.End >= 0)
                End = element.End;
            Elements.Add(CurrentElement = element);
        }
        /// <summary>
        ///     合并单元
        /// </summary>
        /// <param name="element">代码的基本单元</param>
        public void AddAssist(AnalyzeElement element)
        {
            if (element == null || Assists.Contains(element))
                return;
            if ((Start < 0 || Start > element.Start) && element.Start >= 0)
                Start = element.Start;
            if (End < element.End && element.End >= 0)
                End = element.End;
            Assists.Add(element);
        }

        /// <summary>
        ///     接入单元
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="element">代码的基本单元</param>
        public void Insert(int idx, WordElement element)
        {
            if (element == null || Elements.Contains(element))
                return;
            Elements.Insert(idx, element);
            
            //if ((Start < 0 || Start > element.Start) && element.Start >= 0)
            //    Start = element.Start;
            //if (End < element.End && element.End >= 0)
            //    End = element.End;
        }
        
        /// <summary>
        ///     接入补全字符
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="str">字符</param>
        public void Insert(int idx, string str)
        {
            var unit = new WordUnit
            {
                IsReplenish = true,
                Word=str
            };
            unit.Chars.AddRange(str);
            Elements.Insert(idx, new WordElement(unit)
            {
                IsReplenish = true,
                ItemRace = CodeItemRace.Completion
            });
        }

        /// <summary>
        ///     追加补全字符
        /// </summary>
        /// <param name="str">字符</param>
        public void Append(string str)
        {
            var unit = new WordUnit
            {
                IsReplenish = true,
                Word = str
            };
            unit.Chars.AddRange(str);
            Elements.Add(new WordElement(unit)
            {
                IsReplenish = true,
                ItemRace = CodeItemRace.Completion
            });
        }
    }
}