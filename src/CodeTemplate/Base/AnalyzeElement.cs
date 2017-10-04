using System.Collections.Generic;
using System.Text;

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     ����ʱʹ�õĵ�Ԫ
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
        ///     �ļ��е���ʼλ��
        /// </summary>
        public int Start { get; set; } = -1;

        /// <summary>
        ///     �ļ��еĽ���λ��
        /// </summary>
        public int End { get; set; } = -1;

        /// <summary>
        ///     �ı�����
        /// </summary>
        public int Lenght => End - Start + 1;

        /// <summary>
        ///     �Ƿ��
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        ///     �Ƿ�����
        /// </summary>
        public bool IsContent { get; set; }

        /// <summary>
        ///     ����(������)
        /// </summary>
        public CodeItemRace ItemRace
        {
            get;
            set;
        }

        /// <summary>
        ///     ��������(��ǰΪ�ı���ʽ���Ĳ���)
        /// </summary>
        public readonly List<AnalyzeElement> Assists = new List<AnalyzeElement>();

        /// <summary>
        /// �����Ľڵ�
        /// </summary>
        public readonly List<WordElement> Elements = new List<WordElement>();
        /// <summary>
        /// ��ǰ�ڵ�
        /// </summary>
        public WordElement CurrentElement
        {
            get;
            set;
        }
        /// <summary>
        /// �ڵ��ԭʼ����
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
        /// �ڵ������
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

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Code();
        }

        /// <summary>
        ///     ����
        /// </summary>
        public AnalyzeElement()
        {
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="element">�ڵ�</param>
        public AnalyzeElement(WordElement element)
        {
            CurrentElement = element;
            if (element != null)
                Append(element);
        }
        /// <summary>
        ///     �ϲ���Ԫ
        /// </summary>
        /// <param name="element">����Ļ�����Ԫ</param>
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
        ///     �ϲ���Ԫ
        /// </summary>
        /// <param name="element">����Ļ�����Ԫ</param>
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
        ///     ���뵥Ԫ
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="element">����Ļ�����Ԫ</param>
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
        ///     ���벹ȫ�ַ�
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="str">�ַ�</param>
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
        ///     ׷�Ӳ�ȫ�ַ�
        /// </summary>
        /// <param name="str">�ַ�</param>
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