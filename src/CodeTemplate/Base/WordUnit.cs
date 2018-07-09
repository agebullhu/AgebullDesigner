using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    /// <summary>
    ///     表示一个基础单词
    /// </summary>
    public class WordUnit : AnalyzeUnitBase
    {
        #region 方法

        /// <summary>
        ///     构造
        /// </summary>
        internal WordUnit()
        {
            Line = -1;
            Column = -1;
            Start = -1;
            End = -1;
        }

        /// <summary>
        ///     构造
        /// </summary>
        internal WordUnit(char ch)
        {
            Line = -1;
            Column = -1;
            Start = -1;
            End = -1;
            Append(-1, ch);
        }

        /// <summary>
        ///     追加字符
        /// </summary>
        /// <param name="idx">文件位置</param>
        /// <param name="ch">字符</param>
        public void Append(int idx, char ch)
        {
            if (Start < 0)
                Start = idx;
            if (idx >= 0)
                End = idx;
            Chars.Add(ch);
            if (ItemType == CodeItemType.String)
            {
                return;
            }
            if (UnitType == TemplateUnitType.Content)
            {
                if (ch == '\n')
                    IsLine = true;
                return;
            }
            if (UnitType < TemplateUnitType.Content )
            {
                return;
            }
            IsPunctuate = !(ch >= 128
                            || ch >= '0' && ch <= '9' //数字
                            || ch == '_'
                            || ch >= 'A' && ch <= 'Z' //字母
                            || ch >= 'a' && ch <= 'z');
            if (!IsPunctuate)
                return;
            switch (ch)
            {
                case '\n':
                    IsLine = true;
                    IsSpace = true;
                    SetRace(CodeItemRace.Assist, CodeItemFamily.Space, CodeItemType.Line);
                    break;
                case '\r':
                case '\t':
                case ' ':
                case '\u2028':
                case '\u2029':
                case '\u000B':
                case '\u000C':
                    IsSpace = true;
                    SetRace(CodeItemRace.Assist, CodeItemFamily.Space, CodeItemType.Space);
                    break;
                    //    //case ';':
                    //    //    IsLine = true;
                    //    //    IsSpace = true;
                    //    //    SetRace(CodeItemRace.Assist, CodeItemFamily.Space, CodeItemType.Line);
                    //    //    break;
            }
        }

        /// <summary>
        ///     追加字符
        /// </summary>
        /// <param name="unit">字符</param>
        public void Append(WordUnit unit)
        {
            if (unit == null)
                return;
            Chars.AddRange(unit.Chars);
            if (unit.End >= 0)
                End = unit.End;
        }

        public void Clear()
        {
            Chars.Clear();
            Start = -1;
            End = -1;
            _cusWord = null;
        }

        public override bool IsEmpty => Start < 0 || End < 0 || (_cusWord == null && Chars.Count == 0);

        #endregion

        #region 内容

        /// <summary>
        /// 是否基础单元
        /// </summary>
        public override bool IsUnit => true;

        /// <summary>
        ///     为主控符的等级
        /// </summary>
        [DataMember]
        public int PrimaryLevel { get; set; } = 32;


        /// <summary>
        /// 数字类型,0不是,1整数,2小数
        /// </summary>
        public int NumberType { get; set; }

        /// <summary>
        ///     关键字
        /// </summary>
        public bool IsKeyWord { get; set; }

        /// <summary>
        ///     是否标点
        /// </summary>
        public bool IsPunctuate { get; private set; }

        /// <summary>
        ///     是否空白
        /// </summary>
        public bool IsSpace { get; private set; }

        /// <summary>
        ///     是否换行
        /// </summary>
        public bool IsLine { get; private set; }

        /// <summary>
        ///     字符内容
        /// </summary>
        public List<char> Chars { get; } = new List<char>();

        private string _cusWord;
        /// <summary>
        ///     是否可以当成单词
        /// </summary>
        [DataMember]
        public override bool IsWord => true;

        /// <summary>
        ///     当前的单词
        /// </summary>
        public sealed override string Word
        {
            get => _cusWord ?? (Chars.Count == 0 ? string.Empty : string.Concat(Chars));
            set => _cusWord = value;
        }


        /// <summary>
        ///     当前的单词
        /// </summary>
        public char Char => Chars.Count == 1 ? Chars[0] : '\0';

        /// <summary>
        ///     当前的单词
        /// </summary>
        public char FirstChar => Chars.Count == 0 ? '\0' : Chars[0];

        #endregion


        /// <summary>
        ///     开始结束各有几个无用字符
        /// </summary>
        [DataMember]
        public int EmptyChar { get; set; }

        /// <summary>
        /// 到文本
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            return Word;
        }

        /// <summary>
        /// 到文本
        /// </summary>
        /// <returns></returns>
        public override void ToContent(StringBuilder builder)
        {
            if (_cusWord != null)
            {
                foreach (var ch in _cusWord)
                {
                    switch (ch)
                    {
                        default:
                            builder.Append(ch);
                            break;
                        case '\'':
                            builder.Append(@"\'");
                            break;
                        case '\n':
                            builder.Append(@"\n");
                            break;
                        case '\r':
                            builder.Append(@"\r");
                            break;
                        case '\t':
                            builder.Append(@"\t");
                            break;
                        case '\\':
                            builder.Append(@"\\");
                            break;
                    }
                }
            }
            else
            {
                foreach (var ch in Chars)
                {
                    switch (ch)
                    {
                        default:
                            builder.Append(ch);
                            break;
                        case '\'':
                            builder.Append(@"\'");
                            break;
                        case '\n':
                            builder.Append(@"\n");
                            break;
                        case '\r':
                            builder.Append(@"\r");
                            break;
                        case '\t':
                            builder.Append(@"\t");
                            break;
                        case '\\':
                            builder.Append(@"\\");
                            break;
                    }
                }
            }
        }
    }
}