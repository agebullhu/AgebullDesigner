// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-23
// 修改:2014-11-08
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     表示一个单词单元
    /// </summary>
    public sealed class WordElement
    {
        public List<WordUnit> Words { get; } = new List<WordUnit>();
        /// <summary>
        ///     构造
        /// </summary>
        internal WordElement()
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="word">基本单词</param>
        internal WordElement(WordUnit word)
        {
            Append(word);
        }

        /// <summary>
        ///     字符追加的StringBuilder
        /// </summary>
        public StringBuilder CharAppenBuilder { get; } = new StringBuilder();

        /// <summary>
        ///     追加字符
        /// </summary>
        /// <param name="word">基本单词</param>
        public void Append(WordUnit word)
        {
            if (!word.IsReplenish)
            {
                if (Line < 0)
                    Line = word.Line;
                if (Column < 0)
                    Column = word.Column;
                if (Start < 0)
                    Start = word.Start;
                End = word.End;
            }
            if (word.IsPunctuate)
            {
                IsPunctuate = true;
                if (word.IsSpace)
                {
                    ItemRace = CodeItemRace.Assist;
                    ItemFamily = CodeItemFamily.Space;
                    if (word.IsLine)
                        ItemType = CodeItemType.Line;
                }
            }
            CharAppenBuilder.Append(word.Word);
            Words.Add(word);
        }
        /// <summary>
        ///     追加字符
        /// </summary>
        /// <param name="word">基本单词</param>
        public void Append(WordElement word)
        {
            word.Words.ForEach(Append);
        }

        /// <summary>
        ///     结束构造
        /// </summary>
        public void Release()
        {
            Word = CharAppenBuilder.ToString();
            IsRelease = true;
        }

        #region 内容

        /// <summary>
        ///     是否补全节点
        /// </summary>
        [DataMember]
        public bool IsReplenish { get; set; }

        /// <summary>
        ///     是否已中结
        /// </summary>
        public bool IsRelease { get; private set; }

        /// <summary>
        ///     当前的单词
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        ///     当前的单词
        /// </summary>
        public string RealWord => CharAppenBuilder.ToString();

        /// <summary>
        ///     当前的单词
        /// </summary>
        public char Char => CharAppenBuilder.Length == 1 ? CharAppenBuilder[0] : '\0';

        #endregion

        #region 特性



        /// <summary>
        ///     是否错误
        /// </summary>
        public bool IsError { get; set; }

        #endregion
        #region 分类

        public void SetRace(CodeItemRace race, CodeItemFamily family, CodeItemType type = CodeItemType.None)
        {
            ItemRace = race;
            ItemFamily = family;
            ItemType = type;
            foreach (var word in Words)
            {
                word.ItemRace = race;
                word.ItemFamily = family;
                word.ItemType = type;
            }
        }

        /// <summary>
        ///     种族(最大分类)
        /// </summary>
        [DataMember]
        public CodeItemRace ItemRace { get; set; }

        /// <summary>
        ///     家族二级分类
        /// </summary>
        [DataMember]
        public CodeItemFamily ItemFamily { get; set; }

        /// <summary>
        ///     单元类型
        /// </summary>
        [DataMember]
        public CodeItemType ItemType { get; set; }

        /// <summary>
        ///     是否单词
        /// </summary>
        public bool IsWord => !IsPunctuate;

        /// <summary>
        ///     是否为标点
        /// </summary>
        [IgnoreDataMember]
        public bool IsPunctuate { get; set; }

        /// <summary>
        ///     是否空白
        /// </summary>
        public bool IsSpace => IsPunctuate && ItemFamily == CodeItemFamily.Space;

        /// <summary>
        ///     是否空白
        /// </summary>
        public bool IsLine => IsSpace && ItemType == CodeItemType.Line;
        
        #endregion

        #region 文档位置

        /// <summary>
        ///     文件中的行号
        /// </summary>
        [DataMember]
        public int Line { get; set; } = -1;

        /// <summary>
        ///     行中的列号
        /// </summary>
        [DataMember]
        public int Column { get; set; } = -1;

        /// <summary>
        ///     文件中的起始位置
        /// </summary>
        [DataMember]
        public int Start { get; set; } = -1;

        /// <summary>
        ///     文件中的结束位置
        /// </summary>
        [DataMember]
        public int End { get; set; } = -1;

        /// <summary>
        ///     文本长度
        /// </summary>
        [IgnoreDataMember]
        public int Lenght => End < 0 ? 0 : End - Start + 1;

        #endregion
    }


}