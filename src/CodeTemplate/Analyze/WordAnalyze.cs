// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-11-10
// 修改:2014-11-10
// *****************************************************/

#region 引用

using System.Collections.Generic;

#endregion
namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    /// <summary>
    ///     基本单词分析
    /// </summary>
    public sealed class WordAnalyze
    {
        #region 过程对象

        /// <summary>
        ///     基本单词
        /// </summary>
        public List<WordUnit> Words;

        /// <summary>
        ///     基本单词
        /// </summary>
        public List<WordElement> Elements { get; } = new List<WordElement>();

        /// <summary>
        ///     完整的代码文件的文本
        /// </summary>
        public string Code { get; set; }


        #endregion

        #region 分析文本

        public void Reset(string code)
        {
            Code = code;
            Words = null;
            Elements.Clear();
        }

        /// <summary>
        ///     分析文本
        /// </summary>
        public static List<WordElement> Analyze(string code)
        {
            return string.IsNullOrEmpty(code)
                    ? new List<WordElement>()
                    : new WordAnalyze().AnalyzeWords(code);
        }

        /// <summary>
        ///     分析文本
        /// </summary>
        public void Analyze()
        {
            SplitWords();
            MergeWords();
        }

        /// <summary>
        ///     分析文本
        /// </summary>
        public List<WordElement> AnalyzeWords(string code)
        {
            Code = code;
            Words = null;
            Elements.Clear();
            Code = code;
            Analyze(); 
            return Elements;
        }

        #endregion

        #region 拆分基本单词

        /// <summary>
        ///     拆分基本单词
        /// </summary>
        /// <returns></returns>
        internal void SplitWords()
        {
            Words = new List<WordUnit>();
            WordUnit word = null;
            int line = 0;
            int col = 0;
            int idx = 0;
            for (int index = 0; index < Code.Length; index++)
            {
                char ch = Code[index];
                if (ch == '\r')
                    continue;
                col++;


                if (ch >= 128
                    || ch == '_'
                    || ch >= '0' && ch <= '9' //数字
                    || ch >= 'A' && ch <= 'Z' //字母
                    || ch >= 'a' && ch <= 'z')
                {
                    if (word != null && word.IsPunctuate)
                    {
                        Words.Add(word = new WordUnit { Line = line, Column = col });
                    }
                    else if (word == null)
                    {
                        Words.Add(word = new WordUnit { Line = line, Column = col });
                    }
                }
                else //标点符号后期独立处理(都是独立的一个字)
                {
                    Words.Add(word = new WordUnit { Line = line, Column = col });
                    if (ch == '\n')
                    {
                        line++;
                        col = 0;
                    }
                }
                word.Append(idx++, ch);
            }
        }

        #endregion
        
        #region 合并基本单词

        /// <summary>
        ///     合并基本单词
        /// </summary>
        /// <returns></returns>
        internal void MergeWords()
        {
            Elements.Clear();
            WordUnit pre = null;
            WordElement cur = null;
            int line = 0;
            int col = 0;
            foreach (var word in Words)
            {
                word.Line = line;
                word.Column = col;
                if (word.IsLine)
                {
                    line++;
                    col = 0;
                }
                else
                {
                    col += word.Lenght;
                }

                if (cur == null || !word.IsSpace || !pre.IsSpace)
                {
                    Elements.Add(cur = new WordElement(word));
                    pre = word;
                    continue;
                }
                cur.Append(word);
            }
            foreach (var element in Elements)
            {
                element.Release();
            }
        }

        #endregion
    }
}
