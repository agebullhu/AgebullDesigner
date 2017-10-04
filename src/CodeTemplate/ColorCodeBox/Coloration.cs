using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Agebull.CodeRefactor.CodeTemplate
{
    /// <summary>
    /// 着色器
    /// </summary>
    public class Coloration
    {
        public Coloration()
        {
            WordList = new List<string>();
            FontStyle = FontStyle.Regular;
            BackColor = Color.White;
            ForeColor = Color.Black;
            IsIgnoreCase = true;
        }
        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool IsInclude(string word)
        {
            if (IsIgnoreCase)
                word = word.ToUpper();
            return WordList.IndexOf(word) >= 0;
        }

        /// <summary>
        /// 是否忽略大小写
        /// </summary>
        public bool IsIgnoreCase
        {
            get;
            set;
        }

        /// <summary>
        /// 需要着色的单词集合
        /// </summary>
        public List<string> WordList
        {
            get;
            set;
        }

        /// <summary>
        /// 增加一个需要着色的单词
        /// </summary>
        /// <param name="theWord"></param>
        public void AddOneWord(string theWord)
        {
            if (IsIgnoreCase)
                theWord = theWord.ToUpper();
            WordList.Add(theWord);
        }
        /// <summary>
        /// 增加一组需要着色的单词
        /// </summary>
        /// <param name="collection"></param>
        public void AddMultiWords(IEnumerable<string> collection)
        {
            WordList.AddRange(IsIgnoreCase
                ? collection.Where(p => !string.IsNullOrEmpty(p)).Select(p => p.ToUpper())
                : collection.Where(p => !string.IsNullOrEmpty(p)));
        }

        /// <summary>
        /// 需要着色的字体样式
        /// </summary>
        internal FontStyle FontStyle
        {
            get;
            set;
        }

        /// <summary>
        /// 背景
        /// </summary>
        internal Color BackColor
        {
            get;
            set;
        }
        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForeColor
        {
            get;
            set;
        }
    }
}