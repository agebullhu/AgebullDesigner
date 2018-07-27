using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    /// <summary>
    ///     ��ʾһ����������
    /// </summary>
    public class WordUnit : AnalyzeUnitBase
    {
        #region ����

        /// <summary>
        ///     ����
        /// </summary>
        internal WordUnit()
        {
            Line = -1;
            Column = -1;
            Start = -1;
            End = -1;
        }

        /// <summary>
        ///     ����
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
        ///     ׷���ַ�
        /// </summary>
        /// <param name="idx">�ļ�λ��</param>
        /// <param name="ch">�ַ�</param>
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
                            || ch >= '0' && ch <= '9' //����
                            || ch == '_'
                            || ch >= 'A' && ch <= 'Z' //��ĸ
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
        ///     ׷���ַ�
        /// </summary>
        /// <param name="unit">�ַ�</param>
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

        #region ����

        /// <summary>
        /// �Ƿ������Ԫ
        /// </summary>
        public override bool IsUnit => true;

        /// <summary>
        ///     Ϊ���ط��ĵȼ�
        /// </summary>
        [DataMember]
        public int PrimaryLevel { get; set; } = 32;


        /// <summary>
        /// ��������,0����,1����,2С��
        /// </summary>
        public int NumberType { get; set; }

        /// <summary>
        ///     �ؼ���
        /// </summary>
        public bool IsKeyWord { get; set; }

        /// <summary>
        ///     �Ƿ���
        /// </summary>
        public bool IsPunctuate { get; private set; }

        /// <summary>
        ///     �Ƿ�հ�
        /// </summary>
        public bool IsSpace { get; private set; }

        /// <summary>
        ///     �Ƿ���
        /// </summary>
        public bool IsLine { get; private set; }

        /// <summary>
        ///     �ַ�����
        /// </summary>
        public List<char> Chars { get; } = new List<char>();

        private string _cusWord;
        /// <summary>
        ///     �Ƿ���Ե��ɵ���
        /// </summary>
        [DataMember]
        public override bool IsWord => true;

        /// <summary>
        ///     ��ǰ�ĵ���
        /// </summary>
        public sealed override string Word
        {
            get => _cusWord ?? (Chars.Count == 0 ? string.Empty : string.Concat(Chars));
            set => _cusWord = value;
        }


        /// <summary>
        ///     ��ǰ�ĵ���
        /// </summary>
        public char Char => Chars.Count == 1 ? Chars[0] : '\0';

        /// <summary>
        ///     ��ǰ�ĵ���
        /// </summary>
        public char FirstChar => Chars.Count == 0 ? '\0' : Chars[0];

        #endregion


        /// <summary>
        ///     ��ʼ�������м��������ַ�
        /// </summary>
        [DataMember]
        public int EmptyChar { get; set; }

        /// <summary>
        /// ���ı�
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            return Word;
        }

        /// <summary>
        /// ���ı�
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