// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-23
// 修改:2014-11-08
// *****************************************************/

#region 引用

using System;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     表示一个代码的基本单元
    /// </summary>
    [DataContract]
    public class CodeElement : CodeItem
    {
        /// <summary>
        /// 动态标识
        /// </summary>
        [IgnoreDataMember ]
        public int Id
        {
            get;
            set;
        }

        #region 属性

        /// <summary>
        ///     所在行
        /// </summary>
        [DataMember]
        public int Line
        {
            get;
            set;
        }

        /// <summary>
        ///     所在列
        /// </summary>
        [DataMember]
        public int Column
        {
            get;
            set;
        }

        /// <summary>
        ///     此单元的文本
        /// </summary>
        [DataMember]
        public string Word
        {
            get;
            set;
        }

        /// <summary>
        ///     计算符号的优先级
        /// </summary>
        [DataMember]
        internal int Level
        {
            get;
            set;
        }


        /// <summary>
        ///     是否为单词
        /// </summary>
        [IgnoreDataMember]
        public override bool IsElement
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///     是否已完成分析
        /// </summary>
        public override AnalyzerState State
        {
            get
            {
                return AnalyzerState.All;
            }
        }
        #endregion


        #region 预编译与区域

        /// <summary>
        ///     编译条件
        /// </summary>
        [DataMember]
        public string Condition
        {
            get;
            set;
        }

        /// <summary>
        ///     跳过不用
        /// </summary>
        [DataMember]
        public bool Skip
        {
            get;
            set;
        }

        /// <summary>
        ///     区域
        /// </summary>
        [DataMember]
        public string Region
        {
            get;
            set;
        }
        [IgnoreDataMember]
        public object Tag
        {
            get;
            set;
        }

        #endregion

        #region 方法


        /*// <summary>
        ///     对于简单对象,剥离包装
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public static CodeElement ToElement(CodeItem el)
        {
            while (true)
            {
                if (el == null)
                {
                    return null;
                }
                if (el is CodeElement)
                {
                    return el as CodeElement;
                }
                //ElementCombination bl = el as ElementCombination;
                //// ReSharper disable once PossibleNullReferenceException
                //el = bl.Elements.FirstOrDefault();
            }
        }

        /// <summary>
        /// 设置这个单词为变量
        /// </summary>
        public void SetIsVariable(IVariable variable)
        {
            this.ItemRace = CodeItemRace.Variable;
            this.ItemFamily = CodeItemFamily.Variable;
            this.ItemType = variable.ItemType;
        }

        /// <summary>
        /// 取得变量名称(未必它就是变量用于匹配)
        /// </summary>
        /// <returns></returns>
        public virtual string GetVariableName()
        {
            return Word;
        }

        /// <summary>
        ///     文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            switch (this.ItemRace)
            {
                case CodeItemRace.Assist:
                    return string.Format("〖{0}〗", this.ItemType);
                case CodeItemRace.Punctuate:
                case CodeItemRace.KeyWord:
                    return string.Format("{0}〖{1}〗", this.Word, this.ItemType);
            }
            switch (this.ItemFamily)
            {
                case CodeItemFamily.Constant:
                    return string.Format("{0}〖{1}〗", this.Word, this.ItemType);
            }
            return string.Format("{3}〖{0}￤{1}￤{2}〗", this.ItemRace, this.ItemFamily, this.ItemType, this.Word);
        }
        */
        #endregion

    }
}
