// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-11-03
// 修改:2014-11-08
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     表示一个代码的组合单元(需要后期分解的)
    /// </summary>
    [DataContract]
    public class MulitCodeElement : CodeElement
    {
        public MulitCodeElement()
        {
            this.ItemFamily = CodeItemFamily.None;
            this.ItemType = CodeItemType.None;
            this.Use = CodeItemUse.None;
            this.BaseElements = new List<CodeElement>();
        }

        /// <summary>
        ///     此单元的文本
        /// </summary>
        [DataMember]
        public List<CodeElement> BaseElements
        {
            get;
            private set;
        }

        public void AddBaseElement(CodeElement item)
        {
            if (item == null)
                return;
            if (item == this)
                return;
            this.BaseElements.Add(item);
            this.Start = this.BaseElements[0].Start;
            this.End = this.BaseElements[this.BaseElements.Count - 1].End;
        }

        public void AddBaseElement(IEnumerable<CodeElement> items)
        {
            if (items == null)
                return;
            foreach (var item in items)
                this.AddBaseElement(item);
            this.Start = this.BaseElements[0].Start;
            this.End = this.BaseElements[this.BaseElements.Count - 1].End;
        }

        /// <summary>
        /// 取得变量名称(未必它就是变量用于匹配)
        /// </summary>
        /// <returns></returns>
        public override string GetVariableName()
        {
            return this.BaseElements.First(p => p.ItemType != CodeItemType.Key_This &&
                p.ItemType != CodeItemType.Key_Base &&
                p.ItemType != CodeItemType.Punctuate_Dot).Word;
        }

        public void Release()
        {
            this.BaseElements = this.BaseElements.OrderBy(p => p.Start).ToList();
            this.Start = this.BaseElements[0].Start;
            this.End = this.BaseElements[this.BaseElements.Count - 1].End;
            this.Word = this.BaseElements.LinkToString("");
        }
    }
}
