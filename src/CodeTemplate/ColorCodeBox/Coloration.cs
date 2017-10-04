using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Agebull.CodeRefactor.CodeTemplate
{
    /// <summary>
    /// ��ɫ��
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
        /// �Ƿ����
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
        /// �Ƿ���Դ�Сд
        /// </summary>
        public bool IsIgnoreCase
        {
            get;
            set;
        }

        /// <summary>
        /// ��Ҫ��ɫ�ĵ��ʼ���
        /// </summary>
        public List<string> WordList
        {
            get;
            set;
        }

        /// <summary>
        /// ����һ����Ҫ��ɫ�ĵ���
        /// </summary>
        /// <param name="theWord"></param>
        public void AddOneWord(string theWord)
        {
            if (IsIgnoreCase)
                theWord = theWord.ToUpper();
            WordList.Add(theWord);
        }
        /// <summary>
        /// ����һ����Ҫ��ɫ�ĵ���
        /// </summary>
        /// <param name="collection"></param>
        public void AddMultiWords(IEnumerable<string> collection)
        {
            WordList.AddRange(IsIgnoreCase
                ? collection.Where(p => !string.IsNullOrEmpty(p)).Select(p => p.ToUpper())
                : collection.Where(p => !string.IsNullOrEmpty(p)));
        }

        /// <summary>
        /// ��Ҫ��ɫ��������ʽ
        /// </summary>
        internal FontStyle FontStyle
        {
            get;
            set;
        }

        /// <summary>
        /// ����
        /// </summary>
        internal Color BackColor
        {
            get;
            set;
        }
        /// <summary>
        /// ǰ��ɫ
        /// </summary>
        public Color ForeColor
        {
            get;
            set;
        }
    }
}