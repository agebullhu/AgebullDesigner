// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-23
// 修改:2014-11-08
// *****************************************************/

using System.Runtime.Serialization;

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     表示区域的节点
    /// </summary>
    [DataContract]
    public sealed class RegionElement : SharpElement
    {
        /// <summary>
        /// 构造
        /// </summary>
        public RegionElement()
        {
            this.ItemFamily = CodeItemFamily.Region;
        }

        /// <summary>
        ///     是否结束
        /// </summary>
        [DataMember]
        public bool IsEnd
        {
            get;
            set;
        }
    }
}
