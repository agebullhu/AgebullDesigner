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

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     表示#的节点
    /// </summary>
    [DataContract]
    public class SharpElement : MulitCodeElement
    {
        /// <summary>
        /// 构造
        /// </summary>
        public SharpElement()
        {
            this.Skip = true;
            this.ItemRace = CodeItemRace.Assist;
            this.ItemType = CodeItemType.Sharp;
            this.ItemFamily = CodeItemFamily.Sharp;
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

        /// <summary>
        ///     名称
        /// </summary>
        [DataMember]
        public string Name
        {
            get;
            set;
        }
    }
}
